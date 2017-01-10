using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLite.Tests.Models {
    public class GenericClass<T> {
        public T Member { get; set; }
    }

    public class ClosedGenericClass : GenericClass<ClassWithoutAttribute> { }
}
