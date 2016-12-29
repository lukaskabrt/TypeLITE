using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLite.Ts {
    /// <summary>
    /// Represents a type in TypeLite AST
    /// </summary>
    public class TsType : TsNode {
        public Type Context { get; set; }
    }
}
