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

        protected TypeResolver() { }

        public TypeResolver(ITsConfigurationProvider configurationPorvider) {
            _configurationProvider = configurationPorvider;
            _knownTypes = new Dictionary<Type, TsType>();
            foreach (var systemType in TsBasicType.SystemTypes) {
                _knownTypes.Add(systemType.Context, systemType);
            }
        }

        public virtual TsType ResolveType(Type t) {
            if (_knownTypes.ContainsKey(t)) {
                return _knownTypes[t];
            }

            var typeInfo = t.GetTypeInfo();
            var typeConfiguration = _configurationProvider.GetConfiguration(t) as TsModuleMemberConfiguration;

            if (t.IsNullable()) {
                return this.ResolveType(t.GetNullableValueType());
            }

            if (t.IsCollection()) {
                var collectionItemType = t.GetCollectionItemType();
                if (collectionItemType == null) {
                    return this.CacheAndReturn(t, new TsCollectionType() { ItemType = TsBasicType.Any, Context = t });
                } else {
                    return this.CacheAndReturn(t, new TsCollectionType() { ItemType = this.ResolveType(collectionItemType), Context = t });
                }
            }

            if ((typeInfo.IsClass || typeInfo.IsGenericType || typeInfo.IsInterface || typeInfo.IsValueType ) && typeConfiguration != null) {
                var resolvedType = new TsBasicType() { Context = t };
                resolvedType.TypeName = typeConfiguration.Name;
                resolvedType.Module = typeConfiguration.Module;

                if (typeInfo.IsGenericType) {
                    foreach (var arg in typeInfo.GenericTypeArguments) {
                        resolvedType.GenericArguments.Add(this.ResolveType(arg));
                    }
                }

                if (typeInfo.IsGenericTypeDefinition) {
                    foreach (var arg in typeInfo.GenericTypeParameters) {
                        resolvedType.GenericArguments.Add(this.ResolveType(arg));
                    }
                }

                return this.CacheAndReturn(t, resolvedType);
            }

            return TsBasicType.Any;
        }

        private TsType CacheAndReturn(Type t, TsType resolved) {
            _knownTypes[t] = resolved;
            return resolved;
        }
    }
}
