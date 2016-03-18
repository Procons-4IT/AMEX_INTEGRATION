namespace AMEX_APP
{
    partial class TransLog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransLog));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.companyStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.timeStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.oTimer = new System.Windows.Forms.Timer(this.components);
            this.scTransLog = new System.Windows.Forms.SplitContainer();
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.scHeader = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.txtInstance = new System.Windows.Forms.TextBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.BtnProcess = new System.Windows.Forms.Button();
            this.CmbScenario = new System.Windows.Forms.ComboBox();
            this.lblScenario = new System.Windows.Forms.Label();
            this.Fromdate = new System.Windows.Forms.DateTimePicker();
            this.lblFromRequestDate = new System.Windows.Forms.Label();
            this.flpImages = new System.Windows.Forms.FlowLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblRec = new System.Windows.Forms.Label();
            this.DgvTxnLogParent = new System.Windows.Forms.DataGridView();
            this.Image = new System.Windows.Forms.DataGridViewImageColumn();
            this.DA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Currency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dim2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dim3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.D_DocNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErroCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.niTaskBar = new System.Windows.Forms.NotifyIcon(this.components);
            this.Systemtry = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scTransLog)).BeginInit();
            this.scTransLog.Panel1.SuspendLayout();
            this.scTransLog.Panel2.SuspendLayout();
            this.scTransLog.SuspendLayout();
            this.HeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scHeader)).BeginInit();
            this.scHeader.Panel1.SuspendLayout();
            this.scHeader.Panel2.SuspendLayout();
            this.scHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvTxnLogParent)).BeginInit();
            this.Systemtry.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.minimizeToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuStrip1.Size = new System.Drawing.Size(573, 26);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logOffToolStripMenuItem});
            this.exitToolStripMenuItem.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitToolStripMenuItem.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.exitToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(51, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // logOffToolStripMenuItem
            // 
            this.logOffToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logOffToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.logOffToolStripMenuItem.Name = "logOffToolStripMenuItem";
            this.logOffToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.logOffToolStripMenuItem.Text = "Log Off";
            this.logOffToolStripMenuItem.Click += new System.EventHandler(this.logOffToolStripMenuItem_Click);
            // 
            // minimizeToolStripMenuItem
            // 
            this.minimizeToolStripMenuItem.Enabled = false;
            this.minimizeToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimizeToolStripMenuItem.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.minimizeToolStripMenuItem.Name = "minimizeToolStripMenuItem";
            this.minimizeToolStripMenuItem.Size = new System.Drawing.Size(87, 22);
            this.minimizeToolStripMenuItem.Text = "Minimize";
            this.minimizeToolStripMenuItem.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 378);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 18, 0);
            this.statusStrip1.Size = new System.Drawing.Size(573, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.MarqueeAnimationSpeed = 50;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(693, 15);
            this.StatusLabel.Text = resources.GetString("StatusLabel.Text");
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.companyStatusLabel,
            this.timeStatusLabel});
            this.statusStrip2.Location = new System.Drawing.Point(0, 356);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Padding = new System.Windows.Forms.Padding(1, 0, 18, 0);
            this.statusStrip2.Size = new System.Drawing.Size(573, 22);
            this.statusStrip2.TabIndex = 4;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // companyStatusLabel
            // 
            this.companyStatusLabel.Name = "companyStatusLabel";
            this.companyStatusLabel.Size = new System.Drawing.Size(208, 17);
            this.companyStatusLabel.Text = "Company Details                                     ";
            // 
            // timeStatusLabel
            // 
            this.timeStatusLabel.Name = "timeStatusLabel";
            this.timeStatusLabel.Size = new System.Drawing.Size(379, 15);
            this.timeStatusLabel.Text = "                                                   Time                          " +
    "                                      ";
            // 
            // oTimer
            // 
            this.oTimer.Enabled = true;
            this.oTimer.Interval = 10;
            this.oTimer.Tick += new System.EventHandler(this.oTimer_Tick);
            // 
            // scTransLog
            // 
            this.scTransLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scTransLog.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scTransLog.IsSplitterFixed = true;
            this.scTransLog.Location = new System.Drawing.Point(0, 26);
            this.scTransLog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.scTransLog.Name = "scTransLog";
            this.scTransLog.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scTransLog.Panel1
            // 
            this.scTransLog.Panel1.Controls.Add(this.HeaderPanel);
            // 
            // scTransLog.Panel2
            // 
            this.scTransLog.Panel2.Controls.Add(this.splitContainer1);
            this.scTransLog.Size = new System.Drawing.Size(573, 330);
            this.scTransLog.SplitterDistance = 83;
            this.scTransLog.TabIndex = 10;
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.BackColor = System.Drawing.SystemColors.Info;
            this.HeaderPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.HeaderPanel.Controls.Add(this.scHeader);
            this.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.HeaderPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(573, 83);
            this.HeaderPanel.TabIndex = 8;
            // 
            // scHeader
            // 
            this.scHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scHeader.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scHeader.IsSplitterFixed = true;
            this.scHeader.Location = new System.Drawing.Point(0, 0);
            this.scHeader.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.scHeader.Name = "scHeader";
            // 
            // scHeader.Panel1
            // 
            this.scHeader.Panel1.Controls.Add(this.label1);
            this.scHeader.Panel1.Controls.Add(this.label2);
            this.scHeader.Panel1.Controls.Add(this.txtFileName);
            this.scHeader.Panel1.Controls.Add(this.txtInstance);
            this.scHeader.Panel1.Controls.Add(this.btnImport);
            this.scHeader.Panel1.Controls.Add(this.BtnProcess);
            this.scHeader.Panel1.Controls.Add(this.CmbScenario);
            this.scHeader.Panel1.Controls.Add(this.lblScenario);
            this.scHeader.Panel1.Controls.Add(this.Fromdate);
            this.scHeader.Panel1.Controls.Add(this.lblFromRequestDate);
            // 
            // scHeader.Panel2
            // 
            this.scHeader.Panel2.Controls.Add(this.flpImages);
            this.scHeader.Size = new System.Drawing.Size(573, 83);
            this.scHeader.SplitterDistance = 371;
            this.scHeader.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(32, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "StartTime";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(32, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Processing File";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(132, 36);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(224, 21);
            this.txtFileName.TabIndex = 15;
            // 
            // txtInstance
            // 
            this.txtInstance.Location = new System.Drawing.Point(132, 12);
            this.txtInstance.Name = "txtInstance";
            this.txtInstance.ReadOnly = true;
            this.txtInstance.Size = new System.Drawing.Size(224, 21);
            this.txtInstance.TabIndex = 14;
            // 
            // btnImport
            // 
            this.btnImport.Enabled = false;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnImport.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnImport.Location = new System.Drawing.Point(676, 42);
            this.btnImport.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(88, 23);
            this.btnImport.TabIndex = 13;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Visible = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // BtnProcess
            // 
            this.BtnProcess.Enabled = false;
            this.BtnProcess.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnProcess.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BtnProcess.Location = new System.Drawing.Point(676, 12);
            this.BtnProcess.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnProcess.Name = "BtnProcess";
            this.BtnProcess.Size = new System.Drawing.Size(88, 23);
            this.BtnProcess.TabIndex = 12;
            this.BtnProcess.Text = "Process";
            this.BtnProcess.UseVisualStyleBackColor = true;
            this.BtnProcess.Visible = false;
            this.BtnProcess.Click += new System.EventHandler(this.BtnProcess_Click);
            // 
            // CmbScenario
            // 
            this.CmbScenario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbScenario.Enabled = false;
            this.CmbScenario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbScenario.Location = new System.Drawing.Point(584, 11);
            this.CmbScenario.Margin = new System.Windows.Forms.Padding(4);
            this.CmbScenario.Name = "CmbScenario";
            this.CmbScenario.Size = new System.Drawing.Size(84, 21);
            this.CmbScenario.TabIndex = 6;
            this.CmbScenario.Visible = false;
            // 
            // lblScenario
            // 
            this.lblScenario.AutoSize = true;
            this.lblScenario.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblScenario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScenario.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblScenario.Location = new System.Drawing.Point(518, 16);
            this.lblScenario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblScenario.Name = "lblScenario";
            this.lblScenario.Size = new System.Drawing.Size(57, 13);
            this.lblScenario.TabIndex = 7;
            this.lblScenario.Text = "Scenario";
            this.lblScenario.Visible = false;
            // 
            // Fromdate
            // 
            this.Fromdate.Enabled = false;
            this.Fromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Fromdate.Location = new System.Drawing.Point(584, 39);
            this.Fromdate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Fromdate.Name = "Fromdate";
            this.Fromdate.Size = new System.Drawing.Size(84, 21);
            this.Fromdate.TabIndex = 8;
            this.Fromdate.Visible = false;
            // 
            // lblFromRequestDate
            // 
            this.lblFromRequestDate.AutoSize = true;
            this.lblFromRequestDate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblFromRequestDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromRequestDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFromRequestDate.Location = new System.Drawing.Point(518, 42);
            this.lblFromRequestDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFromRequestDate.Name = "lblFromRequestDate";
            this.lblFromRequestDate.Size = new System.Drawing.Size(58, 13);
            this.lblFromRequestDate.TabIndex = 9;
            this.lblFromRequestDate.Text = "File Date";
            this.lblFromRequestDate.Visible = false;
            // 
            // flpImages
            // 
            this.flpImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpImages.Location = new System.Drawing.Point(0, 0);
            this.flpImages.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.flpImages.Name = "flpImages";
            this.flpImages.Size = new System.Drawing.Size(198, 83);
            this.flpImages.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblRec);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.DgvTxnLogParent);
            this.splitContainer1.Size = new System.Drawing.Size(573, 243);
            this.splitContainer1.SplitterDistance = 323;
            this.splitContainer1.TabIndex = 7;
            // 
            // lblRec
            // 
            this.lblRec.AutoSize = true;
            this.lblRec.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec.ForeColor = System.Drawing.Color.Green;
            this.lblRec.Location = new System.Drawing.Point(12, 17);
            this.lblRec.Name = "lblRec";
            this.lblRec.Size = new System.Drawing.Size(0, 18);
            this.lblRec.TabIndex = 0;
            // 
            // DgvTxnLogParent
            // 
            this.DgvTxnLogParent.AllowUserToAddRows = false;
            this.DgvTxnLogParent.AllowUserToDeleteRows = false;
            this.DgvTxnLogParent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DgvTxnLogParent.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.DgvTxnLogParent.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.DgvTxnLogParent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Image,
            this.DA,
            this.CA,
            this.Amount,
            this.Currency,
            this.Dim2,
            this.Dim3,
            this.D_DocNo,
            this.Status,
            this.ErroCode,
            this.Remarks});
            this.DgvTxnLogParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvTxnLogParent.Enabled = false;
            this.DgvTxnLogParent.Location = new System.Drawing.Point(0, 0);
            this.DgvTxnLogParent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DgvTxnLogParent.MultiSelect = false;
            this.DgvTxnLogParent.Name = "DgvTxnLogParent";
            this.DgvTxnLogParent.ReadOnly = true;
            this.DgvTxnLogParent.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DgvTxnLogParent.Size = new System.Drawing.Size(246, 243);
            this.DgvTxnLogParent.TabIndex = 6;
            this.DgvTxnLogParent.Visible = false;
            this.DgvTxnLogParent.Sorted += new System.EventHandler(this.DgvTxnLogParent_Sorted);
            // 
            // Image
            // 
            this.Image.DataPropertyName = "Image";
            this.Image.HeaderText = "Image";
            this.Image.Name = "Image";
            this.Image.ReadOnly = true;
            this.Image.Visible = false;
            this.Image.Width = 50;
            // 
            // DA
            // 
            this.DA.DataPropertyName = "DA";
            this.DA.HeaderText = "Debit Account";
            this.DA.Name = "DA";
            this.DA.ReadOnly = true;
            this.DA.Width = 111;
            // 
            // CA
            // 
            this.CA.DataPropertyName = "CA";
            this.CA.HeaderText = "Credit Account";
            this.CA.Name = "CA";
            this.CA.ReadOnly = true;
            this.CA.Width = 116;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            this.Amount.FillWeight = 91.83587F;
            this.Amount.HeaderText = "Bill Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 97;
            // 
            // Currency
            // 
            this.Currency.DataPropertyName = "Currency";
            this.Currency.HeaderText = "Currency";
            this.Currency.Name = "Currency";
            this.Currency.ReadOnly = true;
            this.Currency.Width = 85;
            // 
            // Dim2
            // 
            this.Dim2.DataPropertyName = "Dim2";
            this.Dim2.FillWeight = 91.83587F;
            this.Dim2.HeaderText = "Dimension 2";
            this.Dim2.Name = "Dim2";
            this.Dim2.ReadOnly = true;
            this.Dim2.Width = 103;
            // 
            // Dim3
            // 
            this.Dim3.DataPropertyName = "Dim3";
            this.Dim3.FillWeight = 91.83587F;
            this.Dim3.HeaderText = "Dimension 3";
            this.Dim3.Name = "Dim3";
            this.Dim3.ReadOnly = true;
            this.Dim3.Width = 103;
            // 
            // D_DocNo
            // 
            this.D_DocNo.DataPropertyName = "DestinationNo";
            this.D_DocNo.HeaderText = "Dest No";
            this.D_DocNo.Name = "D_DocNo";
            this.D_DocNo.ReadOnly = true;
            this.D_DocNo.Width = 77;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.FillWeight = 91.83587F;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 68;
            // 
            // ErroCode
            // 
            this.ErroCode.DataPropertyName = "ErroCode";
            this.ErroCode.FillWeight = 91.83587F;
            this.ErroCode.HeaderText = "Error Code";
            this.ErroCode.Name = "ErroCode";
            this.ErroCode.ReadOnly = true;
            this.ErroCode.Width = 95;
            // 
            // Remarks
            // 
            this.Remarks.DataPropertyName = "Remarks";
            this.Remarks.FillWeight = 197.9695F;
            this.Remarks.HeaderText = "Remarks(Status)";
            this.Remarks.Name = "Remarks";
            this.Remarks.ReadOnly = true;
            this.Remarks.Width = 129;
            // 
            // niTaskBar
            // 
            this.niTaskBar.Icon = ((System.Drawing.Icon)(resources.GetObject("niTaskBar.Icon")));
            this.niTaskBar.Text = "notifyIcon1";
            this.niTaskBar.Visible = true;
            this.niTaskBar.Click += new System.EventHandler(this.niTaskBar_Click);
            this.niTaskBar.DoubleClick += new System.EventHandler(this.niTaskBar_DoubleClick);
            // 
            // Systemtry
            // 
            this.Systemtry.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.exitToolStripMenuItem1});
            this.Systemtry.Name = "Systemtry";
            this.Systemtry.Size = new System.Drawing.Size(104, 48);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.SystemtryShow_Click);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.SystemtryExit_Click);
            // 
            // TransLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 400);
            this.Controls.Add(this.scTransLog);
            this.Controls.Add(this.statusStrip2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "TransLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transaction Log Status";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TransLog_FormClosing);
            this.Load += new System.EventHandler(this.TransLog_Load);
            this.Resize += new System.EventHandler(this.TransLog_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.scTransLog.Panel1.ResumeLayout(false);
            this.scTransLog.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scTransLog)).EndInit();
            this.scTransLog.ResumeLayout(false);
            this.HeaderPanel.ResumeLayout(false);
            this.scHeader.Panel1.ResumeLayout(false);
            this.scHeader.Panel1.PerformLayout();
            this.scHeader.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scHeader)).EndInit();
            this.scHeader.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvTxnLogParent)).EndInit();
            this.Systemtry.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOffToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel companyStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel timeStatusLabel;
        internal System.Windows.Forms.Timer oTimer;
        private System.Windows.Forms.SplitContainer scTransLog;
        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.DataGridView DgvTxnLogParent;
        private System.Windows.Forms.NotifyIcon niTaskBar;
        private System.Windows.Forms.ContextMenuStrip Systemtry;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.SplitContainer scHeader;
        private System.Windows.Forms.Button BtnProcess;
        private System.Windows.Forms.ComboBox CmbScenario;
        private System.Windows.Forms.Label lblScenario;
        private System.Windows.Forms.DateTimePicker Fromdate;
        private System.Windows.Forms.Label lblFromRequestDate;
        private System.Windows.Forms.FlowLayoutPanel flpImages;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.ToolStripMenuItem minimizeToolStripMenuItem;
        private System.Windows.Forms.DataGridViewImageColumn Image;
        private System.Windows.Forms.DataGridViewTextBoxColumn DA;
        private System.Windows.Forms.DataGridViewTextBoxColumn CA;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Currency;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dim2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dim3;
        private System.Windows.Forms.DataGridViewTextBoxColumn D_DocNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErroCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
        private System.Windows.Forms.TextBox txtFileName;
        public System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblRec;
        public System.Windows.Forms.TextBox txtInstance;
    }
}

