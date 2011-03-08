namespace SpritePage2
{
	partial class formSPC
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
			this.lvwSprites = new System.Windows.Forms.ListView();
			this.num = new System.Windows.Forms.ColumnHeader();
			this.File = new System.Windows.Forms.ColumnHeader();
			this.Size = new System.Windows.Forms.ColumnHeader();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnGenerate = new System.Windows.Forms.Button();
			this.cbxWidth = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btn_Up = new System.Windows.Forms.Button();
			this.btn_Down = new System.Windows.Forms.Button();
			this.btnPreview = new System.Windows.Forms.Button();
			this.btn_Reverse = new System.Windows.Forms.Button();
			this.lbl_X = new System.Windows.Forms.Label();
			this.cbxHeight = new System.Windows.Forms.ComboBox();
			this.chkSquare = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// lvwSprites
			// 
			this.lvwSprites.AllowColumnReorder = true;
			this.lvwSprites.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.num,
            this.File,
            this.Size});
			this.lvwSprites.FullRowSelect = true;
			this.lvwSprites.GridLines = true;
			this.lvwSprites.Location = new System.Drawing.Point(26, 23);
			this.lvwSprites.MultiSelect = false;
			this.lvwSprites.Name = "lvwSprites";
			this.lvwSprites.Size = new System.Drawing.Size(299, 277);
			this.lvwSprites.TabIndex = 0;
			this.lvwSprites.UseCompatibleStateImageBehavior = false;
			this.lvwSprites.View = System.Windows.Forms.View.Details;
			// 
			// num
			// 
			this.num.Text = "#";
			this.num.Width = 28;
			// 
			// File
			// 
			this.File.Text = "File";
			this.File.Width = 160;
			// 
			// Size
			// 
			this.Size.Text = "Size";
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(331, 23);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(75, 23);
			this.btnAdd.TabIndex = 1;
			this.btnAdd.Text = "Add";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(331, 52);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(75, 23);
			this.btnRemove.TabIndex = 2;
			this.btnRemove.Text = "Remove";
			this.btnRemove.UseVisualStyleBackColor = true;
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(331, 81);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(75, 23);
			this.btnClear.TabIndex = 3;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnGenerate
			// 
			this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGenerate.Location = new System.Drawing.Point(171, 371);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.Size = new System.Drawing.Size(75, 23);
			this.btnGenerate.TabIndex = 8;
			this.btnGenerate.Text = "Generate!";
			this.btnGenerate.UseVisualStyleBackColor = true;
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			// 
			// cbxWidth
			// 
			this.cbxWidth.FormattingEnabled = true;
			this.cbxWidth.Items.AddRange(new object[] {
            "256",
            "512",
            "1024",
            "2048",
            "4096"});
			this.cbxWidth.Location = new System.Drawing.Point(59, 310);
			this.cbxWidth.Name = "cbxWidth";
			this.cbxWidth.Size = new System.Drawing.Size(84, 21);
			this.cbxWidth.TabIndex = 7;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(23, 313);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(30, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Size:";
			// 
			// btn_Up
			// 
			this.btn_Up.Location = new System.Drawing.Point(331, 138);
			this.btn_Up.Name = "btn_Up";
			this.btn_Up.Size = new System.Drawing.Size(75, 23);
			this.btn_Up.TabIndex = 5;
			this.btn_Up.Text = "Up";
			this.btn_Up.UseVisualStyleBackColor = true;
			this.btn_Up.Click += new System.EventHandler(this.btn_Up_Click);
			// 
			// btn_Down
			// 
			this.btn_Down.Location = new System.Drawing.Point(331, 167);
			this.btn_Down.Name = "btn_Down";
			this.btn_Down.Size = new System.Drawing.Size(75, 23);
			this.btn_Down.TabIndex = 6;
			this.btn_Down.Text = "Down";
			this.btn_Down.UseVisualStyleBackColor = true;
			this.btn_Down.Click += new System.EventHandler(this.btn_Down_Click);
			// 
			// btnPreview
			// 
			this.btnPreview.Location = new System.Drawing.Point(331, 264);
			this.btnPreview.Name = "btnPreview";
			this.btnPreview.Size = new System.Drawing.Size(75, 23);
			this.btnPreview.TabIndex = 15;
			this.btnPreview.Text = "Preview";
			this.btnPreview.UseVisualStyleBackColor = true;
			this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
			// 
			// btn_Reverse
			// 
			this.btn_Reverse.Location = new System.Drawing.Point(331, 196);
			this.btn_Reverse.Name = "btn_Reverse";
			this.btn_Reverse.Size = new System.Drawing.Size(75, 23);
			this.btn_Reverse.TabIndex = 16;
			this.btn_Reverse.Text = "Reverse";
			this.btn_Reverse.UseVisualStyleBackColor = true;
			this.btn_Reverse.Click += new System.EventHandler(this.btn_Reverse_Click);
			// 
			// lbl_X
			// 
			this.lbl_X.AutoSize = true;
			this.lbl_X.Location = new System.Drawing.Point(153, 312);
			this.lbl_X.Name = "lbl_X";
			this.lbl_X.Size = new System.Drawing.Size(12, 13);
			this.lbl_X.TabIndex = 17;
			this.lbl_X.Text = "x";
			// 
			// cbxHeight
			// 
			this.cbxHeight.Enabled = false;
			this.cbxHeight.FormattingEnabled = true;
			this.cbxHeight.Items.AddRange(new object[] {
            "256",
            "512",
            "1024",
            "2048",
            "4096"});
			this.cbxHeight.Location = new System.Drawing.Point(171, 310);
			this.cbxHeight.Name = "cbxHeight";
			this.cbxHeight.Size = new System.Drawing.Size(84, 21);
			this.cbxHeight.TabIndex = 18;
			// 
			// chkSquare
			// 
			this.chkSquare.AutoSize = true;
			this.chkSquare.Checked = true;
			this.chkSquare.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSquare.Location = new System.Drawing.Point(263, 312);
			this.chkSquare.Name = "chkSquare";
			this.chkSquare.Size = new System.Drawing.Size(62, 17);
			this.chkSquare.TabIndex = 19;
			this.chkSquare.Text = "Uniform";
			this.chkSquare.UseVisualStyleBackColor = true;
			this.chkSquare.CheckedChanged += new System.EventHandler(this.chkSquare_CheckedChanged);
			// 
			// formSPC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(421, 406);
			this.Controls.Add(this.chkSquare);
			this.Controls.Add(this.cbxHeight);
			this.Controls.Add(this.lbl_X);
			this.Controls.Add(this.btn_Reverse);
			this.Controls.Add(this.btnPreview);
			this.Controls.Add(this.btn_Down);
			this.Controls.Add(this.btn_Up);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cbxWidth);
			this.Controls.Add(this.btnGenerate);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.lvwSprites);
			this.MaximumSize = new System.Drawing.Size(437, 444);
			this.MinimumSize = new System.Drawing.Size(437, 444);
			this.Name = "formSPC";
			this.Text = "Sprite Page Creator 2.0";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView lvwSprites;
		private System.Windows.Forms.ColumnHeader File;
		private System.Windows.Forms.ColumnHeader Size;
		private System.Windows.Forms.ColumnHeader num;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnGenerate;
		private System.Windows.Forms.ComboBox cbxWidth;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btn_Up;
		private System.Windows.Forms.Button btn_Down;
		private System.Windows.Forms.Button btnPreview;
		private System.Windows.Forms.Button btn_Reverse;
		private System.Windows.Forms.Label lbl_X;
		private System.Windows.Forms.ComboBox cbxHeight;
		private System.Windows.Forms.CheckBox chkSquare;


	}
}

