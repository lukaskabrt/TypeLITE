using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TypeLite.TsConfiguration.Conventions
{
    public class MemberNameFromReflectionConvention : IMemberConvention {
        public TsMemberConfiguration Apply(MemberInfo member) {
            return new TsMemberConfiguration() { Name = member.Name };
        }
    }
}
