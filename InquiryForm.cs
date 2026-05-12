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
                "SELECT   w.WorkspaceType        AS [Workspace Type], " +
                "         COUNT(r.ReservationID) AS [Total Reservations] " +
                "FROM     Workspace w " +
                "LEFT JOIN Reservation r ON w.WorkspaceID = r.WorkspaceID " +
                "GROUP BY w.WorkspaceType " +
                "ORDER BY [Total Reservations] DESC";

            RunQuery(sql, "Most Popular Workspace Types");
        }

        // ── Query 2: Members with no reservations ────────────
        private void btnNoReservation_Click(object sender, EventArgs e)
        {
            string sql =
                "SELECT m.MemberID, " +
                "       m.FullName             AS [Full Name], " +
                "       m.DigitalID            AS [Digital ID], " +
                "       m.CorporateAffiliation AS [Corp. Affiliation] " +
                "FROM   Member m " +
                "WHERE  m.MemberID NOT IN " +
                "       (SELECT DISTINCT MemberID FROM Reservation)";

            RunQuery(sql, "Members With No Reservations");
        }

        // ── Query 3: Hubs with NO reservations last month ─────
        private void btnNoReservationHubs_Click(object sender, EventArgs e)
        {
            string sql =
                "SELECT DISTINCT w.HubName AS [Hub Name] " +
                "FROM   Workspace w " +
                "WHERE  w.WorkspaceID NOT IN ( " +
                "           SELECT r.WorkspaceID " +
                "           FROM   Reservation r " +
                "           WHERE  r.ReservationDate >= DATEADD(MONTH, -1, GETDATE()) " +
                "       ) " +
                "ORDER BY w.HubName";

            RunQuery(sql, "Hubs With No Reservations in the Last Month");
        }

        // ── Query 4: Members who reserved most VARIETY of equipment
        private void btnMostVariedEquipment_Click(object sender, EventArgs e)
        {
            string sql =
                "SELECT   m.FullName                     AS [Member], " +
                "         COUNT(DISTINCT re.EquipmentID) AS [Equipment Variety] " +
                "FROM     Member m " +
                "JOIN     Reservation r           ON r.MemberID      = m.MemberID " +
                "JOIN     ReservationEquipment re  ON re.ReservationID = r.ReservationID " +
                "GROUP BY m.MemberID, m.FullName " +
                "ORDER BY [Equipment Variety] DESC";

            RunQuery(sql, "Members With Most Equipment Variety in Reservations");
        }

        // ── Query 5: Equipment used per hub/urban last month ──
        private void btnEquipmentByHub_Click(object sender, EventArgs e)
        {
            string sql =
                "SELECT   e.HubName               AS [Hub / Urban], " +
                "         e.EquipmentName          AS [Equipment], " +
                "         e.EquipmentType          AS [Type], " +
                "         COUNT(re.ReservationID)  AS [Times Used Last Month] " +
                "FROM     Equipment e " +
                "JOIN     ReservationEquipment re ON re.EquipmentID    = e.EquipmentID " +
                "JOIN     Reservation r           ON r.ReservationID   = re.ReservationID " +
                "WHERE    r.ReservationDate >= DATEADD(MONTH, -1, GETDATE()) " +
                "GROUP BY e.HubName, e.EquipmentID, e.EquipmentName, e.EquipmentType " +
                "ORDER BY e.HubName, [Times Used Last Month] DESC";

            RunQuery(sql, "Equipment Used per Hub in the Last Month");
        }
    }
}
