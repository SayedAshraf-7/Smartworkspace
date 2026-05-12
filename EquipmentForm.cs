// ================================================
// FILE: EquipmentForm.cs
// ================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SmartWorkspace
{
    public partial class EquipmentForm : Form
    {
        private int _selectedEquipmentID = 0;

        // ── Constructor ──────────────────────────────────────
        public EquipmentForm()
        {
            InitializeComponent();
            LoadEquipment();
        }

        // ── Row selected → fill input fields ─────────────────
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            DataGridViewRow row = dataGridView1.SelectedRows[0];
            _selectedEquipmentID = Convert.ToInt32(row.Cells["EquipmentID"].Value);
            txtName.Text = row.Cells["EquipmentName"].Value?.ToString() ?? "";
            txtType.Text = row.Cells["EquipmentType"].Value?.ToString() ?? "";
            txtHub.Text  = row.Cells["HubName"].Value?.ToString() ?? "";
        }

        // ── btnAdd_Click ─────────────────────────────────────
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtType.Text) ||
                string.IsNullOrWhiteSpace(txtHub.Text))
            {
                MessageBox.Show("Equipment Name, Type, and Hub are all required.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    string sql =
                        "INSERT INTO Equipment (EquipmentName, EquipmentType, HubName) " +
                        "VALUES (@EquipmentName, @EquipmentType, @HubName)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@EquipmentName", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@EquipmentType", txtType.Text.Trim());
                        cmd.Parameters.AddWithValue("@HubName",       txtHub.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Equipment added successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
                LoadEquipment();
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
            if (_selectedEquipmentID <= 0)
            {
                MessageBox.Show("Please select an equipment item from the grid first.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtType.Text) ||
                string.IsNullOrWhiteSpace(txtHub.Text))
            {
                MessageBox.Show("Equipment Name, Type, and Hub are all required.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    string sql =
                        "UPDATE Equipment " +
                        "SET    EquipmentName = @EquipmentName, " +
                        "       EquipmentType = @EquipmentType, " +
                        "       HubName       = @HubName " +
                        "WHERE  EquipmentID = @EquipmentID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@EquipmentName", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@EquipmentType", txtType.Text.Trim());
                        cmd.Parameters.AddWithValue("@HubName",       txtHub.Text.Trim());
                        cmd.Parameters.AddWithValue("@EquipmentID",   _selectedEquipmentID);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Equipment updated successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
                LoadEquipment();
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
            if (_selectedEquipmentID <= 0)
            {
                MessageBox.Show("Please select an equipment item from the grid first.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show(
                "Are you sure you want to delete this equipment item?\nThis action cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr != DialogResult.Yes) return;

            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    string sql = "DELETE FROM Equipment WHERE EquipmentID = @EquipmentID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@EquipmentID", _selectedEquipmentID);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Equipment deleted successfully!",
                    "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
                LoadEquipment();
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
            LoadEquipment();
        }

        // ── LoadEquipment (reusable helper) ──────────────────
        private void LoadEquipment()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    string sql =
                        "SELECT EquipmentID, EquipmentName, EquipmentType, HubName " +
                        "FROM   Equipment " +
                        "ORDER  BY HubName, EquipmentName";

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
                MessageBox.Show("Error loading equipment: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── ClearFields ───────────────────────────────────────
        private void ClearFields()
        {
            txtName.Clear();
            txtType.Clear();
            txtHub.Clear();
            _selectedEquipmentID = 0;
            txtName.Focus();
        }
    }
}
