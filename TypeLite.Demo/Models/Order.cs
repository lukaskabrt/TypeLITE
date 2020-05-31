using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TypeLite.Demo.Models {
	/// <summary>
	/// a order
	/// </summary>
	[TsClass(Module = "Eshop")]
	public class Order {
		/// <summary>
		/// products
		/// </summary>
		public Product[] Products { get; set; }
		/// <summary>
		/// total price
		/// </summary>
		public decimal TotalPrice { get; set; }
		/// <summary>
		/// created date
		/// </summary>
		public DateTime Created { get; set; }
		/// <summary>
		/// shipped date
		/// </summary>
        public DateTimeOffset Shipped { get; set; }
	}
}