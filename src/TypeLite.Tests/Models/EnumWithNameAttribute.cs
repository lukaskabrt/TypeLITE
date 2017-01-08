using System;
using System.Collections.Generic;
using System.Text;
using TypeLite.TsConfiguration.Attributes;

namespace TypeLite.Tests.Models {
    [TsEnum(Name = "EnumWithCustomName")]
    enum EnumWithNameAttribute {
        EnumValue1 = 1
    }
}
