using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLite.TsConfiguration {
    public interface IConvention {

    }

    public interface IEnumConvention : IConvention {
        TsEnumConfiguration Apply(Type t);
    }
}
