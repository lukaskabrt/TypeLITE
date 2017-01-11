using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TypeLite.TsConfiguration;

namespace TypeLite.Ts {
    /// <summary>
    /// Represents an enum in TypeLite AST
    /// </summary>
    public class TsEnum : TsModuleMember {
        /// <summary>
        /// Gets collection values of the enum
        /// </summary>
        public List<TsEnumValue> Values { get; private set; }

        /// <summary>
        /// Initializes a new instance of the TsEnum class
        /// </summary>
        public TsEnum(TsBasicType typeName) {
            this.Values = new List<TsEnumValue>();
            this.Name = typeName;
        }

        public static TsEnum CreateFrom<T>(TypeResolver typeResolver, ITsConfigurationProvider configurationProvider) {
            var @enum = new TsEnum((TsBasicType)typeResolver.ResolveType(typeof(T)));

            var enumType = typeof(T);
            var enumTypeInfo = enumType.GetTypeInfo();

            @enum.Values = enumTypeInfo.DeclaredFields
                .Where(fieldInfo => fieldInfo.IsLiteral)
                .Select(fieldInfo => TsEnumValue.CreateFrom(fieldInfo, configurationProvider))
                .Where(enumValue => enumValue != null)
                .ToList();

            return @enum;
        }
    }
}
