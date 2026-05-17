// ================================================
// FILE: WorkspaceForm.Designer.cs
// ================================================
namespace SmartWorkspace
{
    partial class WorkspaceForm
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
            this.lblType          = new System.Windows.Forms.Label();
            this.lblHub           = new System.Windows.Forms.Label();
            this.lblHourlyRate    = new System.Windows.Forms.Label();
            this.lblDailyRate     = new System.Windows.Forms.Label();
            this.lblStatusInput   = new System.Windows.Forms.Label();
            this.cmbType          = new System.Windows.Forms.ComboBox();
            this.cmbHub           = new System.Windows.Forms.ComboBox();
            this.txtHourlyRate    = new System.Windows.Forms.TextBox();
            this.txtDailyRate     = new System.Windows.Forms.TextBox();
            this.cmbStatus        = new System.Windows.Forms.ComboBox();
            this.btnAdd           = new System.Windows.Forms.Button();
            this.btnUpdate        = new System.Windows.Forms.Button();
            this.btnDelete        = new System.Windows.Forms.Button();
            this.btnLoad          = new System.Windows.Forms.Button();

            // Filter controls
            this.lblFilterStatus  = new System.Windows.Forms.Label();
            this.lblFilterHub     = new System.Windows.Forms.Label();
            this.lblFilterType    = new System.Windows.Forms.Label();
            this.cmbFilterStatus  = new System.Windows.Forms.ComboBox();
            this.cmbFilterHub     = new System.Windows.Forms.ComboBox();
            this.cmbFilterType    = new System.Windows.Forms.ComboBox();
            this.btnFilter        = new System.Windows.Forms.Button();
            this.btnClearFilter   = new System.Windows.Forms.Button();

            this.dataGridView1    = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();

            // ── lblTitle ──────────────────────────────────────
            this.lblTitle.AutoSize  = false;
            this.lblTitle.Text      = "Workspaces Management";
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location  = new System.Drawing.Point(15, 15);
            this.lblTitle.Size      = new System.Drawing.Size(750, 30);

            // ── lblType ───────────────────────────────────────
            this.lblType.AutoSize = true;
            this.lblType.Text     = "Workspace Type:";
            this.lblType.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.lblType.Location = new System.Drawing.Point(15, 60);

            // ── cmbType ───────────────────────────────────────
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Font          = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbType.Items.AddRange(new object[] { "private_office", "open_desk", "meeting_pod" });
            this.cmbType.SelectedIndex = 0;
            this.cmbType.Location      = new System.Drawing.Point(160, 57);
            this.cmbType.Size          = new System.Drawing.Size(220, 24);

            // ── lblHub ────────────────────────────────────────
            this.lblHub.AutoSize = true;
            this.lblHub.Text     = "Hub:";
            this.lblHub.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHub.Location = new System.Drawing.Point(15, 96);

            // ── cmbHub ────────────────────────────────────────
            this.cmbHub.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHub.Font          = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbHub.Location      = new System.Drawing.Point(160, 93);
            this.cmbHub.Size          = new System.Drawing.Size(220, 24);

            // ── lblHourlyRate ─────────────────────────────────
            this.lblHourlyRate.AutoSize = true;
            this.lblHourlyRate.Text     = "Hourly Rate:";
            this.lblHourlyRate.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHourlyRate.Location = new System.Drawing.Point(15, 132);

            // ── txtHourlyRate ─────────────────────────────────
            this.txtHourlyRate.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHourlyRate.Location = new System.Drawing.Point(160, 129);
            this.txtHourlyRate.Size     = new System.Drawing.Size(120, 24);

            // ── lblDailyRate ──────────────────────────────────
            this.lblDailyRate.AutoSize = true;
            this.lblDailyRate.Text     = "Daily Rate:";
            this.lblDailyRate.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDailyRate.Location = new System.Drawing.Point(295, 132);

            // ── txtDailyRate ──────────────────────────────────
            this.txtDailyRate.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDailyRate.Location = new System.Drawing.Point(385, 129);
            this.txtDailyRate.Size     = new System.Drawing.Size(120, 24);

            // ── lblStatusInput ────────────────────────────────
            this.lblStatusInput.AutoSize = true;
            this.lblStatusInput.Text     = "Status:";
            this.lblStatusInput.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatusInput.Location = new System.Drawing.Point(520, 132);

            // ── cmbStatus ─────────────────────────────────────
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font          = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbStatus.Items.AddRange(new object[] { "available", "reserved", "maintenance" });
            this.cmbStatus.SelectedIndex = 0;
            this.cmbStatus.Location      = new System.Drawing.Point(580, 129);
            this.cmbStatus.Size          = new System.Drawing.Size(140, 24);

            // ── Buttons Row ───────────────────────────────────
            this.btnAdd.Text      = "Add";
            this.btnAdd.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAdd.Location  = new System.Drawing.Point(15, 170);
            this.btnAdd.Size      = new System.Drawing.Size(110, 35);
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Click    += new System.EventHandler(this.btnAdd_Click);

            this.btnUpdate.Text      = "Update";
            this.btnUpdate.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.btnUpdate.Location  = new System.Drawing.Point(135, 170);
            this.btnUpdate.Size      = new System.Drawing.Size(110, 35);
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Click    += new System.EventHandler(this.btnUpdate_Click);

            this.btnDelete.Text      = "Delete";
            this.btnDelete.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDelete.Location  = new System.Drawing.Point(255, 170);
            this.btnDelete.Size      = new System.Drawing.Size(110, 35);
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Click    += new System.EventHandler(this.btnDelete_Click);

