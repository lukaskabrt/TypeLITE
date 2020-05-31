using System;
using System.Collections.Generic;
using System.Linq;

namespace TypeLite.Demo.Portable.Models {
	/// <summary>
	/// Book
	/// </summary>
	[TsClass(Module = "Library")]
	public class Book {
		/// <summary>
		/// ID
		/// </summary>
		[TsIgnore]
		public int ID { get; set; }

		/// <summary>
		/// name
		/// </summary>
        [TsProperty(Name="Title")]
		public string Name { get; set; }
		/// <summary>
		/// pages of book
		/// </summary>
		public int Pages { get; set; }

		/// <summary>
		/// genre
		/// </summary>
		public Genre Genre { get; set; }
	}
}