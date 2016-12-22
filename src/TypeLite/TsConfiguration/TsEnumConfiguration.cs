using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLite.TsConfiguration {
    /// <summary>
    /// Configuration of TypeResolver for enum
    /// </summary>
    public class TsEnumConfiguration : TsNodeConfiguration {
        /// <summary>
        /// Gets or sets the name of the enum in the script model.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
		/// Gets or sets the namespace of the enum in the script model.
		/// </summary>
        public string Module { get; set; }
    }
}
