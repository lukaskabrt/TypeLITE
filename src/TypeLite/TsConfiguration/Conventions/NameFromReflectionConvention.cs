using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLite.TsConfiguration.Conventions {
    public class NameFromReflectionConvention : IModuleMemberConvention {
        public TsModuleMemberConfiguration Apply(Type t) {
            var typeName = t.Name;
            if(typeName.Contains("`")) {
                typeName = typeName.Remove(typeName.IndexOf('`'));
            }

            return new TsModuleMemberConfiguration() { Name = typeName };
        }
    }
}
