// ================================================
// FILE: ReservationForm.Designer.cs
// ================================================
namespace SmartWorkspace
{
    partial class ReservationForm
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
            this.lblTitle        = new System.Windows.Forms.Label();
            this.lblMember       = new System.Windows.Forms.Label();
            this.lblWorkspace    = new System.Windows.Forms.Label();
            this.lblDate         = new System.Windows.Forms.Label();
            this.lblHours        = new System.Windows.Forms.Label();
            this.lblStatus       = new System.Windows.Forms.Label();
            this.cmbMember       = new System.Windows.Forms.ComboBox();
            this.cmbWorkspace    = new System.Windows.Forms.ComboBox();
            this.lblEquipment    = new System.Windows.Forms.Label();
            this.clbEquipment    = new System.Windows.Forms.CheckedListBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txtHours        = new System.Windows.Forms.TextBox();
            this.cmbResStatus    = new System.Windows.Forms.ComboBox();
            this.btnReserve      = new System.Windows.Forms.Button();
            this.btnUpdateStatus = new System.Windows.Forms.Button();
            this.btnDelete       = new System.Windows.Forms.Button();
            this.btnLoad         = new System.Windows.Forms.Button();
            this.dataGridView1   = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();

            // ── lblTitle ──────────────────────────────────────
            this.lblTitle.AutoSize  = false;
            this.lblTitle.Text      = "Reservations Management";
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location  = new System.Drawing.Point(15, 15);
            this.lblTitle.Size      = new System.Drawing.Size(750, 30);

            // ── lblMember ─────────────────────────────────────
            this.lblMember.AutoSize = true;
            this.lblMember.Text     = "Member:";
            this.lblMember.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMember.Location = new System.Drawing.Point(15, 60);

            // ── cmbMember ─────────────────────────────────────
            this.cmbMember.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMember.Font          = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbMember.Location      = new System.Drawing.Point(130, 57);
            this.cmbMember.Size          = new System.Drawing.Size(280, 24);

            // ── lblWorkspace ──────────────────────────────────
            this.lblWorkspace.AutoSize = true;
            this.lblWorkspace.Text     = "Workspace:";
            this.lblWorkspace.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWorkspace.Location = new System.Drawing.Point(15, 96);

            // ── cmbWorkspace ──────────────────────────────────
            this.cmbWorkspace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWorkspace.Font          = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbWorkspace.Location      = new System.Drawing.Point(130, 93);
            this.cmbWorkspace.Size          = new System.Drawing.Size(280, 24);

            // ── lblEquipment ─────────────────────────────────────
            this.lblEquipment.AutoSize = true;
            this.lblEquipment.Text     = "Equipment:";
            this.lblEquipment.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEquipment.Location = new System.Drawing.Point(15, 135);

            // ── clbEquipment ─────────────────────────────────────
            this.clbEquipment.Font         = new System.Drawing.Font("Segoe UI", 10F);
            this.clbEquipment.Location     = new System.Drawing.Point(130, 132);
            this.clbEquipment.Size         = new System.Drawing.Size(280, 80);
            this.clbEquipment.CheckOnClick = true;

            // ── lblDate ───────────────────────────────────────
            this.lblDate.AutoSize = true;
            this.lblDate.Text     = "Date:";
            this.lblDate.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDate.Location = new System.Drawing.Point(15, 222);

            // ── dateTimePicker1 ───────────────────────────────
            this.dateTimePicker1.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.dateTimePicker1.Format   = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(130, 219);
            this.dateTimePicker1.Size     = new System.Drawing.Size(180, 24);

            // ── lblHours ──────────────────────────────────────
            this.lblHours.AutoSize = true;
            this.lblHours.Text     = "Hours:";
            this.lblHours.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHours.Location = new System.Drawing.Point(15, 258);

            // ── txtHours ──────────────────────────────────────
            this.txtHours.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHours.Location = new System.Drawing.Point(130, 255);
            this.txtHours.Size     = new System.Drawing.Size(100, 24);

            // ── lblStatus ─────────────────────────────────────
            this.lblStatus.AutoSize = true;
            this.lblStatus.Text     = "Status:";
            this.lblStatus.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.Location = new System.Drawing.Point(250, 258);

