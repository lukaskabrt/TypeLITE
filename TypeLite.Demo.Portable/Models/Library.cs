using System;
using System.Collections.Generic;
using System.Linq;

namespace TypeLite.Demo.Portable.Models {
	/// <summary>
	/// book library
	/// </summary>
	[TsClass(Module = "Library")]
	public class Library {
		/// <summary>
		/// name
		/// </summary>
        public string Name { get; set; }

		/// <summary>
		/// books in library
		/// </summary>
        public IEnumerable<Book> Books { get; set; }
	}
}