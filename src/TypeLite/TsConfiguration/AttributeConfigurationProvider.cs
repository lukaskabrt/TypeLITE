using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using TypeLite.TsConfiguration.Attributes;

namespace TypeLite.TsConfiguration {
    /// <summary>
    /// Reads configuration for a type from attributes
    /// </summary>
    public class AttributeConfigurationProvider : ITsConfigurationProvider {
        /// <summary>
        /// Reads configuration for the specific type
        /// </summary>
        /// <param name="t">the type to read configuration for</param>
        /// <returns>the configuration read from attributes or null if there aren't any configuration attributes for the type</returns>
        public virtual TsNodeConfiguration GetConfiguration(Type t) {
            var typeInfo = t.GetTypeInfo();

            if(typeInfo.IsEnum) {
                return this.ReadEnumConfiguration(t, typeInfo);
            }
            throw new NotImplementedException();
        }

        public TsEnumValueConfiguration GetEnumValueConfiguration(FieldInfo enumValue) {
            var attribute = enumValue.GetCustomAttribute<TsEnumValueAttribute>();

            if (attribute == null) {
                return null;
            }

            return new TsEnumValueConfiguration() {
                Name = attribute.Name,
                Value = attribute.Value
            };
        }

        public TsMemberConfiguration GetMemberConfiguration(MemberInfo member) {
            var attribute = member.GetCustomAttribute<TsMemberAttribute>();

            if (attribute == null) {
                return null;
            }

            return new TsMemberConfiguration() {
                Name = attribute.Name
            };
        }

        protected virtual TsModuleMemberConfiguration ReadEnumConfiguration(Type t, TypeInfo typeInfo) {
            var attribute = typeInfo.GetCustomAttribute<TsEnumAttribute>(false);

            if(attribute == null) {
                return null;
            }

            return new TsModuleMemberConfiguration() {
                Name = attribute.Name,
                Module = attribute.Module
            };
        }
    }
}
