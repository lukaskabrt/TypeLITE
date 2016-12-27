using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeLite.TsConfiguration.Conventions {
    public class ModuleFromReflectionConvention : IModuleMemberConvention {
        public TsModuleMemberConfiguration Apply(Type t) {
            var module = t.Namespace;

            var declaringTypes = this.UnwindDeclaringTypes(t);
            if (declaringTypes.Any()) {
                module = $"{module}.{string.Join(".", declaringTypes)}";
            }

            return new TsModuleMemberConfiguration() { Module = module };
        }

        private IEnumerable<string> UnwindDeclaringTypes(Type t) {
            var declaringTypes = new List<string>();

            var declaringType = t.DeclaringType;
            while(declaringType != null) {
                declaringTypes.Insert(0, declaringType.Name);
                declaringType = declaringType.DeclaringType;
            }

            return declaringTypes;
        }
    }
}
