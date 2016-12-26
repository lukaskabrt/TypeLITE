using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLite.TsConfiguration.Conventions {
    public class NameFromReflectionConvention : IEnumConvention {
        public TsEnumConfiguration Apply(Type t) {
            return new TsEnumConfiguration() { Name = t.Name };
        }
    }
}
