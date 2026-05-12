// ================================================
// FILE: Form1.Designer.cs
// ================================================
namespace SmartWorkspace
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle         = new System.Windows.Forms.Label();
            this.btnMembers       = new System.Windows.Forms.Button();
            this.btnWorkspaces    = new System.Windows.Forms.Button();
            this.btnReservations  = new System.Windows.Forms.Button();
            this.btnEquipment     = new System.Windows.Forms.Button();
            this.btnInquiries     = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // ── lblTitle ─────────────────────────────────────
            this.lblTitle.AutoSize   = false;
            this.lblTitle.Text       = "SmartWorkspace";
            this.lblTitle.Font       = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.TextAlign  = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Location   = new System.Drawing.Point(50, 25);
            this.lblTitle.Size       = new System.Drawing.Size(400, 45);

            // ── btnMembers ────────────────────────────────────
            this.btnMembers.Text      = "👤  Members";
            this.btnMembers.Font      = new System.Drawing.Font("Segoe UI", 11F);
            this.btnMembers.Location  = new System.Drawing.Point(150, 90);
            this.btnMembers.Size      = new System.Drawing.Size(200, 45);
            this.btnMembers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMembers.Click    += new System.EventHandler(this.btnMembers_Click);

            // ── btnWorkspaces ─────────────────────────────────
            this.btnWorkspaces.Text      = "🏢  Workspaces";
            this.btnWorkspaces.Font      = new System.Drawing.Font("Segoe UI", 11F);
            this.btnWorkspaces.Location  = new System.Drawing.Point(150, 148);
            this.btnWorkspaces.Size      = new System.Drawing.Size(200, 45);
            this.btnWorkspaces.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWorkspaces.Click    += new System.EventHandler(this.btnWorkspaces_Click);

            // ── btnReservations ───────────────────────────────
            this.btnReservations.Text      = "📅  Reservations";
            this.btnReservations.Font      = new System.Drawing.Font("Segoe UI", 11F);
            this.btnReservations.Location  = new System.Drawing.Point(150, 206);
            this.btnReservations.Size      = new System.Drawing.Size(200, 45);
            this.btnReservations.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReservations.Click    += new System.EventHandler(this.btnReservations_Click);

            // ── btnEquipment ──────────────────────────────────
            this.btnEquipment.Text      = "🔧  Equipment";
            this.btnEquipment.Font      = new System.Drawing.Font("Segoe UI", 11F);
            this.btnEquipment.Location  = new System.Drawing.Point(150, 264);
            this.btnEquipment.Size      = new System.Drawing.Size(200, 45);
            this.btnEquipment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEquipment.Click    += new System.EventHandler(this.btnEquipment_Click);

            // ── btnInquiries ──────────────────────────────────
            this.btnInquiries.Text      = "🔍  Inquiries";
            this.btnInquiries.Font      = new System.Drawing.Font("Segoe UI", 11F);
            this.btnInquiries.Location  = new System.Drawing.Point(150, 322);
            this.btnInquiries.Size      = new System.Drawing.Size(200, 45);
            this.btnInquiries.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInquiries.Click    += new System.EventHandler(this.btnInquiries_Click);

            // ── Form1 ─────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(500, 400);
            this.FormBorderStyle     = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox         = false;
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text                = "SmartWorkspace – Main Menu";
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnMembers);
            this.Controls.Add(this.btnWorkspaces);
            this.Controls.Add(this.btnReservations);
            this.Controls.Add(this.btnEquipment);
            this.Controls.Add(this.btnInquiries);
            this.ResumeLayout(false);
        }

        // ── Control declarations ──────────────────────────────
        private System.Windows.Forms.Label  lblTitle;
        private System.Windows.Forms.Button btnMembers;
        private System.Windows.Forms.Button btnWorkspaces;
        private System.Windows.Forms.Button btnReservations;
        private System.Windows.Forms.Button btnEquipment;
        private System.Windows.Forms.Button btnInquiries;
    }
}