            this.btnLoad.Text      = "Load All";
            this.btnLoad.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.btnLoad.Location  = new System.Drawing.Point(375, 170);
            this.btnLoad.Size      = new System.Drawing.Size(110, 35);
            this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoad.Click    += new System.EventHandler(this.btnLoad_Click);

            // ── Filter Row ────────────────────────────────────
            this.lblFilterStatus.AutoSize = true;
            this.lblFilterStatus.Text     = "Status:";
            this.lblFilterStatus.Font     = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFilterStatus.Location = new System.Drawing.Point(15, 220);

            this.cmbFilterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterStatus.Font          = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbFilterStatus.Items.AddRange(new object[] { "All", "available", "reserved", "maintenance" });
            this.cmbFilterStatus.SelectedIndex = 0;
            this.cmbFilterStatus.Location      = new System.Drawing.Point(60, 217);
            this.cmbFilterStatus.Size          = new System.Drawing.Size(110, 22);

            this.lblFilterHub.AutoSize = true;
            this.lblFilterHub.Text     = "Hub:";
            this.lblFilterHub.Font     = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFilterHub.Location = new System.Drawing.Point(180, 220);

            this.cmbFilterHub.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterHub.Font          = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbFilterHub.Location      = new System.Drawing.Point(215, 217);
            this.cmbFilterHub.Size          = new System.Drawing.Size(150, 22);

            this.lblFilterType.AutoSize = true;
            this.lblFilterType.Text     = "Type:";
            this.lblFilterType.Font     = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFilterType.Location = new System.Drawing.Point(375, 220);

            this.cmbFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterType.Font          = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbFilterType.Location      = new System.Drawing.Point(415, 217);
            this.cmbFilterType.Size          = new System.Drawing.Size(150, 22);

            this.btnFilter.Text      = "Apply Filter";
            this.btnFilter.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFilter.Location  = new System.Drawing.Point(575, 215);
            this.btnFilter.Size      = new System.Drawing.Size(100, 26);
            this.btnFilter.BackColor = System.Drawing.Color.FromArgb(255, 153, 0);
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Click    += new System.EventHandler(this.btnFilter_Click);

            this.btnClearFilter.Text      = "Clear";
            this.btnClearFilter.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClearFilter.Location  = new System.Drawing.Point(685, 215);
            this.btnClearFilter.Size      = new System.Drawing.Size(70, 26);
            this.btnClearFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearFilter.Click    += new System.EventHandler(this.btnClearFilter_Click);

            // ── dataGridView1 ─────────────────────────────────
            this.dataGridView1.AllowUserToAddRows    = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode   = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor       = System.Drawing.SystemColors.Window;
            this.dataGridView1.ReadOnly              = true;
            this.dataGridView1.RowHeadersVisible     = false;
            this.dataGridView1.SelectionMode         = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Location              = new System.Drawing.Point(15, 253);
            this.dataGridView1.Size                  = new System.Drawing.Size(750, 290);
            this.dataGridView1.SelectionChanged     += new System.EventHandler(this.dataGridView1_SelectionChanged);

            // ── WorkspaceForm ─────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(780, 558);
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text                = "SmartWorkspace – Workspaces";
            this.Load               += new System.EventHandler(this.WorkspaceForm_Load);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.lblHub);
            this.Controls.Add(this.cmbHub);
            this.Controls.Add(this.lblHourlyRate);
            this.Controls.Add(this.txtHourlyRate);
            this.Controls.Add(this.lblDailyRate);
            this.Controls.Add(this.txtDailyRate);
            this.Controls.Add(this.lblStatusInput);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.lblFilterStatus);
            this.Controls.Add(this.cmbFilterStatus);
            this.Controls.Add(this.lblFilterHub);
            this.Controls.Add(this.cmbFilterHub);
            this.Controls.Add(this.lblFilterType);
            this.Controls.Add(this.cmbFilterType);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.btnClearFilter);
            this.Controls.Add(this.dataGridView1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // ── Control declarations ──────────────────────────────
        private System.Windows.Forms.Label        lblTitle;
        private System.Windows.Forms.Label        lblType;
        private System.Windows.Forms.Label        lblHub;
        private System.Windows.Forms.Label        lblHourlyRate;
        private System.Windows.Forms.Label        lblDailyRate;
        private System.Windows.Forms.Label        lblStatusInput;
        private System.Windows.Forms.ComboBox     cmbType;
        private System.Windows.Forms.ComboBox     cmbHub;
        private System.Windows.Forms.TextBox      txtHourlyRate;
        private System.Windows.Forms.TextBox      txtDailyRate;
        private System.Windows.Forms.ComboBox     cmbStatus;
        private System.Windows.Forms.Button       btnAdd;
        private System.Windows.Forms.Button       btnUpdate;
        private System.Windows.Forms.Button       btnDelete;
        private System.Windows.Forms.Button       btnLoad;
        private System.Windows.Forms.Label        lblFilterStatus;
        private System.Windows.Forms.Label        lblFilterHub;
        private System.Windows.Forms.Label        lblFilterType;
        private System.Windows.Forms.ComboBox     cmbFilterStatus;
        private System.Windows.Forms.ComboBox     cmbFilterHub;
        private System.Windows.Forms.ComboBox     cmbFilterType;
        private System.Windows.Forms.Button       btnFilter;
        private System.Windows.Forms.Button       btnClearFilter;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}
