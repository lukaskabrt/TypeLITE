using System;
using System.Collections.Generic;
using System.Text;
using TypeLite.TsConfiguration.Attributes;

namespace TypeLite.Tests.Models {
    [TsEnum(Name = "EnumWithAttributeName", Module = "EnumWithAttributeModule")]
    enum EnumWithAttribute {
        EnumValue1 = 1,

        [TsEnumValue(Name = "EnumValueWithAttributeName", Value = "100")]
        EnumValueWithAttribute = 1
    }
}
