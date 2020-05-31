using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TypeLite.Demo.Models {
	/// <summary>
	/// Customer information
	/// </summary>
	[TsClass(Module = "Eshop")]
	public class Customer {
		/// <summary>
		/// Identifier
		/// </summary>
		[TsIgnore]
		public int ID { get; set; }
		/// <summary>
		/// Customer name.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Email address.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Customer is VIP member or not.
		/// </summary>
		[TsProperty(Name = "VIP")]
		public bool IsVIP { get; set; }

		/// <summary>
		/// Customer's kind.
		/// </summary>
		public CustomerKind Kind { get; set; }

		/// <summary>
		/// Customer's orders.
		/// </summary>
		public IEnumerable<Order> Orders { get; set; }
	}
}