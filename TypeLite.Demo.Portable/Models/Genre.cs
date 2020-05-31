using System;
using System.Collections.Generic;
using System.Linq;

namespace TypeLite.Demo.Portable.Models {
	/// <summary>
	/// genre definitions
	/// </summary>
	[TsEnum(Module = "Library")]
	public enum Genre {
		/// <summary>
		/// science fiction
		/// </summary>
		Scifi = 1,
		/// <summary>
		/// course book
		/// </summary>
        Coursebook = 2
	}
}