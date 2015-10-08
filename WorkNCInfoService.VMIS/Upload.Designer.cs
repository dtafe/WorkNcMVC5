namespace WorkNCInfoService.VMIS
{
    partial class frmUpload
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
            this.lblMachineNm = new System.Windows.Forms.Label();
            this.lblMachineDate = new System.Windows.Forms.Label();
            this.lblOperatorNm = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cboMachineNm = new System.Windows.Forms.ComboBox();
            this.cboOperatorNm = new System.Windows.Forms.ComboBox();
            this.dtpMachineDate = new System.Windows.Forms.DateTimePicker();
            this.txtWorkZoneName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMachineNm
            // 
            this.lblMachineNm.AutoSize = true;
            this.lblMachineNm.Location = new System.Drawing.Point(91, 95);
            this.lblMachineNm.Name = "lblMachineNm";
            this.lblMachineNm.Size = new System.Drawing.Size(79, 13);
            this.lblMachineNm.TabIndex = 0;
            this.lblMachineNm.Text = "Machine Name";
            // 
            // lblMachineDate
            // 
            this.lblMachineDate.AutoSize = true;
            this.lblMachineDate.Location = new System.Drawing.Point(91, 127);
            this.lblMachineDate.Name = "lblMachineDate";
            this.lblMachineDate.Size = new System.Drawing.Size(74, 13);
            this.lblMachineDate.TabIndex = 1;
            this.lblMachineDate.Text = "Machine Date";
            // 
            // lblOperatorNm
            // 
            this.lblOperatorNm.AutoSize = true;
            this.lblOperatorNm.Location = new System.Drawing.Point(89, 163);
            this.lblOperatorNm.Name = "lblOperatorNm";
            this.lblOperatorNm.Size = new System.Drawing.Size(79, 13);
            this.lblOperatorNm.TabIndex = 2;
            this.lblOperatorNm.Text = "Operator Name";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(125, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(229, 31);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Welcome to VMIS";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(198, 201);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 4;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(279, 201);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cboMachineNm
            // 
            this.cboMachineNm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMachineNm.FormattingEnabled = true;
            this.cboMachineNm.Location = new System.Drawing.Point(192, 95);
            this.cboMachineNm.Name = "cboMachineNm";
            this.cboMachineNm.Size = new System.Drawing.Size(223, 21);
            this.cboMachineNm.TabIndex = 6;
            // 
            // cboOperatorNm
            // 
            this.cboOperatorNm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOperatorNm.FormattingEnabled = true;
            this.cboOperatorNm.Location = new System.Drawing.Point(192, 160);
            this.cboOperatorNm.Name = "cboOperatorNm";
            this.cboOperatorNm.Size = new System.Drawing.Size(223, 21);
            this.cboOperatorNm.TabIndex = 7;
            // 
            // dtpMachineDate
            // 
            this.dtpMachineDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpMachineDate.Location = new System.Drawing.Point(192, 127);
            this.dtpMachineDate.Name = "dtpMachineDate";
            this.dtpMachineDate.Size = new System.Drawing.Size(115, 20);
            this.dtpMachineDate.TabIndex = 8;
            // 
            // txtWorkZoneName
            // 
            this.txtWorkZoneName.Location = new System.Drawing.Point(192, 61);
            this.txtWorkZoneName.Name = "txtWorkZoneName";
            this.txtWorkZoneName.ReadOnly = true;
            this.txtWorkZoneName.Size = new System.Drawing.Size(162, 20);
            this.txtWorkZoneName.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(91, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Work Zone Name";
            // 
            // frmUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(513, 236);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtWorkZoneName);
            this.Controls.Add(this.dtpMachineDate);
            this.Controls.Add(this.cboOperatorNm);
            this.Controls.Add(this.cboMachineNm);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblOperatorNm);
            this.Controls.Add(this.lblMachineDate);
            this.Controls.Add(this.lblMachineNm);
            this.DoubleBuffered = true;
            this.Name = "frmUpload";
            this.Load += new System.EventHandler(this.Upload_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMachineNm;
        private System.Windows.Forms.Label lblMachineDate;
        private System.Windows.Forms.Label lblOperatorNm;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cboMachineNm;
        private System.Windows.Forms.ComboBox cboOperatorNm;
        private System.Windows.Forms.DateTimePicker dtpMachineDate;
        private System.Windows.Forms.TextBox txtWorkZoneName;
        private System.Windows.Forms.Label label1;
    }
}

