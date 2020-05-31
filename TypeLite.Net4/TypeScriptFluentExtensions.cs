using System;
using System.Linq;
using System.Reflection;

namespace TypeLite.Net4 {
    /// <summary>
    /// Contains extensions methods specific for full .NET framework
    /// </summary>
    public static class TypeScriptFluentExtensions {
        /// <summary>
        /// Adds all classes annotated with the TsClassAttribute from all curently loaded assemblies.
        /// </summary>
        /// <returns>Instance of the TypeScriptFluent that enables fluent configuration.</returns>
        public static TypeScriptFluent ForLoadedAssemblies(this TypeScriptFluent ts) {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                ts.ModelBuilder.Add(assembly);
            }

            return ts;
        }

        /// <summary>
        /// Adds all Types derived from T
        /// </summary>
        /// <returns>Instance of the TypeScriptFluent that enables fluent configuration.</returns>
        public static TypeScriptFluent TypesDervivedFrom<T>(this TypeScriptFluent ts, bool includeBaseType = true) {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                foreach (var type in assembly.GetTypes().Where(x => typeof(T).IsAssignableFrom(x))) {
                    if (includeBaseType || type != typeof(T)) {
                        ts.ModelBuilder.Add(type);
                    }
                }
            }

            return ts;
        }

        /// <summary>
        /// Adds all classes annotated with the TsClassAttribute from the referenced assembly identified by the name parameter.
        /// </summary>
        /// <param name="name">
        /// The name of the assembly to scan
        /// </param>
        /// <returns>Instance of the TypeScriptFluent that enables fluent configuration.</returns>
        public static TypeScriptFluent ForReferencedAssembly(this TypeScriptFluent ts, string name) {

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                foreach (var obj in assembly.GetReferencedAssemblies()) {
                    if (obj.Name == name) {
                        var assembly2 = Assembly.Load(obj);
                        if (assembly2 != null) {
                            ts.ModelBuilder.Add(assembly2);
                        }
                    }

                }
            }

            return ts;
        }

        /// <summary>
        /// Register a document appender.
        /// </summary>
        /// <returns>Instance of the TypeScriptFluent that enables fluent documentation.</returns>
        public static TypeScriptFluent WithJSDoc(this TypeScriptFluent ts) {
            ts.ScriptGenerator.SetDocAppender(new DocAppender());
            return ts;
        }
    }
}
