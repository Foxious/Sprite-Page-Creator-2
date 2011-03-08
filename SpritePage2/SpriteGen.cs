using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using TGADotNet;

namespace SpritePage2
{
	class SpriteGen
	{
		private List<Bitmap> _images;

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Initializes a SpriteGen object from a collection of Bitmaps
		/// </summary>
		/// <param name="images">the list of images to initialize</param>
		public SpriteGen(IEnumerable<Bitmap> images)
		{
			_images = images.ToList();
		}

		/// <summary>
		/// Initializes a SpriteGEn object from a collection of bitmap files
		/// </summary>
		/// <param name="imageFiles">the files to load from bitmaps</param>
		public SpriteGen(IEnumerable<String> imageFiles)
		{
			_images = new List<Bitmap>();
			_images.AddRange((from i in imageFiles select new Bitmap(i)).ToList());
		}

		public Bitmap MakePage(int width, int height)
		{

			int xCells = width / _images[0].Width;
			int yCells = height / _images[0].Height;
			int xSize = _images[0].Width;
			int ySize = _images[0].Height;

			if (TestSize(width) || TestSize(height))
				throw new InvalidSizeException("Size must be a power of 2 more than 128, less than 8192");

			// make sure we have enough on our page to fit all the sprites
			int numSprites = (width / xSize) * (height / ySize);
			if ( numSprites < _images.Count)
				throw new PageOverflowException(String.Format("Page can only hold {0} sprites of this size, got {1}", numSprites,_images.Count));

			// make sure we have everything the same size
			foreach (Bitmap b in _images)
				if ((b.Width != xSize) || (b.Height != ySize))
					throw new InvalidSizeException("Sprites must all be the same size!");

			// now, copy everything to the sprite page
			Bitmap pageBmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
			Graphics page = Graphics.FromImage(pageBmp);
			for (int y = 0; y < yCells; y++)
			{
				for (int x = 0; x < xCells; x++)
				{
					// make sure there's actually an image to place here
					if ((y * xCells + x) >= _images.Count)
						break;

					Bitmap thisFile = _images[y * xCells + x];
					page.DrawImage(thisFile, new Rectangle(x * xSize, y * ySize, xSize, ySize));
					//page.Save();
				}
			}
			return pageBmp;
		}

		private bool TestSize(int size)
		{
			return (!((size & size - 1) == 0 && size > 255 && size < 4097));
		}
	}
}
