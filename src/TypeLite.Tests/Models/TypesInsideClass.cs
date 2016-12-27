using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLite.Tests.Models {
    public class EnclosingClass {
        public enum EnumInsideClass {
            EnumValue1 = 1
        }

        public class EnclosingClassInEnclosingClass {
            public enum EnumInsideClassInsideClass {
                EnumValue1 = 1
            }
        }
    }
}
