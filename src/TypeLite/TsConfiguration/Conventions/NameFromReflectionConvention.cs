using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLite.TsConfiguration.Conventions {
    public class NameFromReflectionConvention : IModuleMemberConvention {
        public TsModuleMemberConfiguration Apply(Type t) {
            return new TsModuleMemberConfiguration() { Name = t.Name };
        }
    }
}
