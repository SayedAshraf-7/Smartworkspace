// ================================================
// FILE: Form1.cs
// ================================================
using System;
using System.Windows.Forms;

namespace SmartWorkspace
{
    public partial class Form1 : Form
    {
        // ── Constructor ──────────────────────────────────────
        public Form1()
        {
            InitializeComponent();
        }

        // ── Button: Members ──────────────────────────────────
        private void btnMembers_Click(object sender, EventArgs e)
        {
            new MembersForm().Show();
        }

        // ── Button: Workspaces ───────────────────────────────
        private void btnWorkspaces_Click(object sender, EventArgs e)
        {
            new WorkspaceForm().Show();
        }

        // ── Button: Reservations ─────────────────────────────
        private void btnReservations_Click(object sender, EventArgs e)
        {
            new ReservationForm().Show();
        }

        // ── Button: Equipment ────────────────────────────────
        private void btnEquipment_Click(object sender, EventArgs e)
        {
            new EquipmentForm().Show();
        }

        // ── Button: Inquiries ────────────────────────────────
        private void btnInquiries_Click(object sender, EventArgs e)
        {
            new InquiryForm().Show();
        }
    }
}
