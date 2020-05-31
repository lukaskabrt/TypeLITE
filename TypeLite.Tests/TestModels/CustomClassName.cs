using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLite.Tests.TestModels {
	[TsClass(Name = "MyClass", Module = "MyModule")]
	public class CustomClassName {
		[TsProperty(Name = "MyProperty")]
		public int CustomPorperty { get; set; }
	}
}
