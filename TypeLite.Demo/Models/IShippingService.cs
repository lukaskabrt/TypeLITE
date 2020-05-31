using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TypeLite.Demo.Models {
	/// <summary>
	/// Shipping Service
	/// </summary>
    [TsInterface]
    public interface IShippingService {
		/// <summary>
		/// price
		/// </summary>
        double Price { get; set; }
    }
}