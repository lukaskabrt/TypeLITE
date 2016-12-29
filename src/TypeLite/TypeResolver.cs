using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TypeLite.Ts;

namespace TypeLite {
    public class TypeResolver {
        private IDictionary<Type, TsType> _knownTypes;

        public TypeResolver() {
            _knownTypes = new Dictionary<Type, TsType>();
            foreach (var systemType in TsBasicType.SystemTypes) {
                _knownTypes.Add(systemType.Context, systemType);
            }
        }

        public TsType ResolveType(Type t) {
            if(_knownTypes.ContainsKey(t)) {
                return _knownTypes[t];
            }

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

            return TsBasicType.Any;
        }
    }
}
