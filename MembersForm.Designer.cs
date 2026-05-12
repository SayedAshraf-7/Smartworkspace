// ================================================
// FILE: MembersForm.Designer.cs
// ================================================
namespace SmartWorkspace
{
    partial class MembersForm
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
            this.lblName         = new System.Windows.Forms.Label();
            this.lblDigitalID    = new System.Windows.Forms.Label();
            this.lblAffiliation  = new System.Windows.Forms.Label();
            this.txtName         = new System.Windows.Forms.TextBox();
            this.txtDigitalID    = new System.Windows.Forms.TextBox();
            this.txtAffiliation  = new System.Windows.Forms.TextBox();
            this.btnAdd          = new System.Windows.Forms.Button();
            this.btnUpdate       = new System.Windows.Forms.Button();
            this.btnDelete       = new System.Windows.Forms.Button();
            this.btnLoad         = new System.Windows.Forms.Button();
            this.dataGridView1   = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();

            // ── lblTitle ──────────────────────────────────────
            this.lblTitle.AutoSize  = false;
            this.lblTitle.Text      = "Members Management";
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location  = new System.Drawing.Point(15, 15);
            this.lblTitle.Size      = new System.Drawing.Size(750, 30);

            // ── lblName ───────────────────────────────────────
            this.lblName.AutoSize = true;
            this.lblName.Text     = "Full Name:";
            this.lblName.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.lblName.Location = new System.Drawing.Point(15, 60);

            // ── txtName ───────────────────────────────────────
            this.txtName.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.Location = new System.Drawing.Point(160, 57);
            this.txtName.Size     = new System.Drawing.Size(280, 24);

            // ── lblDigitalID ──────────────────────────────────
            this.lblDigitalID.AutoSize = true;
            this.lblDigitalID.Text     = "Digital ID:";
            this.lblDigitalID.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDigitalID.Location = new System.Drawing.Point(15, 96);

            // ── txtDigitalID ──────────────────────────────────
            this.txtDigitalID.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDigitalID.Location = new System.Drawing.Point(160, 93);
            this.txtDigitalID.Size     = new System.Drawing.Size(280, 24);

            // ── lblAffiliation ────────────────────────────────
            this.lblAffiliation.AutoSize = true;
            this.lblAffiliation.Text     = "Corp. Affiliation:";
            this.lblAffiliation.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAffiliation.Location = new System.Drawing.Point(15, 132);

            // ── txtAffiliation ────────────────────────────────
            this.txtAffiliation.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAffiliation.Location = new System.Drawing.Point(160, 129);
            this.txtAffiliation.Size     = new System.Drawing.Size(280, 24);

            // ── btnAdd ────────────────────────────────────────
            this.btnAdd.Text      = "Add Member";
            this.btnAdd.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAdd.Location  = new System.Drawing.Point(15, 172);
            this.btnAdd.Size      = new System.Drawing.Size(130, 35);
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Click    += new System.EventHandler(this.btnAdd_Click);

            // ── btnUpdate ─────────────────────────────────────
            this.btnUpdate.Text      = "Update Member";
            this.btnUpdate.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.btnUpdate.Location  = new System.Drawing.Point(155, 172);
            this.btnUpdate.Size      = new System.Drawing.Size(140, 35);
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Click    += new System.EventHandler(this.btnUpdate_Click);

            // ── btnDelete ─────────────────────────────────────
            this.btnDelete.Text      = "Delete Member";
            this.btnDelete.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDelete.Location  = new System.Drawing.Point(305, 172);
            this.btnDelete.Size      = new System.Drawing.Size(140, 35);
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Click    += new System.EventHandler(this.btnDelete_Click);

            // ── btnLoad ───────────────────────────────────────
            this.btnLoad.Text      = "Load Members";
            this.btnLoad.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.btnLoad.Location  = new System.Drawing.Point(455, 172);
            this.btnLoad.Size      = new System.Drawing.Size(130, 35);
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
            this.dataGridView1.Size                  = new System.Drawing.Size(750, 310);
            this.dataGridView1.SelectionChanged     += new System.EventHandler(this.dataGridView1_SelectionChanged);

            // ── MembersForm ───────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(780, 545);
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text                = "SmartWorkspace – Members";
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblDigitalID);
            this.Controls.Add(this.txtDigitalID);
            this.Controls.Add(this.lblAffiliation);
            this.Controls.Add(this.txtAffiliation);
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
        private System.Windows.Forms.Label        lblDigitalID;
        private System.Windows.Forms.Label        lblAffiliation;
        private System.Windows.Forms.TextBox      txtName;
        private System.Windows.Forms.TextBox      txtDigitalID;
        private System.Windows.Forms.TextBox      txtAffiliation;
        private System.Windows.Forms.Button       btnAdd;
        private System.Windows.Forms.Button       btnUpdate;
        private System.Windows.Forms.Button       btnDelete;
        private System.Windows.Forms.Button       btnLoad;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}
