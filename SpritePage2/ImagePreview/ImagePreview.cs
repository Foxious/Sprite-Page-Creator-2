using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TGADotNet;

namespace SpritePage2
{
	
	public partial class ImagePreview : Form
	{
		private const int MARGIN = 16;
		private Bitmap _img;

		public ImagePreview()
		{
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Adds an image to the previewer and displays it
		/// </summary>
		/// <param name="bitmap">Bitmap to preview</param>
		public void PreviewImage (Bitmap bitmap)
		{
			_img = bitmap;
			Bitmap pImg;

			// see if we need to resize it
			if (bitmap.Width > 512 || bitmap.Height > 512)
			{
				// figure out the scale delta
				int delta = bitmap.Width > bitmap.Height ? bitmap.Width - 512 : bitmap.Height - 512;
				
				// now resize the image to fit the window
				pImg = new Bitmap(bitmap.Width-delta, bitmap.Height-delta, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			}
			else
			{
				// it's 512 or smaller, no need to resize
				pImg = new Bitmap(bitmap.Width, bitmap.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			}

			// give it a black background
			Graphics g = Graphics.FromImage(pImg);
			g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, pImg.Width, pImg.Height));
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(_img, 0, 0, pImg.Width, pImg.Height);
			
			// finally, generate the preview
			imgPreview.Size = pImg.Size;
			imgPreview.Image = pImg;
			imgPreview.Location = new Point(MARGIN / 2, MARGIN / 2);
			btn_Save.Location = new Point((pImg.Width + MARGIN) / 2 - btn_Save.Width / 2, pImg.Height + MARGIN + btn_Save.Height);
			this.ClientSize = new Size((pImg.Width + MARGIN), btn_Save.Height + MARGIN + btn_Save.Location.Y);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		
		private void btn_Save_Click(object sender, EventArgs e)
		{
			SaveFileDialog save = new SaveFileDialog();
			save.Filter = "Targa Files (.tga)|*.tga|PNG Files (.png)|*.png";
			save.AddExtension= true;
			if (save.ShowDialog() == DialogResult.OK)
			{
				if (save.FilterIndex == 1)
					//_img.BitmapToTGA(save.FileName);
					new TargaImage(_img).Save(save.FileName);
				else
					_img.Save(save.FileName);
			}
		}
	}
}
