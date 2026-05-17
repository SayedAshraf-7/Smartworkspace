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
        private long _selectedWorkspaceID = 0;

        // ── Constructor ──────────────────────────────────────
        public WorkspaceForm()
        {
            InitializeComponent();
        }

        // ── Form Load ────────────────────────────────────────
        private void WorkspaceForm_Load(object sender, EventArgs e)
        {
            LoadHubs();
            LoadFilterOptions();
            LoadWorkspaces();
        }

        // ── Row selected → fill input fields ─────────────────
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            DataGridViewRow row = dataGridView1.SelectedRows[0];
            _selectedWorkspaceID = Convert.ToInt64(row.Cells["id"].Value);

            string type = row.Cells["type"].Value?.ToString() ?? "";
            if (cmbType.Items.Contains(type)) cmbType.SelectedItem = type;

            long hubId = Convert.ToInt64(row.Cells["hub_id"].Value);
            cmbHub.SelectedValue = hubId;

            txtHourlyRate.Text = row.Cells["hourly_rate"].Value?.ToString() ?? "";
            txtDailyRate.Text  = row.Cells["daily_rate"].Value?.ToString() ?? "";

            string status = row.Cells["status"].Value?.ToString() ?? "available";
            if (cmbStatus.Items.Contains(status)) cmbStatus.SelectedItem = status;
        }

        // ── Populate hub combo (form-input combo) ─────────────
        private void LoadHubs()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT id, name FROM Hubs ORDER BY name", con))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cmbHub.DisplayMember = "name";
                        cmbHub.ValueMember   = "id";
                        cmbHub.DataSource    = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading hubs: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Populate filter combos ────────────────────────────
        private void LoadFilterOptions()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    // Hub filter — store name in the items, look up id at filter time
                    using (SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT name FROM Hubs ORDER BY name", con))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cmbFilterHub.Items.Clear();
                        cmbFilterHub.Items.Add("All");
                        foreach (DataRow r in dt.Rows)
                            cmbFilterHub.Items.Add(r["name"].ToString());
                        cmbFilterHub.SelectedIndex = 0;
                    }

                    // Type filter (use the enum values)
                    cmbFilterType.Items.Clear();
                    cmbFilterType.Items.Add("All");
                    cmbFilterType.Items.Add("private_office");
                    cmbFilterType.Items.Add("open_desk");
                    cmbFilterType.Items.Add("meeting_pod");
                    cmbFilterType.SelectedIndex = 0;
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
            if (cmbType.SelectedItem == null || cmbHub.SelectedValue == null ||
                string.IsNullOrWhiteSpace(txtHourlyRate.Text) ||
                string.IsNullOrWhiteSpace(txtDailyRate.Text))
            {
                MessageBox.Show("Type, Hub, Hourly Rate and Daily Rate are required.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            long hourlyRate, dailyRate;
            if (!long.TryParse(txtHourlyRate.Text.Trim(), out hourlyRate) || hourlyRate < 0 ||
                !long.TryParse(txtDailyRate.Text.Trim(),  out dailyRate)  || dailyRate  < 0)
            {
                MessageBox.Show("Hourly and Daily Rate must be valid non-negative numbers.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                long hubId = Convert.ToInt64(cmbHub.SelectedValue);

                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    string sql =
                        "INSERT INTO Workspaces (type, hub_id, hourly_rate, daily_rate, status) " +
                        "VALUES (@type, @hub_id, @hourly_rate, @daily_rate, @status)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@type",        cmbType.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@hub_id",      hubId);
                        cmd.Parameters.AddWithValue("@hourly_rate", hourlyRate);
                        cmd.Parameters.AddWithValue("@daily_rate",  dailyRate);
                        cmd.Parameters.AddWithValue("@status",      cmbStatus.SelectedItem.ToString());
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

            long hourlyRate, dailyRate;
            if (!long.TryParse(txtHourlyRate.Text.Trim(), out hourlyRate) || hourlyRate < 0 ||
                !long.TryParse(txtDailyRate.Text.Trim(),  out dailyRate)  || dailyRate  < 0)
            {
                MessageBox.Show("Hourly and Daily Rate must be valid non-negative numbers.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                long hubId = Convert.ToInt64(cmbHub.SelectedValue);

                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    string sql =
                        "UPDATE Workspaces " +
                        "SET    type        = @type, " +
                        "       hub_id      = @hub_id, " +
                        "       hourly_rate = @hourly_rate, " +
                        "       daily_rate  = @daily_rate, " +
                        "       status      = @status " +
                        "WHERE  id = @id";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@type",        cmbType.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@hub_id",      hubId);
                        cmd.Parameters.AddWithValue("@hourly_rate", hourlyRate);
                        cmd.Parameters.AddWithValue("@daily_rate",  dailyRate);
                        cmd.Parameters.AddWithValue("@status",      cmbStatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@id",          _selectedWorkspaceID);
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

                    string sql = "DELETE FROM Workspaces WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", _selectedWorkspaceID);
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
                        where += " AND w.status = @status";

                    if (cmbFilterHub.SelectedIndex > 0)
                        where += " AND h.name = @hub_name";

                    if (cmbFilterType.SelectedIndex > 0)
                        where += " AND w.type = @type";

                    string sql =
                        "SELECT w.id, w.type, w.hub_id, h.name AS hub, w.hourly_rate, w.daily_rate, w.status " +
                        "FROM   Workspaces w " +
                        "JOIN   Hubs h ON w.hub_id = h.id " + where +
                        " ORDER BY w.type";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        if (cmbFilterStatus.SelectedIndex > 0)
                            cmd.Parameters.AddWithValue("@status", cmbFilterStatus.SelectedItem.ToString());

                        if (cmbFilterHub.SelectedIndex > 0)
                            cmd.Parameters.AddWithValue("@hub_name", cmbFilterHub.SelectedItem.ToString());

                        if (cmbFilterType.SelectedIndex > 0)
                            cmd.Parameters.AddWithValue("@type", cmbFilterType.SelectedItem.ToString());

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;
                            if (dataGridView1.Columns["hub_id"] != null)
                                dataGridView1.Columns["hub_id"].Visible = false;
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
                        "SELECT w.id, w.type, w.hub_id, h.name AS hub, w.hourly_rate, w.daily_rate, w.status " +
                        "FROM   Workspaces w " +
                        "JOIN   Hubs h ON w.hub_id = h.id " +
                        "ORDER  BY w.type";

                    using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                        if (dataGridView1.Columns["hub_id"] != null)
                            dataGridView1.Columns["hub_id"].Visible = false;
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
            if (cmbType.Items.Count > 0) cmbType.SelectedIndex = 0;
            if (cmbHub.Items.Count > 0)  cmbHub.SelectedIndex  = 0;
            txtHourlyRate.Clear();
            txtDailyRate.Clear();
            cmbStatus.SelectedIndex = 0;
            _selectedWorkspaceID = 0;
        }
    }
}
