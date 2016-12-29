using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLite.Ts {
    public class TsCollectionType : TsType {
        public TsType ItemType { get; set; }

        public int Dimension { get; set; }
    }
}
