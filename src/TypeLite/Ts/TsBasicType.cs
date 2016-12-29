using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLite.Ts {
    public class TsBasicType : TsType {
        public static readonly TsBasicType Any = new TsBasicType() { TypeName = "any", Context = typeof(object) };

        public static readonly TsBasicType[] SystemTypes = new TsBasicType[] {
            new TsBasicType() { TypeName = "number", Context = typeof(byte) },
            new TsBasicType() { TypeName = "number", Context = typeof(sbyte) },
            new TsBasicType() { TypeName = "number", Context = typeof(short) },
            new TsBasicType() { TypeName = "number", Context = typeof(ushort) },
            new TsBasicType() { TypeName = "number", Context = typeof(int) },
            new TsBasicType() { TypeName = "number", Context = typeof(uint) },
            new TsBasicType() { TypeName = "number", Context = typeof(long) },
            new TsBasicType() { TypeName = "number", Context = typeof(ulong) },
            new TsBasicType() { TypeName = "number", Context = typeof(float) },
            new TsBasicType() { TypeName = "number", Context = typeof(double) },
            new TsBasicType() { TypeName = "number", Context = typeof(decimal) },
            new TsBasicType() { TypeName = "string", Context = typeof(string) },
            new TsBasicType() { TypeName = "string", Context = typeof(char) },
            new TsBasicType() { TypeName = "boolean", Context = typeof(bool) },
        };

        public string TypeName { get; set; }

        public string Module { get; set; }

        public IList<TsType> GenericArguments { get; private set; }

        public TsBasicType() {
            this.GenericArguments = new List<TsType>();
        }
    }
}
