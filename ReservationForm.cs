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
        private long      _selectedReservationID  = 0;
        private long      _selectedWorkspaceID    = 0;
        private DataTable _equipmentTable         = new DataTable();

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
            LoadEquipmentList();
            LoadReservations();
            RecalculateDuration_Event(this, EventArgs.Empty);
        }

        // ── Auto-calculate Duration from Start/End + Pricing ─
        private void RecalculateDuration_Event(object sender, EventArgs e)
        {
            if (cmbPricingType == null || cmbPricingType.SelectedItem == null) return;

            TimeSpan span = dtpEnd.Value - dtpStart.Value;
            if (span.TotalSeconds <= 0)
            {
                txtDuration.Text = "0";
                return;
            }

            long duration = cmbPricingType.SelectedItem.ToString() == "daily"
                ? (long)Math.Ceiling(span.TotalDays)
                : (long)Math.Ceiling(span.TotalHours);

            txtDuration.Text = duration.ToString();
        }

        // ── Row selected → sync Status combo ─────────────────
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            DataGridViewRow row = dataGridView1.SelectedRows[0];
            _selectedReservationID = Convert.ToInt64(row.Cells["id"].Value);

            if (row.Cells["workspace_id"] != null && row.Cells["workspace_id"].Value != null)
                _selectedWorkspaceID = Convert.ToInt64(row.Cells["workspace_id"].Value);

            string status = row.Cells["Status"].Value?.ToString() ?? "running";
            if (cmbResStatus.Items.Contains(status))
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
                        "SELECT id, name FROM Members ORDER BY name", con))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cmbMember.DisplayMember = "name";
                        cmbMember.ValueMember   = "id";
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
                        "SELECT w.id, w.type + ' – ' + h.name AS DisplayName " +
                        "FROM   Workspaces w " +
                        "JOIN   Hubs h ON w.hub_id = h.id " +
                        "WHERE  w.status = 'available' " +
                        "ORDER  BY w.type";

                    using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cmbWorkspace.DisplayMember = "DisplayName";
                        cmbWorkspace.ValueMember   = "id";
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

        // ── Fill clbEquipment ──────────────────────────────────
        private void LoadEquipmentList()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    string sql =
                        "SELECT id, type + ' #' + CAST(id AS NVARCHAR(20)) AS DisplayName " +
                        "FROM   Equipments " +
                        "ORDER  BY type, id";

                    using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                    {
                        _equipmentTable = new DataTable();
                        da.Fill(_equipmentTable);
                    }
                }

                clbEquipment.Items.Clear();
                foreach (DataRow row in _equipmentTable.Rows)
                    clbEquipment.Items.Add(row["DisplayName"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading equipment: " + ex.Message,
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

            long duration;
            if (!long.TryParse(txtDuration.Text.Trim(), out duration) || duration <= 0)
            {
                MessageBox.Show("Duration must be a positive whole number.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpEnd.Value <= dtpStart.Value)
            {
                MessageBox.Show("End must be after Start.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                long memberID    = Convert.ToInt64(cmbMember.SelectedValue);
                long workspaceID = Convert.ToInt64(cmbWorkspace.SelectedValue);

                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    // 1. Insert reservation – capture new ID via OUTPUT
                    string insertSql =
                        "INSERT INTO Reservations " +
                        "    (pricing_type, duration, start_date, end_date, member_id, workspace_id, status) " +
                        "OUTPUT INSERTED.id " +
                        "VALUES (@pricing_type, @duration, @start_date, @end_date, @member_id, @workspace_id, @status)";

                    long newReservationID;
                    using (SqlCommand cmd = new SqlCommand(insertSql, con))
                    {
                        cmd.Parameters.AddWithValue("@pricing_type", cmbPricingType.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@duration",     duration);
                        cmd.Parameters.AddWithValue("@start_date",   dtpStart.Value);
                        cmd.Parameters.AddWithValue("@end_date",     dtpEnd.Value);
                        cmd.Parameters.AddWithValue("@member_id",    memberID);
                        cmd.Parameters.AddWithValue("@workspace_id", workspaceID);
                        cmd.Parameters.AddWithValue("@status",       cmbResStatus.SelectedItem.ToString());
                        newReservationID = Convert.ToInt64(cmd.ExecuteScalar());
                    }

                    // 2. Link any checked equipment (optional)
                    foreach (int idx in clbEquipment.CheckedIndices)
                    {
                        long equipID = Convert.ToInt64(_equipmentTable.Rows[idx]["id"]);
                        string linkSql =
                            "INSERT INTO Reserved_equipments (equipment_id, reservation_id, duration) " +
                            "VALUES (@equipment_id, @reservation_id, @duration)";

                        using (SqlCommand linkCmd = new SqlCommand(linkSql, con))
                        {
                            linkCmd.Parameters.AddWithValue("@equipment_id",   equipID);
                            linkCmd.Parameters.AddWithValue("@reservation_id", newReservationID);
                            linkCmd.Parameters.AddWithValue("@duration",       duration);
                            linkCmd.ExecuteNonQuery();
                        }
                    }

                    // 3. Mark workspace as reserved
                    string updateSql =
                        "UPDATE Workspaces SET status = 'reserved' WHERE id = @id";

                    using (SqlCommand cmd2 = new SqlCommand(updateSql, con))
                    {
                        cmd2.Parameters.AddWithValue("@id", workspaceID);
                        cmd2.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Reservation added successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                cmbResStatus.SelectedIndex = 0;
                RecalculateDuration_Event(this, EventArgs.Empty);
                for (int i = 0; i < clbEquipment.Items.Count; i++)
                    clbEquipment.SetItemChecked(i, false);
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
                        "UPDATE Reservations SET status = @status " +
                        "WHERE  id = @id";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@status", newStatus);
                        cmd.Parameters.AddWithValue("@id",     _selectedReservationID);
                        cmd.ExecuteNonQuery();
                    }

                    // If marking finished or cancelled, free the workspace back to available
                    if ((newStatus == "finished" || newStatus == "cancelled") && _selectedWorkspaceID > 0)
                    {
                        string freeSql =
                            "UPDATE Workspaces SET status = 'available' " +
                            "WHERE  id = @id";

                        using (SqlCommand cmd2 = new SqlCommand(freeSql, con))
                        {
                            cmd2.Parameters.AddWithValue("@id", _selectedWorkspaceID);
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

                    // Restore workspace to available before deleting reservation
                    if (_selectedWorkspaceID > 0)
                    {
                        string freeSql =
                            "UPDATE Workspaces SET status = 'available' " +
                            "WHERE  id = @id";

                        using (SqlCommand cmd = new SqlCommand(freeSql, con))
                        {
                            cmd.Parameters.AddWithValue("@id", _selectedWorkspaceID);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    string sql = "DELETE FROM Reservations WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", _selectedReservationID);
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
                        "SELECT r.id, " +
                        "       r.workspace_id, " +              // hidden – needed for update/delete
                        "       m.name           AS Member, " +
                        "       w.type           AS [Workspace Type], " +
                        "       h.name           AS Hub, " +
                        "       r.start_date     AS [Start], " +
                        "       r.end_date       AS [End], " +
                        "       r.duration       AS Duration, " +
                        "       r.pricing_type   AS Pricing, " +
                        "       r.status         AS Status, " +
                        "       ISNULL((" +
                        "           SELECT STRING_AGG(e.type, ', ') " +
                        "           FROM   Reserved_equipments re " +
                        "           JOIN   Equipments e ON re.equipment_id = e.id " +
                        "           WHERE  re.reservation_id = r.id" +
                        "       ), 'None') AS Equipment " +
                        "FROM   Reservations r " +
                        "JOIN   Members m    ON r.member_id    = m.id " +
                        "JOIN   Workspaces w ON r.workspace_id = w.id " +
                        "JOIN   Hubs h       ON w.hub_id       = h.id " +
                        "ORDER  BY r.id DESC";

                    using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;

                        if (dataGridView1.Columns["workspace_id"] != null)
                            dataGridView1.Columns["workspace_id"].Visible = false;
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
