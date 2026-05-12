// ================================================
// FILE: ReservationForm.cs
// ================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SmartWorkspace
{
    public partial class ReservationForm : Form
    {
        private int _selectedReservationID  = 0;
        private int _selectedWorkspaceID    = 0;

        // ── Constructor ──────────────────────────────────────
        public ReservationForm()
        {
            InitializeComponent();
        }

        // ── Form_Load ────────────────────────────────────────
        private void ReservationForm_Load(object sender, EventArgs e)
        {
            LoadMembersCombo();
            LoadWorkspacesCombo();
            LoadReservations();
        }

        // ── Row selected → sync Status combo ─────────────────
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            DataGridViewRow row = dataGridView1.SelectedRows[0];
            _selectedReservationID = Convert.ToInt32(row.Cells["ReservationID"].Value);

            // Try to grab WorkspaceID (hidden column)
            if (row.Cells["WorkspaceID"] != null && row.Cells["WorkspaceID"].Value != null)
                _selectedWorkspaceID = Convert.ToInt32(row.Cells["WorkspaceID"].Value);

            string status = row.Cells["Status"].Value?.ToString() ?? "Running";
            cmbResStatus.SelectedItem = status;
        }

        // ── Fill cmbMember ────────────────────────────────────
        private void LoadMembersCombo()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT MemberID, FullName FROM Member ORDER BY FullName", con))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cmbMember.DisplayMember = "FullName";
                        cmbMember.ValueMember   = "MemberID";
                        cmbMember.DataSource    = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading members: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Fill cmbWorkspace (Available only) ───────────────
        private void LoadWorkspacesCombo()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    string sql =
                        "SELECT WorkspaceID, " +
                        "       WorkspaceType + ' – ' + HubName AS DisplayName " +
                        "FROM   Workspace " +
                        "WHERE  Status = 'Available' " +
                        "ORDER  BY WorkspaceType";

                    using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cmbWorkspace.DisplayMember = "DisplayName";
                        cmbWorkspace.ValueMember   = "WorkspaceID";
                        cmbWorkspace.DataSource    = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading workspaces: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── btnReserve_Click ──────────────────────────────────
        private void btnReserve_Click(object sender, EventArgs e)
        {
            if (cmbMember.SelectedValue == null || cmbWorkspace.SelectedValue == null)
            {
                MessageBox.Show("Please select both a Member and a Workspace.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int hours;
            if (!int.TryParse(txtHours.Text.Trim(), out hours) || hours <= 0)
            {
                MessageBox.Show("Hours Reserved must be a positive whole number.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int memberID    = Convert.ToInt32(cmbMember.SelectedValue);
                int workspaceID = Convert.ToInt32(cmbWorkspace.SelectedValue);

                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    // 1. Insert reservation
                    string insertSql =
                        "INSERT INTO Reservation " +
                        "    (MemberID, WorkspaceID, ReservationDate, HoursReserved, Status) " +
                        "VALUES (@MemberID, @WorkspaceID, @ReservationDate, @HoursReserved, @Status)";

                    using (SqlCommand cmd = new SqlCommand(insertSql, con))
                    {
                        cmd.Parameters.AddWithValue("@MemberID",        memberID);
                        cmd.Parameters.AddWithValue("@WorkspaceID",     workspaceID);
                        cmd.Parameters.AddWithValue("@ReservationDate", dateTimePicker1.Value.Date);
                        cmd.Parameters.AddWithValue("@HoursReserved",   hours);
                        cmd.Parameters.AddWithValue("@Status",          cmbResStatus.SelectedItem.ToString());
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Mark workspace as Reserved
                    string updateSql =
                        "UPDATE Workspace SET Status = 'Reserved' WHERE WorkspaceID = @WorkspaceID";

                    using (SqlCommand cmd2 = new SqlCommand(updateSql, con))
                    {
                        cmd2.Parameters.AddWithValue("@WorkspaceID", workspaceID);
                        cmd2.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Reservation added successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtHours.Clear();
                cmbResStatus.SelectedIndex = 0;
                LoadMembersCombo();
                LoadWorkspacesCombo();
                LoadReservations();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── btnUpdateStatus_Click ─────────────────────────────
        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (_selectedReservationID <= 0)
            {
                MessageBox.Show("Please select a reservation from the grid first.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string newStatus = cmbResStatus.SelectedItem.ToString();

                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    string sql =
                        "UPDATE Reservation SET Status = @Status " +
                        "WHERE  ReservationID = @ReservationID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Status",        newStatus);
                        cmd.Parameters.AddWithValue("@ReservationID", _selectedReservationID);
                        cmd.ExecuteNonQuery();
                    }

                    // If marking finished, free the workspace back to Available
                    if (newStatus == "Finished" && _selectedWorkspaceID > 0)
                    {
                        string freeSql =
                            "UPDATE Workspace SET Status = 'Available' " +
                            "WHERE  WorkspaceID = @WorkspaceID";

                        using (SqlCommand cmd2 = new SqlCommand(freeSql, con))
                        {
                            cmd2.Parameters.AddWithValue("@WorkspaceID", _selectedWorkspaceID);
                            cmd2.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Reservation status updated successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadWorkspacesCombo();
                LoadReservations();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── btnDelete_Click ───────────────────────────────────
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedReservationID <= 0)
            {
                MessageBox.Show("Please select a reservation from the grid first.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show(
                "Are you sure you want to delete this reservation?\nThis action cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr != DialogResult.Yes) return;

            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    // Restore workspace to Available before deleting reservation
                    if (_selectedWorkspaceID > 0)
                    {
                        string freeSql =
                            "UPDATE Workspace SET Status = 'Available' " +
                            "WHERE  WorkspaceID = @WorkspaceID";

                        using (SqlCommand cmd = new SqlCommand(freeSql, con))
                        {
                            cmd.Parameters.AddWithValue("@WorkspaceID", _selectedWorkspaceID);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    string sql = "DELETE FROM Reservation WHERE ReservationID = @ReservationID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@ReservationID", _selectedReservationID);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Reservation deleted successfully!",
                    "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _selectedReservationID = 0;
                _selectedWorkspaceID   = 0;
                LoadMembersCombo();
                LoadWorkspacesCombo();
                LoadReservations();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── btnLoad_Click ────────────────────────────────────
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadReservations();
        }

        // ── LoadReservations (reusable helper) ───────────────
        private void LoadReservations()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    string sql =
                        "SELECT r.ReservationID, " +
                        "       r.WorkspaceID, " +          // hidden – needed for update/delete
                        "       m.FullName        AS Member, " +
                        "       w.WorkspaceType   AS [Workspace Type], " +
                        "       w.HubName         AS Hub, " +
                        "       r.ReservationDate AS [Date], " +
                        "       r.HoursReserved   AS Hours, " +
                        "       r.Status " +
                        "FROM   Reservation r " +
                        "JOIN   Member    m ON r.MemberID    = m.MemberID " +
                        "JOIN   Workspace w ON r.WorkspaceID = w.WorkspaceID " +
                        "ORDER  BY r.ReservationID DESC";

                    using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;

                        // Hide the WorkspaceID column from the user
                        if (dataGridView1.Columns["WorkspaceID"] != null)
                            dataGridView1.Columns["WorkspaceID"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading reservations: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
