using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGADotNet
{
	struct TargaContants
	{
		public static readonly long SignaturePosition = -18;
		public static readonly int SignatureSize = 16;

		public static readonly bool bottomLeft = false;
		public static readonly bool upRight = true;

	} // end enum

	[Flags]
	enum Origin
	{
		rightCorner = 0x10,
		topCorner = 0x20,
	} // end enum

	enum ImageTypes
	{
		NoImageData = 0,
		UncompressedColorMap = 1,
		UncompressedTrueColor = 2,
		UncompressedBlackAndWhite = 3,
		RLEColorMap = 9,
		RLETrueColor = 10,
	}

	enum PixelDepth
	{
		Color16 = 16,
		Color24 = 24,
		Color32 = 32,
	}
}