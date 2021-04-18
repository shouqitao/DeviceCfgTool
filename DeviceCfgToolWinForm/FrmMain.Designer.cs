namespace DeviceCfgToolWinForm {
    partial class FrmMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnModifyChannalName = new System.Windows.Forms.Button();
            this.btmModifyDeviceName = new System.Windows.Forms.Button();
            this.btnRebootDevice = new System.Windows.Forms.Button();
            this.btnImportDevices = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(800, 368);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnModifyChannalName);
            this.groupBox1.Controls.Add(this.btmModifyDeviceName);
            this.groupBox1.Controls.Add(this.btnRebootDevice);
            this.groupBox1.Controls.Add(this.btnImportDevices);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 374);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 76);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作区";
            // 
            // btnModifyChannalName
            // 
            this.btnModifyChannalName.Location = new System.Drawing.Point(255, 32);
            this.btnModifyChannalName.Name = "btnModifyChannalName";
            this.btnModifyChannalName.Size = new System.Drawing.Size(75, 23);
            this.btnModifyChannalName.TabIndex = 3;
            this.btnModifyChannalName.Text = "修改通道名称";
            this.btnModifyChannalName.UseVisualStyleBackColor = true;
            // 
            // btmModifyDeviceName
            // 
            this.btmModifyDeviceName.Location = new System.Drawing.Point(174, 32);
            this.btmModifyDeviceName.Name = "btmModifyDeviceName";
            this.btmModifyDeviceName.Size = new System.Drawing.Size(75, 23);
            this.btmModifyDeviceName.TabIndex = 2;
            this.btmModifyDeviceName.Text = "修改设备名称";
            this.btmModifyDeviceName.UseVisualStyleBackColor = true;
            // 
            // btnRebootDevice
            // 
            this.btnRebootDevice.Location = new System.Drawing.Point(93, 32);
            this.btnRebootDevice.Name = "btnRebootDevice";
            this.btnRebootDevice.Size = new System.Drawing.Size(75, 23);
            this.btnRebootDevice.TabIndex = 1;
            this.btnRebootDevice.Text = "重启设备";
            this.btnRebootDevice.UseVisualStyleBackColor = true;
            this.btnRebootDevice.Click += new System.EventHandler(this.btnRebootDevice_Click);
            // 
            // btnImportDevices
            // 
            this.btnImportDevices.Location = new System.Drawing.Point(12, 32);
            this.btnImportDevices.Name = "btnImportDevices";
            this.btnImportDevices.Size = new System.Drawing.Size(75, 23);
            this.btnImportDevices.TabIndex = 0;
            this.btnImportDevices.Text = "导入设备";
            this.btnImportDevices.UseVisualStyleBackColor = true;
            this.btnImportDevices.Click += new System.EventHandler(this.btnImportDevices_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gridControl1);
            this.Name = "FrmMain";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnImportDevices;
        private System.Windows.Forms.Button btnModifyChannalName;
        private System.Windows.Forms.Button btmModifyDeviceName;
        private System.Windows.Forms.Button btnRebootDevice;
    }
}

