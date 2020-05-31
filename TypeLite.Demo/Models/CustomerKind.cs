using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TypeLite.Demo.Models {
	/// <summary>
	/// Customer Kind
	/// </summary>
	[TsEnum(Module = "Eshop")]
	public enum CustomerKind {
		/// <summary>
		/// Corporate customer
		/// </summary>
		Corporate = 1,
		/// <summary>
		/// Individual customer
		/// </summary>
		Individual = 2
	}
}