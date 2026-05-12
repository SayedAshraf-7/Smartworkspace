// ================================================
// FILE: MembersForm.cs
// ================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SmartWorkspace
{
    public partial class MembersForm : Form
    {
        private int _selectedMemberID = 0;

        // ── Constructor ──────────────────────────────────────
        public MembersForm()
        {
            InitializeComponent();
            LoadMembers();
        }

        // ── Row selected → fill input fields ─────────────────
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            DataGridViewRow row = dataGridView1.SelectedRows[0];
            _selectedMemberID   = Convert.ToInt32(row.Cells["MemberID"].Value);
            txtName.Text        = row.Cells["FullName"].Value?.ToString() ?? "";
            txtDigitalID.Text   = row.Cells["DigitalID"].Value?.ToString() ?? "";
            txtAffiliation.Text = row.Cells["CorporateAffiliation"].Value?.ToString() ?? "";
        }

        // ── btnAdd_Click ─────────────────────────────────────
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Full Name is required.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    string sql =
                        "INSERT INTO Member (FullName, DigitalID, CorporateAffiliation) " +
                        "VALUES (@FullName, @DigitalID, @CorporateAffiliation)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@FullName",             txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@DigitalID",            txtDigitalID.Text.Trim());
                        cmd.Parameters.AddWithValue("@CorporateAffiliation", txtAffiliation.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Member added successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
                LoadMembers();
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
            if (_selectedMemberID <= 0)
            {
                MessageBox.Show("Please select a member from the grid first.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Full Name is required.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    string sql =
                        "UPDATE Member " +
                        "SET    FullName             = @FullName, " +
                        "       DigitalID            = @DigitalID, " +
                        "       CorporateAffiliation = @CorporateAffiliation " +
                        "WHERE  MemberID = @MemberID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@FullName",             txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@DigitalID",            txtDigitalID.Text.Trim());
                        cmd.Parameters.AddWithValue("@CorporateAffiliation", txtAffiliation.Text.Trim());
                        cmd.Parameters.AddWithValue("@MemberID",             _selectedMemberID);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Member updated successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
                LoadMembers();
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
            if (_selectedMemberID <= 0)
            {
                MessageBox.Show("Please select a member from the grid first.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show(
                "Are you sure you want to delete this member?\nThis action cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr != DialogResult.Yes) return;

            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    string sql = "DELETE FROM Member WHERE MemberID = @MemberID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", _selectedMemberID);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Member deleted successfully!",
                    "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
                LoadMembers();
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
            LoadMembers();
        }

        // ── LoadMembers (reusable helper) ────────────────────
        private void LoadMembers()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    string sql =
                        "SELECT MemberID, FullName, DigitalID, " +
                        "       CorporateAffiliation, TotalReservedHours " +
                        "FROM   Member " +
                        "ORDER  BY FullName";

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
                MessageBox.Show("Error loading members: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── ClearFields (helper) ─────────────────────────────
        private void ClearFields()
        {
            txtName.Clear();
            txtDigitalID.Clear();
            txtAffiliation.Clear();
            _selectedMemberID = 0;
            txtName.Focus();
        }
    }
}
