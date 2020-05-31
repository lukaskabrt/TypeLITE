using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLite.Tests.TestModels {
	public class CustomTypeCollectionReference {
		public Product[] Products { get; set; }
		public IEnumerable<Person> People { get; set; }
	}
}
