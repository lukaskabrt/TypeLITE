using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLite.Ts {
    /// <summary>
    /// Represents a type in TypeLite AST
    /// </summary>
    public class TsTypeName : TsNode {
        /// <summary>
        /// Gets name of the type
        /// </summary>
        public string TypeName { get; set; }
    }
}
