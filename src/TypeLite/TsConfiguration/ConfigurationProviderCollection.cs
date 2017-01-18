using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TypeLite.TsConfiguration {
    /// <summary>
    /// Combines several configuration providers
    /// </summary>
    public class ConfigurationProviderCollection : ITsConfigurationProvider {
        /// <summary>
        /// Gets providers in the collection
        /// </summary>
        public IList<ITsConfigurationProvider> Providers { get; private set; }

        /// <summary>
        /// Initializes a new instance of the ConfigurationProviderCollection class
        /// </summary>
        public ConfigurationProviderCollection() {
            this.Providers = new List<ITsConfigurationProvider>();
        }

        /// <summary>
        /// Initializes a new instance of the ConfigurationProviderCollection class
        /// </summary>
        public ConfigurationProviderCollection(IEnumerable<ITsConfigurationProvider> providers) {
            this.Providers = new List<ITsConfigurationProvider>(providers);
        }

        public TsNodeConfiguration GetConfiguration(Type t) {
            var configurations = this.Providers.Select(o => o.GetConfiguration(t)).OfType<TsModuleMemberConfiguration>().ToList();
            return TsNodeConfiguration.Merge(configurations);
        }

        public TsEnumValueConfiguration GetEnumValueConfiguration(FieldInfo enumValue) {
            var configurations = this.Providers.Select(o => o.GetEnumValueConfiguration(enumValue)).OfType<TsEnumValueConfiguration>().ToList();
            return TsNodeConfiguration.Merge(configurations);
        }

        public TsMemberConfiguration GetMemberConfiguration(MemberInfo member) {
            var configurations = this.Providers.Select(o => o.GetMemberConfiguration(member)).OfType<TsMemberConfiguration>().ToList();
            return TsNodeConfiguration.Merge(configurations);
        }
    }
}
