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
            var conventions = this.Conventions.Where(o => o as IEnumConvention != null);
            var configurations = conventions.OfType<IEnumConvention>().Select(o => o.Apply(t)).ToList();
            return TsNodeConfiguration.Merge<TsEnumConfiguration>(configurations);
        }

        public TsEnumValueConfiguration GetEnumValueConfiguration(FieldInfo enumValue) {
            throw new NotImplementedException();
        }
    }
}
