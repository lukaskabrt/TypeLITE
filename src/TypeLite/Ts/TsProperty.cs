using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TypeLite.TsConfiguration;

namespace TypeLite.Ts {
    public class TsProperty : TsMember {
        public PropertyInfo Context { get; set; }

        public static TsProperty CreateFrom(PropertyInfo propertyInfo, TypeResolver resolver, ITsConfigurationProvider configurationProvider) {
            var property = new TsProperty();

            var propertyConfiguration = configurationProvider.GetMemberConfiguration(propertyInfo);
            property.Name = propertyConfiguration.Name;

            property.Type = resolver.ResolveType(propertyInfo.PropertyType);

            return property;
        }
    }
}
