// ================================================
// FILE: EquipmentForm.Designer.cs
// ================================================
namespace SmartWorkspace
{
    partial class EquipmentForm
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
            this.lblTitle      = new System.Windows.Forms.Label();
            this.lblName       = new System.Windows.Forms.Label();
            this.lblType       = new System.Windows.Forms.Label();
            this.lblHub        = new System.Windows.Forms.Label();
            this.txtName       = new System.Windows.Forms.TextBox();
            this.txtType       = new System.Windows.Forms.TextBox();
            this.txtHub        = new System.Windows.Forms.TextBox();
            this.btnAdd        = new System.Windows.Forms.Button();
            this.btnUpdate     = new System.Windows.Forms.Button();
            this.btnDelete     = new System.Windows.Forms.Button();
            this.btnLoad       = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();

            // ── lblTitle ──────────────────────────────────────
            this.lblTitle.AutoSize  = false;
            this.lblTitle.Text      = "Equipment Management";
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location  = new System.Drawing.Point(15, 15);
            this.lblTitle.Size      = new System.Drawing.Size(750, 30);

            // ── lblName ───────────────────────────────────────
            this.lblName.AutoSize = true;
            this.lblName.Text     = "Equipment Name:";
            this.lblName.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.lblName.Location = new System.Drawing.Point(15, 60);

            // ── txtName ───────────────────────────────────────
            this.txtName.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.Location = new System.Drawing.Point(165, 57);
            this.txtName.Size     = new System.Drawing.Size(250, 24);

            // ── lblType ───────────────────────────────────────
            this.lblType.AutoSize = true;
            this.lblType.Text     = "Equipment Type:";
            this.lblType.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.lblType.Location = new System.Drawing.Point(15, 96);

            // ── txtType ───────────────────────────────────────
            this.txtType.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.txtType.Location = new System.Drawing.Point(165, 93);
            this.txtType.Size     = new System.Drawing.Size(250, 24);

            // ── lblHub ────────────────────────────────────────
            this.lblHub.AutoSize = true;
            this.lblHub.Text     = "Hub / Urban:";
            this.lblHub.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHub.Location = new System.Drawing.Point(15, 132);

            // ── txtHub ────────────────────────────────────────
            this.txtHub.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHub.Location = new System.Drawing.Point(165, 129);
            this.txtHub.Size     = new System.Drawing.Size(250, 24);

            // ── Buttons ───────────────────────────────────────
            this.btnAdd.Text      = "Add Equipment";
            this.btnAdd.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAdd.Location  = new System.Drawing.Point(15, 170);
            this.btnAdd.Size      = new System.Drawing.Size(140, 35);
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Click    += new System.EventHandler(this.btnAdd_Click);

            this.btnUpdate.Text      = "Update";
            this.btnUpdate.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.btnUpdate.Location  = new System.Drawing.Point(165, 170);
            this.btnUpdate.Size      = new System.Drawing.Size(120, 35);
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Click    += new System.EventHandler(this.btnUpdate_Click);

            this.btnDelete.Text      = "Delete";
            this.btnDelete.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDelete.Location  = new System.Drawing.Point(295, 170);
            this.btnDelete.Size      = new System.Drawing.Size(120, 35);
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Click    += new System.EventHandler(this.btnDelete_Click);

            this.btnLoad.Text      = "Load All";
            this.btnLoad.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.btnLoad.Location  = new System.Drawing.Point(425, 170);
            this.btnLoad.Size      = new System.Drawing.Size(120, 35);
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
            this.dataGridView1.Location              = new System.Drawing.Point(15, 220);
            this.dataGridView1.Size                  = new System.Drawing.Size(750, 300);
            this.dataGridView1.SelectionChanged     += new System.EventHandler(this.dataGridView1_SelectionChanged);

            // ── EquipmentForm ─────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(780, 535);
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text                = "SmartWorkspace – Equipment";
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.lblHub);
            this.Controls.Add(this.txtHub);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.dataGridView1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // ── Control declarations ──────────────────────────────
        private System.Windows.Forms.Label        lblTitle;
        private System.Windows.Forms.Label        lblName;
        private System.Windows.Forms.Label        lblType;
        private System.Windows.Forms.Label        lblHub;
        private System.Windows.Forms.TextBox      txtName;
        private System.Windows.Forms.TextBox      txtType;
        private System.Windows.Forms.TextBox      txtHub;
        private System.Windows.Forms.Button       btnAdd;
        private System.Windows.Forms.Button       btnUpdate;
        private System.Windows.Forms.Button       btnDelete;
        private System.Windows.Forms.Button       btnLoad;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}
