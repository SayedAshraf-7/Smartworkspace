// ================================================
// FILE: InquiryForm.Designer.cs
// ================================================
namespace SmartWorkspace
{
    partial class InquiryForm
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
            this.lblTitle                = new System.Windows.Forms.Label();

            // Row 1 buttons
            this.btnPopular              = new System.Windows.Forms.Button();
            this.btnNoReservation        = new System.Windows.Forms.Button();
            this.btnNoReservationHubs    = new System.Windows.Forms.Button();

            // Row 2 buttons
            this.btnMostVariedEquipment  = new System.Windows.Forms.Button();
            this.btnEquipmentByHub       = new System.Windows.Forms.Button();

            this.lblResult               = new System.Windows.Forms.Label();
            this.dataGridView1           = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();

            // ── lblTitle ──────────────────────────────────────
            this.lblTitle.AutoSize  = false;
            this.lblTitle.Text      = "Inquiries";
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location  = new System.Drawing.Point(15, 15);
            this.lblTitle.Size      = new System.Drawing.Size(750, 30);

            // ── Row 1 ─────────────────────────────────────────
            this.btnPopular.Text      = "Most Popular Workspace Type";
            this.btnPopular.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.btnPopular.Location  = new System.Drawing.Point(15, 58);
            this.btnPopular.Size      = new System.Drawing.Size(225, 38);
            this.btnPopular.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPopular.Click    += new System.EventHandler(this.btnPopular_Click);

            this.btnNoReservation.Text      = "Members With No Reservations";
            this.btnNoReservation.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNoReservation.Location  = new System.Drawing.Point(250, 58);
            this.btnNoReservation.Size      = new System.Drawing.Size(225, 38);
            this.btnNoReservation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNoReservation.Click    += new System.EventHandler(this.btnNoReservation_Click);

            this.btnNoReservationHubs.Text      = "Hubs With No Reservations (Last Month)";
            this.btnNoReservationHubs.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNoReservationHubs.Location  = new System.Drawing.Point(485, 58);
            this.btnNoReservationHubs.Size      = new System.Drawing.Size(275, 38);
            this.btnNoReservationHubs.BackColor = System.Drawing.Color.FromArgb(23, 162, 184);
            this.btnNoReservationHubs.ForeColor = System.Drawing.Color.White;
            this.btnNoReservationHubs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNoReservationHubs.Click    += new System.EventHandler(this.btnNoReservationHubs_Click);

            // ── Row 2 ─────────────────────────────────────────
            this.btnMostVariedEquipment.Text      = "Members – Most Equipment Variety";
            this.btnMostVariedEquipment.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.btnMostVariedEquipment.Location  = new System.Drawing.Point(15, 105);
            this.btnMostVariedEquipment.Size      = new System.Drawing.Size(250, 38);
            this.btnMostVariedEquipment.BackColor = System.Drawing.Color.FromArgb(111, 66, 193);
            this.btnMostVariedEquipment.ForeColor = System.Drawing.Color.White;
            this.btnMostVariedEquipment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMostVariedEquipment.Click    += new System.EventHandler(this.btnMostVariedEquipment_Click);

            this.btnEquipmentByHub.Text      = "Equipment Used per Hub (Last Month)";
            this.btnEquipmentByHub.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.btnEquipmentByHub.Location  = new System.Drawing.Point(275, 105);
            this.btnEquipmentByHub.Size      = new System.Drawing.Size(270, 38);
            this.btnEquipmentByHub.BackColor = System.Drawing.Color.FromArgb(255, 153, 0);
            this.btnEquipmentByHub.ForeColor = System.Drawing.Color.White;
            this.btnEquipmentByHub.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEquipmentByHub.Click    += new System.EventHandler(this.btnEquipmentByHub_Click);

            // ── lblResult ─────────────────────────────────────
            this.lblResult.AutoSize  = false;
            this.lblResult.Text      = "Click a button above to run a query.";
            this.lblResult.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblResult.ForeColor = System.Drawing.Color.DimGray;
            this.lblResult.Location  = new System.Drawing.Point(15, 152);
            this.lblResult.Size      = new System.Drawing.Size(750, 22);

            // ── dataGridView1 ─────────────────────────────────
            this.dataGridView1.AllowUserToAddRows    = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode   = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor       = System.Drawing.SystemColors.Window;
            this.dataGridView1.ReadOnly              = true;
            this.dataGridView1.RowHeadersVisible     = false;
            this.dataGridView1.SelectionMode         = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Location              = new System.Drawing.Point(15, 180);
            this.dataGridView1.Size                  = new System.Drawing.Size(750, 350);

            // ── InquiryForm ───────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(780, 545);
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text                = "SmartWorkspace – Inquiries";
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnPopular);
            this.Controls.Add(this.btnNoReservation);
            this.Controls.Add(this.btnNoReservationHubs);
            this.Controls.Add(this.btnMostVariedEquipment);
            this.Controls.Add(this.btnEquipmentByHub);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.dataGridView1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // ── Control declarations ──────────────────────────────
        private System.Windows.Forms.Label        lblTitle;
        private System.Windows.Forms.Button       btnPopular;
        private System.Windows.Forms.Button       btnNoReservation;
        private System.Windows.Forms.Button       btnNoReservationHubs;
        private System.Windows.Forms.Button       btnMostVariedEquipment;
        private System.Windows.Forms.Button       btnEquipmentByHub;
        private System.Windows.Forms.Label        lblResult;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}
