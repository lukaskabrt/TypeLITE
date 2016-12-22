using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLite.TsConfiguration.Attributes {
    /// <summary>
    /// Configures an enum value
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class TsEnumValueAttribute : Attribute {
        /// <summary>
        /// Gets or sets the name of the enum value in the script model
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets value of the enum value.
        /// </summary>
        public string Value { get; set; }
    }
}
