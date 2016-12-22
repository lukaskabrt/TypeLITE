using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TypeLite.TsConfiguration {
    /// <summary>
    /// Reads configuration for a type from a source
    /// </summary>
    public interface ITsConfigurationReader {
        /// <summary>
        /// Reads configuration for the specific type
        /// </summary>
        /// <param name="t">the type to read configuration for</param>
        /// <returns>the configuration read from a source or null if the source doesn't contain any configuration for the type</returns>
        TsNodeConfiguration Read(Type t);

        TsEnumValueConfiguration ReadEnumValueConfig(FieldInfo enumValue);
    }
}
