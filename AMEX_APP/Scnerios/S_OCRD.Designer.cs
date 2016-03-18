namespace Armada_App
{
    partial class S_OCRD
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(S_OCRD));
            this.dgv_S_OCRD = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.scForm = new System.Windows.Forms.SplitContainer();
            this.lblScenerio = new System.Windows.Forms.Label();
            this.scDetail = new System.Windows.Forms.SplitContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnSync = new System.Windows.Forms.Button();
            this.DocEntry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Scenario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransactionType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Source_Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Country = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_S_OCRD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scForm)).BeginInit();
            this.scForm.Panel1.SuspendLayout();
            this.scForm.Panel2.SuspendLayout();
            this.scForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scDetail)).BeginInit();
            this.scDetail.Panel1.SuspendLayout();
            this.scDetail.Panel2.SuspendLayout();
            this.scDetail.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_S_OCRD
            // 
            this.dgv_S_OCRD.AllowUserToAddRows = false;
            this.dgv_S_OCRD.AllowUserToDeleteRows = false;
            this.dgv_S_OCRD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_S_OCRD.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DocEntry,
            this.Scenario,
            this.TransactionType,
            this.Source_Key,
            this.Remarks,
            this.Country});
            this.dgv_S_OCRD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_S_OCRD.Location = new System.Drawing.Point(0, 0);
            this.dgv_S_OCRD.Margin = new System.Windows.Forms.Padding(4);
            this.dgv_S_OCRD.Name = "dgv_S_OCRD";
            this.dgv_S_OCRD.ReadOnly = true;
            this.dgv_S_OCRD.Size = new System.Drawing.Size(894, 362);
            this.dgv_S_OCRD.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnCancel.Location = new System.Drawing.Point(116, 7);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // scForm
            // 
            this.scForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scForm.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scForm.IsSplitterFixed = true;
            this.scForm.Location = new System.Drawing.Point(0, 0);
            this.scForm.Margin = new System.Windows.Forms.Padding(4);
            this.scForm.Name = "scForm";
            this.scForm.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scForm.Panel1
            // 
            this.scForm.Panel1.Controls.Add(this.lblScenerio);
            // 
            // scForm.Panel2
            // 
            this.scForm.Panel2.Controls.Add(this.scDetail);
            this.scForm.Size = new System.Drawing.Size(894, 472);
            this.scForm.SplitterDistance = 37;
            this.scForm.TabIndex = 7;
            // 
            // lblScenerio
            // 
            this.lblScenerio.AutoSize = true;
            this.lblScenerio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblScenerio.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScenerio.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblScenerio.Location = new System.Drawing.Point(0, 0);
            this.lblScenerio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblScenerio.Name = "lblScenerio";
            this.lblScenerio.Size = new System.Drawing.Size(264, 23);
            this.lblScenerio.TabIndex = 0;
            this.lblScenerio.Text = "BUSINESS PARTNER SYNC";
            this.lblScenerio.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // scDetail
            // 
            this.scDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scDetail.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scDetail.IsSplitterFixed = true;
            this.scDetail.Location = new System.Drawing.Point(0, 0);
            this.scDetail.Margin = new System.Windows.Forms.Padding(4);
            this.scDetail.Name = "scDetail";
            this.scDetail.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scDetail.Panel1
            // 
            this.scDetail.Panel1.Controls.Add(this.dgv_S_OCRD);
            // 
            // scDetail.Panel2
            // 
            this.scDetail.Panel2.Controls.Add(this.statusStrip1);
            this.scDetail.Panel2.Controls.Add(this.btnCancel);
            this.scDetail.Panel2.Controls.Add(this.btnSync);
            this.scDetail.Size = new System.Drawing.Size(894, 431);
            this.scDetail.SplitterDistance = 362;
            this.scDetail.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 43);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip1.Size = new System.Drawing.Size(894, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(765, 17);
            this.StatusLabel.Text = resources.GetString("StatusLabel.Text");
            // 
            // btnSync
            // 
            this.btnSync.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnSync.Location = new System.Drawing.Point(5, 7);
            this.btnSync.Margin = new System.Windows.Forms.Padding(4);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(105, 30);
            this.btnSync.TabIndex = 0;
            this.btnSync.Text = "Sync";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // DocEntry
            // 
            this.DocEntry.DataPropertyName = "DocEntry";
            this.DocEntry.HeaderText = "DocEntry";
            this.DocEntry.Name = "DocEntry";
            this.DocEntry.ReadOnly = true;
            // 
            // Scenario
            // 
            this.Scenario.DataPropertyName = "Scenario";
            this.Scenario.HeaderText = "Scenario";
            this.Scenario.Name = "Scenario";
            this.Scenario.ReadOnly = true;
            // 
            // TransactionType
            // 
            this.TransactionType.DataPropertyName = "TransType";
            this.TransactionType.HeaderText = "TransactionType";
            this.TransactionType.Name = "TransactionType";
            this.TransactionType.ReadOnly = true;
            this.TransactionType.Width = 120;
            // 
            // Source_Key
            // 
            this.Source_Key.DataPropertyName = "Key";
            this.Source_Key.HeaderText = "Source_Key";
            this.Source_Key.Name = "Source_Key";
            this.Source_Key.ReadOnly = true;
            // 
            // Remarks
            // 
            this.Remarks.DataPropertyName = "Remarks";
            this.Remarks.HeaderText = "Remarks";
            this.Remarks.Name = "Remarks";
            this.Remarks.ReadOnly = true;
            // 
            // Country
            // 
            this.Country.DataPropertyName = "Country";
            this.Country.HeaderText = "Country";
            this.Country.Name = "Country";
            this.Country.ReadOnly = true;
            // 
            // S_OCRD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(894, 472);
            this.Controls.Add(this.scForm);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "S_OCRD";
            this.ShowInTaskbar = false;
            this.Text = "BUSINESS PARTNER SYNC";
            this.Load += new System.EventHandler(this.S_OCRD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_S_OCRD)).EndInit();
            this.scForm.Panel1.ResumeLayout(false);
            this.scForm.Panel1.PerformLayout();
            this.scForm.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scForm)).EndInit();
            this.scForm.ResumeLayout(false);
            this.scDetail.Panel1.ResumeLayout(false);
            this.scDetail.Panel2.ResumeLayout(false);
            this.scDetail.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scDetail)).EndInit();
            this.scDetail.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_S_OCRD;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.SplitContainer scForm;
        private System.Windows.Forms.Label lblScenerio;
        private System.Windows.Forms.SplitContainer scDetail;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocEntry;
        private System.Windows.Forms.DataGridViewTextBoxColumn Scenario;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransactionType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Source_Key;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn Country;
    }
}