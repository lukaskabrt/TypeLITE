using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLite.Tests.TestModels.Namespace1;
using TypeLite.Tests.TestModels.Namespace2;
using TypeLite.TsModels;
using Xunit;

namespace TypeLite.Tests.RegressionTests
{
    public class Issue82_OrderingTests
    {
        [Fact]
        public void OrdersOutputByNamespace()
        {
            var builder = new TsModelBuilder();
            builder.Add<DifferentNamespaces_Class1>();
            builder.Add<DifferentNamespaces_Class2>();
            builder.Add<DifferentNamespaces_Class3>();

            var generator = new TsGenerator();
            var model = builder.Build();

            var result = generator.Generate(model);

            Assert.True(result.IndexOf("TypeLite.Tests.TestModels.Namespace1") < result.IndexOf("TypeLite.Tests.TestModels.Namespace2"), "Didn't order namespaces");
        }

        [Fact]
        public void RespectsModuleNameFormatterOverrides()
        {
            var builder = new TsModelBuilder();
            builder.Add<DifferentNamespaces_Class2>();
            builder.Add<DifferentNamespaces_Class3>();

            var generator = new TsGenerator();
            // Reverse the order of the modules
            generator.SetModuleNameFormatter(m => m.Name == "TypeLite.Tests.TestModels.Namespace1" ? "modz" : "moda");
            var model = builder.Build();

            var result = generator.Generate(model);

            Assert.True(result.IndexOf("moda") < result.IndexOf("modz"), "Didn't order namespaces when formatters involved");
        }

        [Fact]
        public void OrdersClassesByName()
        {
            var builder = new TsModelBuilder();
            builder.Add<DifferentNamespaces_Class1>();
            builder.Add<DifferentNamespaces_Class2>();

            var generator = new TsGenerator();
            var model = builder.Build();

            var result = generator.Generate(model);

            Assert.True(result.IndexOf("DifferentNamespaces_Class1") < result.IndexOf("DifferentNamespaces_Class2"), "Didn't order classes");
        }

        [Fact]
        public void RespectsTypeNameFormatterOverrides()
        {
            var builder = new TsModelBuilder();
            builder.Add<DifferentNamespaces_Class1>();
            builder.Add<DifferentNamespaces_Class2>();

            var generator = new TsGenerator();
            // Reverse the order of the classes
            generator.RegisterTypeFormatter((t, f) => ((TsClass) t).Name == "DifferentNamespaces_Class1" ? "classz" : "classa");
            var model = builder.Build();

            var result = generator.Generate(model);

            Assert.True(result.IndexOf("classa") < result.IndexOf("classz"), "Didn't order classes when formatters involved");
        }

        [Fact]
        public void OrdersPropertiesAndFieldsByName()
        {
            var builder = new TsModelBuilder();
            builder.Add<DifferentNamespaces_Class2>();

            var generator = new TsGenerator();
            var model = builder.Build();

            var result = generator.Generate(model);

            Assert.True(result.IndexOf("Property1") < result.IndexOf("Property2"), "Didn't order properties");
        }

        [Fact]
        public void RespectsFieldNameFormatterOverrides()
        {
            var builder = new TsModelBuilder();
            builder.Add<DifferentNamespaces_Class2>();

            var generator = new TsGenerator();
            // Reverse the order of the properties
            generator.SetIdentifierFormatter(x => x.Name == "Property1" ? "propz" : "propa");
            var model = builder.Build();

            var result = generator.Generate(model);

            Assert.True(result.IndexOf("propa") < result.IndexOf("propz"), "Didn't order properties when formatters involved");
        }
    }
}
