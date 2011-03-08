using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TGADotNet.Exceptions;

namespace TGADotNet
{
	public class TargaImage
	{
		// M E M B E R S ////////////////////////////////////////////////////////////////////////////////////////////////////////
		private TGAData _tgaData;

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Small class that stores relevant data about a TGA Image
		/// </summary>
		private class TGAData
		{
			public byte[] rawData;	// raw data from File I/O

			// sorted image data
			public byte[] imageData;
			public byte[] colorTable;
			public int width;
			public int height;
			public int rightCorner;
			public int topCorner;
			public int xOrig;
			public int yOrig;
			public int bytesPerChannel;
			public ImageTypes imagetype;
			public int bitsPerAlpha;
		}

		// H E L P E R S ////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Helper Class to read and parse image data from a TGAData class
		/// </summary>
		private class ImageReader
		{
			private TGAData _tgadata;
			private ImageTypes _imagetype;
			private delegate void ColorReader(ref int index);

			private int r;

			// C O N S T R U C T O R ////////////////////////////////////////////////////////////////////////////////////////////
			/// <summary>
			/// Constructor. Parses the imagedata of a TGAData class
			/// </summary>
			/// <param name="tgadata">The TGAData to parse</param>
			/// <param name="imagetype">ImageTypes enum of the sort of image we're deconstructing</param>
			/// <param name="offset">The index of the first entry in the image table</param>
			public ImageReader(TGAData tgadata, int offset)
			{
				_tgadata = tgadata;

				r = 0;

				switch (_tgadata.imagetype)
				{
					case ImageTypes.UncompressedBlackAndWhite:
					case ImageTypes.UncompressedTrueColor:
						ParseUncompressed(ReadFromRaw, offset);
						break;
					case ImageTypes.UncompressedColorMap:
						ParseUncompressed(ReadFromTable, offset);
						break;

					case ImageTypes.RLETrueColor:
						ParseRLE(ReadFromRaw, offset);
						break;
					case ImageTypes.RLEColorMap:
						ParseRLE(ReadFromTable, offset);
						break;
				} // end switch
			}

			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			/// <summary>
			/// reads from the raw targa data at position index
			/// and adds the color referenced from that point 
			/// to the imageData
			/// </summary>
			/// <param name="index">the index in rawdata to read from</param>
			private void ReadFromTable(ref int index)
			{
				int colorIndex = _tgadata.rawData[index++] * _tgadata.bytesPerChannel;
				if (_tgadata.colorTable.Length > 256 * _tgadata.bytesPerChannel)
					colorIndex += _tgadata.rawData[index++] * 256;

				for (int i = 0; i < _tgadata.bytesPerChannel; i++)
					_tgadata.imageData[r++] = _tgadata.colorTable[colorIndex + i];
			}

			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			/// <summary>
			/// Reads a color from raw data into the image data
			/// </summary>
			/// <param name="index">the index of the raw data element</param>
			private void ReadFromRaw(ref int index)
			{
				for (int i = 0; i < _tgadata.bytesPerChannel; i++)
					_tgadata.imageData[r++] = _tgadata.rawData[index++];
			}

			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			/// <summary>
			/// Parses uncompressed image data
			/// </summary>
			/// <param name="readcolor">delegate function for reading a single color from the data</param>
			/// <param name="offset">the place to start reading</param>
			private void ParseUncompressed(ColorReader readcolor, int offset)
			{
				while (_tgadata.imageData.Length > r)
					readcolor(ref offset);
			}

			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			/// <summary>
			/// Scans through image data if it has RLE compression
			/// </summary>
			/// <param name="readcolor">delegate function for reading a single color from the data</param>
			/// <param name="offset">the place to start reading</param>
			private void ParseRLE(ColorReader readcolor, int offset)
			{
				int imageStart = offset;
				while (offset < (_tgadata.imageData.Length + imageStart))
				{
					if (r >= _tgadata.imageData.Length)
					{
						break;
					}
					int id = _tgadata.rawData[offset++];

					bool compressed = (id >= 128);
					if (!compressed)
					{
						// we aren't compressed, so let's just read in the next id+1 bytes and call it a day.
						++id;
						for (int i = 0; i != id; i++)
						{
							readcolor(ref offset);
						}
					}
					else
					{
						// we're compressed. The next id-127 bytes are RLE
						id -= 127;
						for (int i = 0; i != id; i++)
						{
							int rleColor = offset;
							readcolor(ref rleColor);
						}
						offset += _tgadata.bytesPerChannel;
					}
				} // end while
			}
		}

