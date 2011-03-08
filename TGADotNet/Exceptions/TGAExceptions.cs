using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGADotNet.Exceptions
{
	class InvalidFileException : System.Exception
	{
		public InvalidFileException()
			: base()
		{
		}

		public InvalidFileException(string m)
			: base(m)
		{
		}

		public InvalidFileException(string m, Exception iE)
			: base(m, iE)
		{
		}
	}
	
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	class InvalidColorDepth : System.Exception
	{
		public InvalidColorDepth()
			: base()
		{
		}

		public InvalidColorDepth(string m)
			: base(m)
		{
		}

		public InvalidColorDepth(string m, Exception iE)
			: base(m, iE)
		{
		}
	}
}
