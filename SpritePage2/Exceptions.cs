using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpritePage2
{
	class PageOverflowException : System.Exception
	{
		public PageOverflowException()
			: base()
		{
		}

		public PageOverflowException(string m)
			: base(m)
		{
		}

		public PageOverflowException(string m, Exception iE)
			: base(m, iE)
		{
		}
	} 

	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	class InvalidSizeException : System.Exception
	{
		public InvalidSizeException()
			: base()
		{
		}

		public InvalidSizeException(string m)
			: base(m)
		{
		}

		public InvalidSizeException(string m, Exception iE)
			: base(m, iE)
		{
		}
	}
}
