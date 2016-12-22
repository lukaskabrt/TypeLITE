using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
