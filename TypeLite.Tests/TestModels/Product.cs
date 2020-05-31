using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLite.Tests.TestModels {
	[TsClass]
	public class Product {
		public string Name { get; set; }
		public double Price { get; set; }

		[TsIgnore]
		public double Ignored { get; set; }
	}
}
