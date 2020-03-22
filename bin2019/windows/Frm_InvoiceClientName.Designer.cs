namespace Bin2019.windows
{
	partial class Frm_InvoiceClientName
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
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.te_cuname = new DevExpress.XtraEditors.TextEdit();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.te_cuid = new DevExpress.XtraEditors.TextEdit();
			this.b_exit = new DevExpress.XtraEditors.SimpleButton();
			this.b_ok = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.te_cuname.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.te_cuid.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// labelControl1
			// 
			this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelControl1.Appearance.Options.UseFont = true;
			this.labelControl1.Location = new System.Drawing.Point(30, 26);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(87, 18);
			this.labelControl1.TabIndex = 1;
			this.labelControl1.Text = "交款人(单位)";
			// 
			// te_cuname
			// 
			this.te_cuname.Location = new System.Drawing.Point(143, 24);
			this.te_cuname.Name = "te_cuname";
			this.te_cuname.Size = new System.Drawing.Size(346, 24);
			this.te_cuname.TabIndex = 2;
			// 
			// labelControl2
			// 
			this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelControl2.Appearance.Options.UseFont = true;
			this.labelControl2.Location = new System.Drawing.Point(30, 73);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(105, 18);
			this.labelControl2.TabIndex = 3;
			this.labelControl2.Text = "交款人身份证号";
			// 
			// te_cuid
			// 
			this.te_cuid.Location = new System.Drawing.Point(143, 70);
			this.te_cuid.Name = "te_cuid";
			this.te_cuid.Size = new System.Drawing.Size(346, 24);
			this.te_cuid.TabIndex = 4;
			// 
			// b_exit
			// 
			this.b_exit.Appearance.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.b_exit.Appearance.ForeColor = System.Drawing.Color.White;
			this.b_exit.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			this.b_exit.Appearance.Options.UseBackColor = true;
			this.b_exit.Appearance.Options.UseForeColor = true;
			this.b_exit.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.b_exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.b_exit.Location = new System.Drawing.Point(155, 124);
			this.b_exit.Name = "b_exit";
			this.b_exit.Size = new System.Drawing.Size(121, 31);
			this.b_exit.TabIndex = 61;
			this.b_exit.Text = "退出";
			this.b_exit.Click += new System.EventHandler(this.b_exit_Click);
			// 
			// b_ok
			// 
			this.b_ok.Appearance.BackColor = System.Drawing.Color.Lime;
			this.b_ok.Appearance.ForeColor = System.Drawing.Color.White;
			this.b_ok.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			this.b_ok.Appearance.Options.UseBackColor = true;
			this.b_ok.Appearance.Options.UseForeColor = true;
			this.b_ok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.b_ok.Location = new System.Drawing.Point(28, 124);
			this.b_ok.Name = "b_ok";
			this.b_ok.Size = new System.Drawing.Size(121, 31);
			this.b_ok.TabIndex = 62;
			this.b_ok.Text = "确定";
			this.b_ok.Click += new System.EventHandler(this.b_ok_Click);
			// 
			// Frm_InvoiceClientName
			// 
			this.AcceptButton = this.b_ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.b_exit;
			this.ClientSize = new System.Drawing.Size(515, 174);
			this.Controls.Add(this.b_exit);
			this.Controls.Add(this.b_ok);
			this.Controls.Add(this.te_cuid);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.te_cuname);
			this.Controls.Add(this.labelControl1);
			this.Name = "Frm_InvoiceClientName";
			this.Text = "交款人信息";
			this.Load += new System.EventHandler(this.Frm_InvoiceClientName_Load);
			((System.ComponentModel.ISupportInitialize)(this.te_cuname.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.te_cuid.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.TextEdit te_cuname;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.TextEdit te_cuid;
		private DevExpress.XtraEditors.SimpleButton b_exit;
		private DevExpress.XtraEditors.SimpleButton b_ok;
	}
}