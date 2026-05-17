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
        private long _selectedEquipmentID = 0;

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
            _selectedEquipmentID = Convert.ToInt64(row.Cells["id"].Value);

            string type = row.Cells["type"].Value?.ToString() ?? "";
            if (cmbType.Items.Contains(type)) cmbType.SelectedItem = type;
        }

        // ── btnAdd_Click ─────────────────────────────────────
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbType.SelectedItem == null)
            {
                MessageBox.Show("Equipment Type is required.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    string sql = "INSERT INTO Equipments (type) VALUES (@type)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@type", cmbType.SelectedItem.ToString());
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

            if (cmbType.SelectedItem == null)
            {
                MessageBox.Show("Equipment Type is required.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    string sql = "UPDATE Equipments SET type = @type WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@type", cmbType.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@id",   _selectedEquipmentID);
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

                    string sql = "DELETE FROM Equipments WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", _selectedEquipmentID);
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
                        "SELECT id, type " +
                        "FROM   Equipments " +
                        "ORDER  BY type, id";

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
            if (cmbType.Items.Count > 0) cmbType.SelectedIndex = 0;
            _selectedEquipmentID = 0;
        }
    }
}
