using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TypeLite.TsConfiguration {
    public interface IConvention {

    }

    public interface IModuleMemberConvention : IConvention {
        TsModuleMemberConfiguration Apply(Type t);
    }

    public interface IEnumValueConvention : IConvention {
        TsEnumValueConfiguration Apply(FieldInfo enumValue);
    }
}
