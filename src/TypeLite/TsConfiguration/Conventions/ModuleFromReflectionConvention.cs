using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLite.TsConfiguration.Conventions {
    public class ModuleFromReflectionConvention : IEnumConvention {
        public TsEnumConfiguration Apply(Type t) {
            return new TsEnumConfiguration() { Module = t.Namespace };
        }
    }
}
