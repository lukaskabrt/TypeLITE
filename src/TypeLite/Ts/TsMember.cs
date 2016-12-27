using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLite.Ts {
    public abstract class TsMember : TsNode {
        public string Name { get; set; }
        public TsAccessModifier? AccessModifier { get; set; }
    }
}
