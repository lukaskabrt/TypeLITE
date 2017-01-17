using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLite.Tests.Models {
    class ClassWithIntefaces : IInterfaceForClassWithInterfaces, IInterfaceForClassWithInterfaces2 {
    }

    public interface IInterfaceForClassWithInterfaces { }
    public interface IInterfaceForClassWithInterfaces2 { }
}
