using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TypeLite.TsConfiguration;

namespace TypeLite.Ts {
    /// <summary>
    /// Represents a class in the code model.
    /// </summary>
    public class TsClass : TsModuleMember {
        public TsType BaseType { get; set; }

        public IList<TsType> Interfaces { get; private set; }

        public TsClass(TsBasicType typeName) {
            this.Name = typeName;
            this.Interfaces = new List<TsType>();
        }

        public static TsClass CreateFrom<T>(TypeResolver typeResolver, ITsConfigurationProvider configurationProvider) {
            var @class = new TsClass((TsBasicType)typeResolver.ResolveType(typeof(T)));

            var classType = typeof(T);
            var classTypeInfo = classType.GetTypeInfo();

            @class.Interfaces = classTypeInfo.ImplementedInterfaces
                .Except(classTypeInfo.ImplementedInterfaces.SelectMany(@interface => @interface.GetTypeInfo().ImplementedInterfaces))
                .Select(@interface => typeResolver.ResolveType(@interface))
                .Where(interfaceType => interfaceType != null)
                .ToList();

            //TODO: why we filter ValueType in V1?
            if(classTypeInfo.BaseType != null && classTypeInfo.BaseType != typeof(object) && classTypeInfo.BaseType != typeof(ValueType)) {
                @class.BaseType = typeResolver.ResolveType(classTypeInfo.BaseType);
            }

            return @class;
        }
    }
}
