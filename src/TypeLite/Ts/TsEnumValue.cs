using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TypeLite.TsConfiguration;

namespace TypeLite.Ts {
    /// <summary>
    /// Represents a value of enums in TypeLite AST
    /// </summary>
    public class TsEnumValue : TsNode {
        /// <summary>
        /// Gets name of the enum value
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets value of the enum value
        /// </summary>
        public string Value { get; private set; }

        public static TsEnumValue CreateFrom(FieldInfo enumValueField, ITsConfigurationProvider configurationProvider) {
            var enumValue = new TsEnumValue();

            var enumValueConfiguration = configurationProvider.GetEnumValueConfiguration(enumValueField);
            enumValue.Name = enumValueConfiguration.Name;
            enumValue.Value = enumValueConfiguration.Value;

            return enumValue;
        }
    }
}
