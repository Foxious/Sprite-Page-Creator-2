namespace SpritePage2
{
	partial class ImagePreview
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
			this.imgPreview = new System.Windows.Forms.PictureBox();
			this.btn_Save = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.imgPreview)).BeginInit();
			this.SuspendLayout();
			// 
			// imgPreview
			// 
			this.imgPreview.Location = new System.Drawing.Point(1, 2);
			this.imgPreview.Name = "imgPreview";
			this.imgPreview.Size = new System.Drawing.Size(100, 50);
			this.imgPreview.TabIndex = 0;
			this.imgPreview.TabStop = false;
			// 
			// btn_Save
			// 
			this.btn_Save.Location = new System.Drawing.Point(101, 229);
			this.btn_Save.Name = "btn_Save";
			this.btn_Save.Size = new System.Drawing.Size(75, 23);
			this.btn_Save.TabIndex = 1;
			this.btn_Save.Text = "Save Image";
			this.btn_Save.UseVisualStyleBackColor = true;
			this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
			// 
			// ImagePreview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 264);
			this.Controls.Add(this.btn_Save);
			this.Controls.Add(this.imgPreview);
			this.Name = "ImagePreview";
			this.Text = "ImagePreview";
			((System.ComponentModel.ISupportInitialize)(this.imgPreview)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox imgPreview;
		private System.Windows.Forms.Button btn_Save;
	}
}