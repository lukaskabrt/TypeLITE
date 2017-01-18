using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLite.Tests.Models {
    class ClassWithProperty : BaseClassWithProperty {
        public int Field;
        public const int Constant = 5;

        public int Property { get; set; }
    }

    class BaseClassWithProperty {
        public int BaseProperty { get; set; }
    }
}
