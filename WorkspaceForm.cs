// ================================================
// FILE: WorkspaceForm.cs
// ================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SmartWorkspace
{
    public partial class WorkspaceForm : Form
    {
        private int _selectedWorkspaceID = 0;

        // ── Constructor ──────────────────────────────────────
        public WorkspaceForm()
        {
            InitializeComponent();
        }

        // ── Form Load ────────────────────────────────────────
        private void WorkspaceForm_Load(object sender, EventArgs e)
        {
            LoadFilterOptions();
            LoadWorkspaces();
        }

        // ── Row selected → fill input fields ─────────────────
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            DataGridViewRow row = dataGridView1.SelectedRows[0];
            _selectedWorkspaceID = Convert.ToInt32(row.Cells["WorkspaceID"].Value);
            txtType.Text  = row.Cells["WorkspaceType"].Value?.ToString() ?? "";
            txtHub.Text   = row.Cells["HubName"].Value?.ToString() ?? "";
            txtPrice.Text = row.Cells["PricePerHour"].Value?.ToString() ?? "";

            string status = row.Cells["Status"].Value?.ToString() ?? "Available";
            cmbStatus.SelectedItem = status;
        }

        // ── Populate filter combos ────────────────────────────
        private void LoadFilterOptions()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    // Hub filter
                    using (SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT DISTINCT HubName FROM Workspace ORDER BY HubName", con))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cmbFilterHub.Items.Clear();
                        cmbFilterHub.Items.Add("All");
                        foreach (DataRow r in dt.Rows)
                            cmbFilterHub.Items.Add(r["HubName"].ToString());
                        cmbFilterHub.SelectedIndex = 0;
                    }

                    // Type filter
                    using (SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT DISTINCT WorkspaceType FROM Workspace ORDER BY WorkspaceType", con))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cmbFilterType.Items.Clear();
                        cmbFilterType.Items.Add("All");
                        foreach (DataRow r in dt.Rows)
                            cmbFilterType.Items.Add(r["WorkspaceType"].ToString());
                        cmbFilterType.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading filter options: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── btnAdd_Click ─────────────────────────────────────
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtType.Text) ||
                string.IsNullOrWhiteSpace(txtHub.Text)  ||
                string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Type, Hub Name, and Price are required.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal price;
            if (!decimal.TryParse(txtPrice.Text.Trim(), out price) || price < 0)
            {
                MessageBox.Show("Price must be a valid positive number.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    string sql =
                        "INSERT INTO Workspace (WorkspaceType, HubName, PricePerHour, Status) " +
                        "VALUES (@WorkspaceType, @HubName, @PricePerHour, @Status)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@WorkspaceType", txtType.Text.Trim());
                        cmd.Parameters.AddWithValue("@HubName",       txtHub.Text.Trim());
                        cmd.Parameters.AddWithValue("@PricePerHour",  price);
                        cmd.Parameters.AddWithValue("@Status",        cmbStatus.SelectedItem.ToString());
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Workspace added successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
                LoadFilterOptions();
                LoadWorkspaces();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── btnUpdate_Click ───────────────────────────────────
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedWorkspaceID <= 0)
            {
                MessageBox.Show("Please select a workspace from the grid first.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal price;
            if (!decimal.TryParse(txtPrice.Text.Trim(), out price) || price < 0)
            {
                MessageBox.Show("Price must be a valid positive number.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    string sql =
                        "UPDATE Workspace " +
                        "SET    WorkspaceType = @WorkspaceType, " +
                        "       HubName       = @HubName, " +
                        "       PricePerHour  = @PricePerHour, " +
                        "       Status        = @Status " +
                        "WHERE  WorkspaceID = @WorkspaceID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@WorkspaceType", txtType.Text.Trim());
                        cmd.Parameters.AddWithValue("@HubName",       txtHub.Text.Trim());
                        cmd.Parameters.AddWithValue("@PricePerHour",  price);
                        cmd.Parameters.AddWithValue("@Status",        cmbStatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@WorkspaceID",   _selectedWorkspaceID);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Workspace updated successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
                LoadFilterOptions();
                LoadWorkspaces();
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
            if (_selectedWorkspaceID <= 0)
            {
                MessageBox.Show("Please select a workspace from the grid first.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show(
                "Are you sure you want to delete this workspace?\nThis action cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr != DialogResult.Yes) return;

            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    string sql = "DELETE FROM Workspace WHERE WorkspaceID = @WorkspaceID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@WorkspaceID", _selectedWorkspaceID);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Workspace deleted successfully!",
                    "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
                LoadFilterOptions();
                LoadWorkspaces();
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
            cmbFilterStatus.SelectedIndex = 0;
            cmbFilterHub.SelectedIndex    = 0;
            cmbFilterType.SelectedIndex   = 0;
            LoadWorkspaces();
        }

        // ── btnFilter_Click ───────────────────────────────────
        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    string where = "WHERE 1=1";

                    if (cmbFilterStatus.SelectedIndex > 0)
                        where += " AND Status = @Status";

                    if (cmbFilterHub.SelectedIndex > 0)
                        where += " AND HubName = @HubName";

                    if (cmbFilterType.SelectedIndex > 0)
                        where += " AND WorkspaceType = @WorkspaceType";

                    string sql =
                        "SELECT WorkspaceID, WorkspaceType, HubName, PricePerHour, Status " +
                        "FROM   Workspace " + where +
                        " ORDER BY WorkspaceType";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        if (cmbFilterStatus.SelectedIndex > 0)
                            cmd.Parameters.AddWithValue("@Status", cmbFilterStatus.SelectedItem.ToString());

                        if (cmbFilterHub.SelectedIndex > 0)
                            cmd.Parameters.AddWithValue("@HubName", cmbFilterHub.SelectedItem.ToString());

                        if (cmbFilterType.SelectedIndex > 0)
                            cmd.Parameters.AddWithValue("@WorkspaceType", cmbFilterType.SelectedItem.ToString());

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error applying filter: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── btnClearFilter_Click ──────────────────────────────
        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            cmbFilterStatus.SelectedIndex = 0;
            cmbFilterHub.SelectedIndex    = 0;
            cmbFilterType.SelectedIndex   = 0;
            LoadWorkspaces();
        }

        // ── LoadWorkspaces (reusable helper) ─────────────────
        private void LoadWorkspaces()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    string sql =
                        "SELECT WorkspaceID, WorkspaceType, HubName, PricePerHour, Status " +
                        "FROM   Workspace " +
                        "ORDER  BY WorkspaceType";

                    using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading workspaces: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── ClearFields ───────────────────────────────────────
        private void ClearFields()
        {
            txtType.Clear();
            txtHub.Clear();
            txtPrice.Clear();
            cmbStatus.SelectedIndex = 0;
            _selectedWorkspaceID = 0;
            txtType.Focus();
        }
    }
}
