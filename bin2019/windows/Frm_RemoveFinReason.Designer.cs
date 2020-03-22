namespace Bin2019.windows
{
	partial class Frm_RemoveFinReason
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
			this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
			this.b_ok = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// memoEdit1
			// 
			this.memoEdit1.Location = new System.Drawing.Point(13, 12);
			this.memoEdit1.Name = "memoEdit1";
			this.memoEdit1.Size = new System.Drawing.Size(598, 172);
			this.memoEdit1.TabIndex = 0;
			// 
			// b_ok
			// 
			this.b_ok.Appearance.BackColor = System.Drawing.Color.Lime;
			this.b_ok.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.b_ok.Appearance.ForeColor = System.Drawing.Color.White;
			this.b_ok.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			this.b_ok.Appearance.Options.UseBackColor = true;
			this.b_ok.Appearance.Options.UseFont = true;
			this.b_ok.Appearance.Options.UseForeColor = true;
			this.b_ok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.b_ok.Location = new System.Drawing.Point(13, 199);
			this.b_ok.Name = "b_ok";
			this.b_ok.Size = new System.Drawing.Size(598, 31);
			this.b_ok.TabIndex = 72;
			this.b_ok.Text = "确定";
			this.b_ok.Click += new System.EventHandler(this.b_ok_Click);
			// 
			// Frm_RemoveFinReason
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(623, 242);
			this.Controls.Add(this.b_ok);
			this.Controls.Add(this.memoEdit1);
			this.Name = "Frm_RemoveFinReason";
			this.Text = "作废原因";
			this.Load += new System.EventHandler(this.Frm_RemoveFinReason_Load);
			((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.MemoEdit memoEdit1;
		private DevExpress.XtraEditors.SimpleButton b_ok;
	}
}