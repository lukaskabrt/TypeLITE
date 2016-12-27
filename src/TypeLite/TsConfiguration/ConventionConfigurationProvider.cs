using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TypeLite.TsConfiguration {
    public class ConventionConfigurationProvider : ITsConfigurationProvider {
        public IList<IConvention> Conventions { get; private set; }

        public ConventionConfigurationProvider() {
            this.Conventions = new List<IConvention>();
        }

        public TsNodeConfiguration GetConfiguration(Type t) {
            var configurations = this.Conventions.OfType<IModuleMemberConvention>().Select(o => o.Apply(t)).ToList();
            return TsNodeConfiguration.Merge(configurations);
        }

        public TsEnumValueConfiguration GetEnumValueConfiguration(FieldInfo enumValue) {
            var configurations = this.Conventions.OfType<IEnumValueConvention>().Select(o => o.Apply(enumValue)).ToList();
            return TsNodeConfiguration.Merge(configurations);
        }
    }
}
