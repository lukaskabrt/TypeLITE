using System;
using System.Collections.Generic;
using TypeLite.Ts;

namespace TypeLite {
    public class TsModel {
        public ISet<TsEnum> Enums { get; set; }

        public TsModel() {
            this.Enums = new HashSet<TsEnum>();
        }
    }
}
