using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TypeLite.TsConfiguration {
    /// <summary>
    /// Provides configuration for a type from a source
    /// </summary>
    public interface ITsConfigurationProvider {
        /// <summary>
        /// Provides configuration for the specific type
        /// </summary>
        /// <param name="t">the type to read configuration for</param>
        /// <returns>the configuration read from a source or null if the source doesn't contain any configuration for the type</returns>
        TsNodeConfiguration GetConfiguration(Type t);

        /// <summary>
        /// Provides configuration for the specific enum value
        /// </summary>
        /// <param name="enumValue">FieldInfo representing enum value</param>
        /// <returns>the configuration read from a source or null if the source doesn't contain any configuration for the type</returns>
        TsEnumValueConfiguration GetEnumValueConfiguration(FieldInfo enumValue);

        /// <summary>
        /// Provides configuration for the specific member of a type
        /// </summary>
        /// <param name="member">MemberInfo representing a memebr</param>
        /// <returns>the configuration read from a source or null if the source doesn't contain any configuration for the type</returns>
        TsMemberConfiguration GetMemberConfiguration(MemberInfo member);
    }
}
