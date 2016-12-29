using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TypeLite {
    public static class TypeExtensions {
        /// <summary>
		/// Determined whether the specific type is nullable value type.
		/// </summary>
		/// <param name="type">The type to inspect.</param>
		/// <returns>true if the type is nullable value type otherwise false</returns>
		public static bool IsNullable(this Type type) {
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// Retrieves underlaying value type of the nullable value type.
        /// </summary>
        /// <param name="type">The type to inspect.</param>
        /// <returns>The underlaying value type.</returns>
        public static Type GetNullableValueType(this Type type) {
            var typeInfo = type.GetTypeInfo();
            return typeInfo.GenericTypeArguments.Single();
        }

        public static bool IsCollection(this Type type) {
            var typeInfo = type.GetTypeInfo();
            var enumerableTypeInfo = typeof(System.Collections.IEnumerable).GetTypeInfo();

            return enumerableTypeInfo.IsAssignableFrom(typeInfo);
        }

        /// <summary>
        /// Gets type of items in generic version of IEnumerable.
        /// </summary>
        /// <param name="type">The IEnumerable type to get items type from</param>
        /// <returns>The type of items in the generic IEnumerable or null if the type doesn't implement the generic version of IEnumerable.</returns>
        public static Type GetCollectionItemType(this Type type) {
            var typeInfo = type.GetTypeInfo();

            if (typeInfo.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>)) {
                return typeInfo.GenericTypeArguments[0];
            }

            foreach (Type interfaceType in typeInfo.ImplementedInterfaces) {
                var interfaceTypeInfo = interfaceType.GetTypeInfo();

                if (interfaceTypeInfo.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IEnumerable<>)) {
                    return interfaceTypeInfo.GenericTypeArguments[0];
                }
            }

            return null;
        }
    }
}
