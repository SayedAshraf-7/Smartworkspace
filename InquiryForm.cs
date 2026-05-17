// ================================================
// FILE: InquiryForm.cs
// ================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SmartWorkspace
{
    public partial class InquiryForm : Form
    {
        // ── Constructor ──────────────────────────────────────
        public InquiryForm()
        {
            InitializeComponent();
        }

        // ── Helper: run a SELECT and show in grid ─────────────
        private void RunQuery(string sql, string label, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DB.ConnectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        if (parameters != null)
                            cmd.Parameters.AddRange(parameters);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }

                lblResult.Text = "Showing: " + label;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Query 1: Most popular workspace type ─────────────
        private void btnPopular_Click(object sender, EventArgs e)
        {
            string sql =
                "SELECT   w.type           AS [Workspace Type], " +
                "         COUNT(r.id)      AS [Total Reservations] " +
                "FROM     Workspaces w " +
                "LEFT JOIN Reservations r ON w.id = r.workspace_id " +
                "GROUP BY w.type " +
                "ORDER BY [Total Reservations] DESC";

            RunQuery(sql, "Most Popular Workspace Types");
        }

        // ── Query 2: Members with no reservations last month ─
        private void btnNoReservation_Click(object sender, EventArgs e)
        {
            string sql =
                "SELECT m.id                    AS [Member ID], " +
                "       m.name                  AS [Full Name], " +
                "       m.digital_identification AS [Digital ID], " +
                "       m.corporate_affiliation AS [Corp. Affiliation] " +
                "FROM   Members m " +
                "WHERE  m.id NOT IN ( " +
                "           SELECT DISTINCT member_id FROM Reservations " +
                "           WHERE  start_date >= DATEADD(MONTH, -1, GETDATE()) " +
                "       )";

            RunQuery(sql, "Members With No Reservations in the Last Month");
        }

        // ── Query 3: Hubs with NO reservations last month ─────
        private void btnNoReservationHubs_Click(object sender, EventArgs e)
        {
            string sql =
                "SELECT DISTINCT h.name AS [Hub Name] " +
                "FROM   Hubs h " +
                "JOIN   Workspaces w ON w.hub_id = h.id " +
                "WHERE  w.id NOT IN ( " +
                "           SELECT r.workspace_id " +
                "           FROM   Reservations r " +
                "           WHERE  r.start_date >= DATEADD(MONTH, -1, GETDATE()) " +
                "       ) " +
                "ORDER BY h.name";

            RunQuery(sql, "Hubs With No Reservations in the Last Month");
        }

        // ── Query 4: Members who reserved most VARIETY of equipment last month
        private void btnMostVariedEquipment_Click(object sender, EventArgs e)
        {
            string sql =
                "SELECT   m.name                         AS [Member], " +
                "         COUNT(DISTINCT re.equipment_id) AS [Equipment Variety] " +
                "FROM     Members m " +
                "JOIN     Reservations r         ON r.member_id      = m.id " +
                "JOIN     Reserved_equipments re ON re.reservation_id = r.id " +
                "WHERE    r.start_date >= DATEADD(MONTH, -1, GETDATE()) " +
                "GROUP BY m.id, m.name " +
                "ORDER BY [Equipment Variety] DESC";

            RunQuery(sql, "Members With Most Equipment Variety in the Last Month");
        }

        // ── Query 5: Equipment used per hub last month ────────
        private void btnEquipmentByHub_Click(object sender, EventArgs e)
        {
            string sql =
                "SELECT   h.name                  AS [Hub], " +
                "         e.type                  AS [Equipment Type], " +
                "         COUNT(re.reservation_id) AS [Times Used Last Month] " +
                "FROM     Equipments e " +
                "JOIN     Reserved_equipments re ON re.equipment_id    = e.id " +
                "JOIN     Reservations r         ON r.id               = re.reservation_id " +
                "JOIN     Workspaces w           ON r.workspace_id     = w.id " +
                "JOIN     Hubs h                 ON w.hub_id           = h.id " +
                "WHERE    r.start_date >= DATEADD(MONTH, -1, GETDATE()) " +
                "GROUP BY h.name, e.type " +
                "ORDER BY h.name, [Times Used Last Month] DESC";

            RunQuery(sql, "Equipment Used per Hub in the Last Month");
        }

        // ── Query 6: Per-member profile + total hours reserved
        private void btnMemberHours_Click(object sender, EventArgs e)
        {
            string sql =
                "SELECT   m.id                    AS [Member ID], " +
                "         m.name                  AS [Full Name], " +
                "         m.digital_identification AS [Digital ID], " +
                "         m.corporate_affiliation AS [Corp. Affiliation], " +
                "         ISNULL(SUM(CASE " +
                "             WHEN r.pricing_type = 'hourly' THEN r.duration " +
                "             WHEN r.pricing_type = 'daily'  THEN r.duration * 24 " +
                "             ELSE 0 END), 0)     AS [Total Hours Reserved] " +
                "FROM     Members m " +
                "LEFT JOIN Reservations r ON r.member_id = m.id " +
                "GROUP BY m.id, m.name, m.digital_identification, m.corporate_affiliation " +
                "ORDER BY m.name";

            RunQuery(sql, "Members – Profile and Total Hours Reserved");
        }
    }
}