		// C O N S T R U C T O R S //////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Constructor for a TargaImage from a file
		/// </summary>
		/// <param name="filename"> name of the file</param>
		public TargaImage(string filename)
		{
			InitializeTGAImage();
			this.Load(filename);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Constructor for a TargaImage from a .NET Bitmap
		/// </summary>
		/// <param name="bitmap">the .NET Bitmap to load from</param>
		public TargaImage(Bitmap bitmap)
		{
			InitializeTGAImage();
			this.FromBitmap(bitmap);
		}

		// M E T H O D S ////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Initializer for TGAData
		/// </summary>
		private void InitializeTGAImage()
		{
			_tgaData = new TGAData();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Loads all the data from a file into the rawdata
		/// </summary>
		/// <param name="filename">file to load</param>
		private void LoadData(string filename)
		{
			if (!File.Exists(filename))
				throw new FileNotFoundException();

			using (BinaryReader b = new BinaryReader(File.Open(filename, FileMode.Open)))
			{
				if (b.BaseStream.Length != 0)
				{
					_tgaData.rawData = new byte[b.BaseStream.Length];
					int offset = 0;
					int remaining = _tgaData.rawData.Length;

					while (remaining > 0)
					{
						int read = b.Read(_tgaData.rawData, offset, remaining);
						if (read <= 0)
							throw new EndOfStreamException(String.Format("End of stream reached with {0} bytes left", remaining));
						remaining -= read;
						offset += read;
					} // end while
				}
			} // close stram 
		} // end LoadData

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Orients an image based on the right corner and top corner orientations
		/// </summary>
		/// <param name="image">image to flip</param>
		private void OrientImage(Bitmap image)
		{
			if (_tgaData.rightCorner == 0 && _tgaData.topCorner == 0)
				image.RotateFlip(RotateFlipType.Rotate180FlipX);
			else if (_tgaData.rightCorner == 1 && _tgaData.topCorner == 1)
				image.RotateFlip(RotateFlipType.Rotate90FlipNone);
			else if (_tgaData.rightCorner == 1 && _tgaData.topCorner == 0)
				image.RotateFlip(RotateFlipType.RotateNoneFlipXY);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Creates a Bitmap object from TargaImage data
		/// </summary>
		/// <returns>The bitmap that was created</returns>
		public Bitmap AsBitmap()
		{
			byte[] tempImageData;
			PixelFormat pf = new PixelFormat();
			if (ImageTypes.UncompressedBlackAndWhite == _tgaData.imagetype)
			{
				// bitmaps can't do Black and White in the same way.
				tempImageData = new byte[_tgaData.imageData.Length * 2];
				for (int i = 0; i < tempImageData.Length; i++)
					tempImageData[i] = _tgaData.imageData[i / 2];
				pf = PixelFormat.Format16bppGrayScale;
			}
			else
			{
				switch (_tgaData.bytesPerChannel)
				{
					case 2:
						if (0 < _tgaData.bitsPerAlpha)
							pf = PixelFormat.Format16bppArgb1555;
						else
							pf = PixelFormat.Format16bppRgb555;
						break;
					case 3:
						pf = PixelFormat.Format24bppRgb;
						break;
					case 4:
						pf = PixelFormat.Format32bppArgb;
						break;
					default:
						throw new InvalidColorDepth("bytesPerChannel must be 2, 3, or 4");
						break;
				} // end switch
			} // end else

			// makes a bitmap out of the image data extracted from the raw
			Bitmap image = new Bitmap(_tgaData.width, _tgaData.height, pf);
			BitmapData imageData = image.LockBits(new Rectangle(_tgaData.xOrig, _tgaData.yOrig, _tgaData.width, _tgaData.height), ImageLockMode.WriteOnly, pf);
			System.Runtime.InteropServices.Marshal.Copy(_tgaData.imageData, 0, imageData.Scan0, _tgaData.imageData.Length);
			image.UnlockBits(imageData);

			//finally, orient it properly
			OrientImage(image);

			return image;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Creates a TargaImage from a .NET Bitmap
		/// </summary>
		/// <param name="bitmap">the bitmap to use</param>
		public void FromBitmap(Bitmap bitmap)
		{
			_tgaData.width = bitmap.Width;
			_tgaData.height = bitmap.Height;
			_tgaData.xOrig = 0;
			_tgaData.yOrig = 0;
			_tgaData.rightCorner = 0;
			_tgaData.topCorner = 1;
			_tgaData.imagetype = ImageTypes.UncompressedTrueColor;
			int colorDepth;
			switch (bitmap.PixelFormat)
			{
				case PixelFormat.Format16bppRgb555:
					// 16 bit, no alpha
					colorDepth = 16;
					_tgaData.bytesPerChannel = 2;
					_tgaData.bitsPerAlpha = 0;
					break;
				case PixelFormat.Format16bppArgb1555:
					// 16 bit, 1-bit alpha
					_tgaData.bytesPerChannel = 2;
					_tgaData.bitsPerAlpha = 1;
					colorDepth = 16;
					break;
				case PixelFormat.Format24bppRgb:
					// 24 bit, no alpha
					_tgaData.bytesPerChannel = 3;
					_tgaData.bitsPerAlpha = 0;
					colorDepth = 24;
					break;
				case PixelFormat.Format32bppArgb:
				// 32 bit, 8 bit alpha
				case PixelFormat.Canonical:
					// same as above
					_tgaData.bytesPerChannel = 4;
					_tgaData.bitsPerAlpha = 8;
					colorDepth = 32;

					break;
				default:
					throw new System.Exception("Unsupported PixelFormat Exception"); // make this custom later
					break;
			} // end switch

			_tgaData.imageData = new byte[_tgaData.width * _tgaData.height * colorDepth];
			byte[] rawImage = new byte[bitmap.Width * bitmap.Height * colorDepth];
			BitmapData rawImageData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
			System.Runtime.InteropServices.Marshal.Copy(rawImageData.Scan0, _tgaData.imageData, 0, rawImageData.Stride * bitmap.Height);
			bitmap.UnlockBits(rawImageData); // we should now have all our data in the tgaData array
		} // end FromBitmap

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Writes this TargaImage to file
		/// </summary>
		/// <param name="filename">name of the file</param>
		public void Save(string filename)
		{
			// first, write the footer, because it's always the same.
			byte[] footer = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 
										(byte)'T', (byte)'R', (byte)'U', (byte)'E', 
										(byte)'V', (byte)'I', (byte)'S', (byte)'I', (byte)'O', (byte)'N', 
										(byte)'-', (byte)'X', (byte)'F', (byte)'I', (byte)'L', (byte)'E', (byte)'.', 0x00 };

			// now, build the header
			byte[] header = new byte[18];

			// Image Type, only supports Uncompressed True Color ATM
			header[2] = 2;

			// Width
			header[12] = (byte)(_tgaData.width & 0xFF);
			header[13] = (byte)((_tgaData.width >> 8) & 0xFF);
			// Height
			header[14] = (byte)(_tgaData.height & 0xFF);
			header[15] = (byte)((_tgaData.height >> 8) & 0xFF);

			// bits & descriptor
			header[17] = (byte)((_tgaData.topCorner << 5) + (_tgaData.rightCorner << 4) + _tgaData.bitsPerAlpha);
			header[16] = (byte)(_tgaData.bytesPerChannel * 8);

			// now for the writing
			using (BinaryWriter b = new BinaryWriter(File.Open(filename, FileMode.Create)))
			{
				b.Write(header);
				b.Write(_tgaData.imageData);
				b.Write(footer);
			}

		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Loads a TGA from a file
		/// </summary>
		/// <param name="filename"></param>
		public void Load(string filename)
		{
			LoadData(filename);
			byte[] ImageID = new byte[_tgaData.rawData[0]];
			for (int i = 0; i < ImageID.Length; i++)
			{
				ImageID[i] = _tgaData.rawData[18 + i];
			}

			_tgaData.imagetype = (ImageTypes)_tgaData.rawData[2];

			// it's possible for rawData[7] to have 15 bytes, but we still want it to return 2 in this case.
			int cTableBytes = (int)Math.Ceiling(Convert.ToDouble(_tgaData.rawData[7]) / 8.0);
			byte[] colorTable = new byte[_tgaData.rawData[1] * (_tgaData.rawData[5] + _tgaData.rawData[6] * 256) * cTableBytes];
			_tgaData.width = _tgaData.rawData[12] + _tgaData.rawData[13] * 256;
			_tgaData.height = _tgaData.rawData[14] + _tgaData.rawData[15] * 256;
			int _xOrig = _tgaData.rawData[8] + _tgaData.rawData[9] * 256;
			int _yOrig = _tgaData.rawData[10] + _tgaData.rawData[11] * 256;

			// set up the orientation for later
			_tgaData.rightCorner = (byte)Origin.rightCorner & _tgaData.rawData[17];
			_tgaData.topCorner = (byte)Origin.topCorner & _tgaData.rawData[17];

			// where the actual bytes per channel is stored depends on if we're color mapped or not.
			_tgaData.bytesPerChannel = _tgaData.rawData[1] != 0 ? cTableBytes : _tgaData.rawData[16] / 8;
			_tgaData.imageData = new byte[_tgaData.bytesPerChannel * _tgaData.width * _tgaData.height];

			// record the number of alpha bits. It's either 1 or 8 if it's anything.
			_tgaData.bitsPerAlpha = _tgaData.rawData[17] & 0x09;

			// all of the image types above 8 are RLE images
			bool RLE = ((int)_tgaData.imagetype > 8);
			bool trueColor = (((int)_tgaData.imagetype & 0x02) == 0x02);


			// We've gone through the header. Now to prep all the info and
			// parse it into an image

			// imageOffset is the point in the file the actual image data starts
			int imageOffset = ImageID.Length + colorTable.Length + 18;
			// offset is used to keep track of our position reading the image data
			int offset = imageOffset;
			// value to make sure we don't read past the image data into the footer
			int numPixels = _tgaData.width * _tgaData.height;

			ImageReader imageReader;
			switch (_tgaData.imagetype)
			{
				case ImageTypes.UncompressedColorMap:
					// where the color table starts
					int cMapOffset = 18 + (_tgaData.rawData[3] + _tgaData.rawData[4] * 256);

					// populate the colorTable
					for (int i = 0; i < colorTable.Length; i++)
						colorTable[i] = _tgaData.rawData[i + cMapOffset];

					int cMapSize = colorTable.Length;
					_tgaData.colorTable = colorTable;
					// load the image data from the colorTable
					imageReader = new ImageReader(_tgaData, offset);
					break;
				case ImageTypes.UncompressedTrueColor:
					imageReader = new ImageReader(_tgaData, offset);
					break;

				case ImageTypes.UncompressedBlackAndWhite:
					imageReader = new ImageReader(_tgaData, offset);
					break;

				case ImageTypes.RLETrueColor:
					imageReader = new ImageReader(_tgaData, offset);
					break;
			} // end switch

			// we don't need the raw data anymore.
			_tgaData.rawData = new byte[0];
		} // end Load

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	}
}