            // ── cmbResStatus ──────────────────────────────────
            this.cmbResStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResStatus.Font          = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbResStatus.Items.AddRange(new object[] { "Running", "Finished" });
            this.cmbResStatus.SelectedIndex = 0;
            this.cmbResStatus.Location      = new System.Drawing.Point(310, 255);
            this.cmbResStatus.Size          = new System.Drawing.Size(130, 24);

            // ── Buttons ───────────────────────────────────────
            this.btnReserve.Text      = "Add Reservation";
            this.btnReserve.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.btnReserve.Location  = new System.Drawing.Point(15, 295);
            this.btnReserve.Size      = new System.Drawing.Size(145, 35);
            this.btnReserve.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.btnReserve.ForeColor = System.Drawing.Color.White;
            this.btnReserve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReserve.Click    += new System.EventHandler(this.btnReserve_Click);

            this.btnUpdateStatus.Text      = "Update Status";
            this.btnUpdateStatus.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.btnUpdateStatus.Location  = new System.Drawing.Point(170, 295);
            this.btnUpdateStatus.Size      = new System.Drawing.Size(140, 35);
            this.btnUpdateStatus.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnUpdateStatus.ForeColor = System.Drawing.Color.White;
            this.btnUpdateStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateStatus.Click    += new System.EventHandler(this.btnUpdateStatus_Click);

            this.btnDelete.Text      = "Delete";
            this.btnDelete.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDelete.Location  = new System.Drawing.Point(320, 295);
            this.btnDelete.Size      = new System.Drawing.Size(110, 35);
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Click    += new System.EventHandler(this.btnDelete_Click);

            this.btnLoad.Text      = "Load All";
            this.btnLoad.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.btnLoad.Location  = new System.Drawing.Point(440, 295);
            this.btnLoad.Size      = new System.Drawing.Size(140, 35);
            this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoad.Click    += new System.EventHandler(this.btnLoad_Click);

            // ── dataGridView1 ─────────────────────────────────
            this.dataGridView1.AllowUserToAddRows    = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode   = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor       = System.Drawing.SystemColors.Window;
            this.dataGridView1.ReadOnly              = true;
            this.dataGridView1.RowHeadersVisible     = false;
            this.dataGridView1.SelectionMode         = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Location              = new System.Drawing.Point(15, 342);
            this.dataGridView1.Size                  = new System.Drawing.Size(750, 310);
            this.dataGridView1.SelectionChanged     += new System.EventHandler(this.dataGridView1_SelectionChanged);

            // ── ReservationForm ───────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(780, 665);
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text                = "SmartWorkspace – Reservations";
            this.Load               += new System.EventHandler(this.ReservationForm_Load);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblMember);
            this.Controls.Add(this.cmbMember);
            this.Controls.Add(this.lblWorkspace);
            this.Controls.Add(this.cmbWorkspace);
            this.Controls.Add(this.lblEquipment);
            this.Controls.Add(this.clbEquipment);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.lblHours);
            this.Controls.Add(this.txtHours);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmbResStatus);
            this.Controls.Add(this.btnReserve);
            this.Controls.Add(this.btnUpdateStatus);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.dataGridView1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // ── Control declarations ──────────────────────────────
        private System.Windows.Forms.Label          lblTitle;
        private System.Windows.Forms.Label          lblMember;
        private System.Windows.Forms.Label          lblWorkspace;
        private System.Windows.Forms.Label          lblDate;
        private System.Windows.Forms.Label          lblHours;
        private System.Windows.Forms.Label          lblStatus;
        private System.Windows.Forms.ComboBox       cmbMember;
        private System.Windows.Forms.ComboBox       cmbWorkspace;
        private System.Windows.Forms.Label          lblEquipment;
        private System.Windows.Forms.CheckedListBox clbEquipment;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox        txtHours;
        private System.Windows.Forms.ComboBox       cmbResStatus;
        private System.Windows.Forms.Button         btnReserve;
        private System.Windows.Forms.Button         btnUpdateStatus;
        private System.Windows.Forms.Button         btnDelete;
        private System.Windows.Forms.Button         btnLoad;
        private System.Windows.Forms.DataGridView   dataGridView1;
    }
}
