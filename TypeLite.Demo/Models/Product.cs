using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TypeLite.Demo.Models {
	/// <summary>
	/// product
	/// </summary>
	[TsClass(Module = "Eshop")]
	public class Product {
		/// <summary>
		/// name
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// price
		/// </summary>
		public decimal Price { get; set; }
		/// <summary>
		/// ID
		/// </summary>
		public Guid ID { get; set; }
	}
}