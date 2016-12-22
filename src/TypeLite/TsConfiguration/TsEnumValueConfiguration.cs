using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLite.TsConfiguration {
    /// <summary>
    /// Configuration of TypeResolver for enum value
    /// </summary>
    public class TsEnumValueConfiguration : TsNodeConfiguration {
        /// <summary>
        /// Gets or sets the name of the enum value in the script model.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the enum value.
        /// </summary>
        public string Value { get; set; }
    }
}
