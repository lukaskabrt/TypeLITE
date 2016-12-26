using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TypeLite.TsConfiguration {
    /// <summary>
    /// Base class for configuration of TypeResolver
    /// </summary>
    public abstract class TsNodeConfiguration {
        /// <summary>
        /// Merges a collection of configuration obejcts from different providers into single configuration. 
        /// </summary>
        /// <typeparam name="T">thr type of configuration objects to merge</typeparam>
        /// <param name="source">a collection of configuration objects to merge</param>
        /// <returns>the configuration object with values merged from the source configuration objects. If the source collection is empty, null is returned</returns>
        /// <remarks>
        /// Merge is additive operation. It takes values from the first configuration object, and overwrites them with non-null values
        /// from the next configuration objects.
        /// </remarks>
        public static T Merge<T>(IEnumerable<T> source) where T : new() {
            if(!source.Any()) {
                return default(T);
            }

            var typeInfo = typeof(T).GetTypeInfo();
            var result = new T();

            foreach (var item in source) {
                foreach (var propertyInfo in typeInfo.DeclaredProperties) {
                    var propertyValue = propertyInfo.GetValue(item);
                    if(propertyValue != null) {
                        propertyInfo.SetValue(result, propertyValue);
                    }
                }
            }
            
            return result;
        }
    }
}
