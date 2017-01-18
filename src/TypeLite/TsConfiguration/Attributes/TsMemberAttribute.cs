using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLite.TsConfiguration.Attributes {
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class TsMemberAttribute : Attribute {
        /// <summary>
        /// Gets or sets the name of the member in the script model
        /// </summary>
        public string Name { get; set; }
    }
}
