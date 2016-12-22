using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLite.Ts {
    /// <summary>
    /// Represents an enum in TypeLite AST
    /// </summary>
    public class TsEnum : TsNode {
        /// <summary>
        /// Gets or sets name of the enum
        /// </summary>
        public  TsTypeName Name { get; set; }

        /// <summary>
        /// Gets collection values of the enum
        /// </summary>
        public List<TsEnumValue> Values { get; private set; }

        /// <summary>
        /// Initializes a new instance of the TsEnum class
        /// </summary>
        public TsEnum() {
            this.Values = new List<TsEnumValue>();
        }
    }
}
