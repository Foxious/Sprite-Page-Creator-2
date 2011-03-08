using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using TGADotNet;

namespace SpritePage2
{
	static class SpriteGen
	{
		public static Bitmap MakePage(Bitmap[] files, int size, Color bgcolor)
		{
			int xCells = size/files[0].Width;
			int yCells = size/files[0].Height;
			int xSize = files[0].Width;
			int ySize = files[0].Height;

			// see if the bitmap's size is big enough
			if (!((size & size - 1) == 0 && size > 255 && size < 4097))
				throw new ImproperSizeException("Size must be a power of 2 more than 128, less than 8192");

			// make sure we have enough on our page to fit all the sprites
			if ((size / xSize) * (size / ySize) < files.Length)
				throw new PageOverflowException("Too many sprites for this page");

			// make sure we have everything the same size
			foreach (Bitmap b in files)
				if ((b.Width != xSize) || (b.Height != ySize))
					throw new InvalidSizeException("Sprites must all be the same size!");

			// now, copy everything to the sprite page
			Bitmap pageBmp = new Bitmap(size, size, PixelFormat.Format32bppArgb);
			Graphics page = Graphics.FromImage(pageBmp);
			for (int y = 0; y < yCells; y++)
			{
				for (int x = 0; x < xCells; x++)
				{
					// make sure there's actually an image to place here
					if ((y * xCells + x) >= files.Length)
						break;

					Bitmap thisFile = files[y * xCells + x];
					page.DrawImage(thisFile,new Rectangle(x*xSize, y*ySize, xSize, ySize));
					//page.Save();
				}
			}
			return pageBmp;
		}
	}
}
