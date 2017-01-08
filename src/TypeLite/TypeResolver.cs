using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TypeLite.Ts;
using TypeLite.TsConfiguration;

namespace TypeLite {
    public class TypeResolver {
        private IDictionary<Type, TsType> _knownTypes;
        private ITsConfigurationProvider _configurationProvider;

        public TypeResolver(ITsConfigurationProvider configurationPorvider) {
            _configurationProvider = configurationPorvider;
            _knownTypes = new Dictionary<Type, TsType>();
            foreach (var systemType in TsBasicType.SystemTypes) {
                _knownTypes.Add(systemType.Context, systemType);
            }
        }

        public TsType ResolveType(Type t) {
            if(_knownTypes.ContainsKey(t)) {
                return _knownTypes[t];
            }

            var typeInfo = t.GetTypeInfo();
            var typeConfiguration = _configurationProvider.GetConfiguration(t) as TsModuleMemberConfiguration;

            if(t.IsNullable()) {
                return this.ResolveType(t.GetNullableValueType());
            }

            if(t.IsCollection()) {
                var collectionItemType = t.GetCollectionItemType();
                if(collectionItemType == null) {
                    return new TsCollectionType() { ItemType = TsBasicType.Any, Context = t };
                } else {
                    return new TsCollectionType() { ItemType = this.ResolveType(collectionItemType), Context = t };
                }
            }

            if(typeInfo.IsGenericType) {
                var genericType = new TsBasicType() { Context = t };
                genericType.TypeName = typeConfiguration.Name;
                genericType.Module = typeConfiguration.Module;
                foreach (var arg in typeInfo.GenericTypeArguments) {
                    genericType.GenericArguments.Add(this.ResolveType(arg));
                }

                return genericType;
            }

            return TsBasicType.Any;
        }
    }
}
