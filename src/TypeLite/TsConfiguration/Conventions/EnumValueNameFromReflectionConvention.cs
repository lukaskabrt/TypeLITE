using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TypeLite.TsConfiguration.Conventions
{
    public class EnumValueNameFromReflectionConvention : IEnumValueConvention {
        public TsEnumValueConfiguration Apply(FieldInfo enumValue) {
            return new TsEnumValueConfiguration() { Name = enumValue.Name };
        }
    }
}
