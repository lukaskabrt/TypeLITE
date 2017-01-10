using System;
using System.Collections.Generic;
using System.Text;
using TypeLite.TsConfiguration.Attributes;

namespace TypeLite.Tests.Models {
    [TsClass(Name = "GenericClassWithAttributeAndCustomName")]
    public class GenericClassWithAttribute<T> {
        public T Member { get; set; }
    }

    [TsClass(Name = "ClosedGenericClassWithAttributeAndCustomName")]
    public class ClosedGenericClassWithAttribute : GenericClassWithAttribute<ClassWithoutAttribute> { }
}
