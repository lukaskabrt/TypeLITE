using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Reflection;
using TypeLite.Extensions;

namespace TypeLite.TsModels {
    /// <summary>
    /// Represents a class in the code model.
    /// </summary>
    [DebuggerDisplay("TsClass - Name: {Name}")]
    public class TsClass : TsModuleMember {
        /// <summary>
        /// Gets collection of properties of the class.
        /// </summary>
        public ICollection<TsProperty> Properties { get; private set; }

        /// <summary>
        /// Gets collection of fields of the class.
        /// </summary>
        public ICollection<TsProperty> Fields { get; private set; }

        /// <summary>
        /// Gets collection of GenericArguments for this class
        /// </summary>
        public IList<TsType> GenericArguments { get; private set; }

        /// <summary>
        /// Gets collection of constants of the class.
        /// </summary>
        public ICollection<TsProperty> Constants { get; private set; }

        /// <summary>
        /// Gets base type of the class
        /// </summary>
        /// <remarks>
        /// If the class derives from the object, the BaseType property is null.
        /// </remarks>
        public TsType BaseType { get; internal set; }

        // TODO document
        public IList<TsType> Interfaces { get; internal set; }

        /// <summary>
        /// Gets or sets bool value indicating whether this class will be ignored by TsGenerator.
        /// </summary>
        public bool IsIgnored { get; set; }

        /// <summary>
        /// Initializes a new instance of the TsClass class with the specific CLR type.
        /// </summary>
        /// <param name="type">The CLR type represented by this instance of the TsClass</param>
        public TsClass(Type type)
            : base(type) {

            this.Properties = this.Type
                .GetProperties()
                .Where(pi => pi.DeclaringType == this.Type)
                .Select(pi => new TsProperty(pi))
                .ToList();

            this.Fields = this.Type
                .GetFields()
                .Where(fi => fi.DeclaringType == this.Type
                    && !(fi.IsLiteral && !fi.IsInitOnly)) // skip constants
                .Select(fi => new TsProperty(fi))
                .ToList();

            this.Constants = this.Type
                .GetFields()
                .Where(fi => fi.DeclaringType == this.Type
                    && fi.IsLiteral && !fi.IsInitOnly) // constants only
                .Select(fi => new TsProperty(fi))
                .ToList();

            if (type.IsGenericType) {
                this.Name = type.Name.Remove(type.Name.IndexOf('`'));
                this.GenericArguments = type
                    .GetGenericArguments()
                    .Select(TsType.Create)
                    .ToList();
            } else {
                this.Name = type.Name;
                this.GenericArguments = new TsType[0];
            }

            if (this.Type.BaseType != null && this.Type.BaseType != typeof(object) && this.Type.BaseType != typeof(ValueType)) {
                this.BaseType = new TsType(this.Type.BaseType);
            }

            var interfaces = this.Type.GetInterfaces();
            this.Interfaces = interfaces
                .Where(@interface => @interface.GetCustomAttribute<TsInterfaceAttribute>(false) != null)
                .Except(interfaces.SelectMany(@interface => @interface.GetInterfaces()))
                .Select(TsType.Create).ToList();

            var attribute = this.Type.GetCustomAttribute<TsClassAttribute>(false);
            if (attribute != null) {
                if (!string.IsNullOrEmpty(attribute.Name)) {
                    this.Name = attribute.Name;
                }

                if (attribute.Module != null) {
                    this.Module.Name = attribute.Module;
                }
            }

            var ignoreAttribute = this.Type.GetCustomAttribute<TsIgnoreAttribute>(false);
            if (ignoreAttribute != null) {
                this.IsIgnored = true;
            }
        }
    }
}
