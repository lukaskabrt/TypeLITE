using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLite.Tests.TestModels;
using TypeLite.TsModels;
using Xunit;

namespace TypeLite.Tests.TsModels {
	public class TsClassTests {

		[Fact]
		public void WhenInitialized_NameIsSet() {
			var target = new TsClass(typeof(Person));

			Assert.Equal("Person", target.Name);
		}

		[Fact]
		public void WhenInitialized_IsIgnoredIsFalse() {
			var target = new TsClass(typeof(Person));

			Assert.False(target.IsIgnored);
		}

		[Fact]
		public void WhenInitialized_PropertiesAreCreated() {
			var target = new TsClass(typeof(Address));

			Assert.Single(target.Properties.Where(o => o.MemberInfo == typeof(Address).GetProperty("Street")));
			Assert.Single(target.Properties.Where(o => o.MemberInfo == typeof(Address).GetProperty("Town")));
		}

		[Fact]
		public void WhenInitialized_FieldsAreCreated() {
			var target = new TsClass(typeof(Address));

			Assert.Single(target.Fields.Where(o => o.MemberInfo == typeof(Address).GetField("PostalCode")));
		}

        [Fact]
        public void WhenInitialized_ConstantsAreCreated()
        {
            var target = new TsClass(typeof(Person));

            Assert.Single(target.Constants.Where(o => o.MemberInfo == typeof(Person).GetField("MaxAddresses")));
        }

        [Fact]
        public void WhenInitialized_ConstantsHaveCorrectValues()
        {
            var target = new TsClass(typeof(Person));

            var maxAddresses = target.Constants.Single(o => o.MemberInfo == typeof(Person).GetField("MaxAddresses"));
            Assert.Equal(Person.MaxAddresses, maxAddresses.ConstantValue);
        }
        
        [Fact]
		public void WhenInitializedWithClassWithBaseTypeObject_BaseTypeIsSetToNull() {
			var target = new TsClass(typeof(Address));

			Assert.Null(target.BaseType);
		}

		[Fact]
		public void WhenInitializedWithClassThatHasBaseClass_BaseTypeIsSet() {
			var target = new TsClass(typeof(Employee));

			Assert.NotNull(target.BaseType);
			Assert.Equal(typeof(Person), target.BaseType.Type);
		}

		[Fact]
		public void WhenInitializedWithClassThatHasBaseClass_OnlyPropertiesDefinedInDerivedClassAreCreated() {
			var target = new TsClass(typeof(Employee));

			Assert.Single(target.Properties.Where(o => o.MemberInfo == typeof(Employee).GetProperty("Salary")));

			Assert.Empty(target.Properties.Where(o => o.MemberInfo == typeof(Employee).GetProperty("Street")));
			Assert.Empty(target.Properties.Where(o => o.MemberInfo == typeof(Employee).GetProperty("Street")));
		}

		[Fact]
		public void WhenInitializedAndClassHasCustomNameInAttribute_CustomNameIsUsed() {
			var target = new TsClass(typeof(CustomClassName));

			Assert.Equal("MyClass", target.Name);
		}

		[Fact]
		public void WhenInitialized_ModuleIsSetToNamespaceModule() {
			var target = new TsClass(typeof(Address));

			Assert.NotNull(target.Module);
			Assert.Equal(typeof(Address).Namespace, target.Module.Name);
		}

        [Fact]
        public void WhenInitializedWithInnerClass_ModuleIsSetToNamespaceAndOuterClass() {
            var target = new TsClass(typeof(TypeLite.Tests.TestModels.Outer.Inner));

            Assert.NotNull(target.Module);
            Assert.Equal(typeof(TypeLite.Tests.TestModels.Outer.Inner).Namespace + ".Outer", target.Module.Name);
        }

        [Fact]
        public void WhenInitializedWithSecondLevelInnerClass_ModuleIsSetToNamespaceAndAllOuterClasses() {
            var target = new TsClass(typeof(TypeLite.Tests.TestModels.Outer.Inner.SecondLevelInner));

            Assert.NotNull(target.Module);
            Assert.Equal(typeof(TypeLite.Tests.TestModels.Outer.Inner).Namespace + ".Outer.Inner", target.Module.Name);
        }

        [Fact]
		public void WhenInitializedAndClassHasCustomModuleInAttribute_CustomModuleIsUsed() {
			var target = new TsClass(typeof(CustomClassName));

			Assert.Equal("MyModule", target.Module.Name);
		}

		#region Module property tests

		[Fact]
		public void WhenModuleIsSet_ClassIsAddedToModule() {
			var module = new TsModule("Tests");
			var target = new TsClass(typeof(Address));

			target.Module = module;

			Assert.Contains(target, module.Classes);
		}

		[Fact]
		public void WhenModuleIsSetToOtherModule_ClassIsRemovedFromOriginalModule() {
			var originalModule = new TsModule("Tests.Original");
			var module = new TsModule("Tests");
			var target = new TsClass(typeof(Address));

			target.Module = originalModule;
			target.Module = module;

			Assert.DoesNotContain(target, originalModule.Classes);
		}

		[Fact]
		public void WhenInitializedWithClassWithEnum_PropertiesCreated() {
			var target = new TsClass(typeof(Item));

			Assert.Single(target.Properties.Where(o => o.MemberInfo == typeof(Item).GetProperty("Type")));
			Assert.Single(target.Properties.Where(o => o.MemberInfo == typeof(Item).GetProperty("Id")));
			Assert.Single(target.Properties.Where(o => o.MemberInfo == typeof(Item).GetProperty("Name")));

			Assert.Null(target.BaseType);
		}

		[Fact]
		public void WhenInitializedWithClassWithEnum_EnumPropertyCreated() {
			var target = new TsClass(typeof(Item));

			Assert.Single(target.Properties.Where(o => o.MemberInfo == typeof(Item).GetProperty("Type")));
			var property = target.Properties.Single(o => o.MemberInfo == typeof(Item).GetProperty("Type"));
			Assert.True(property.PropertyType.GetType() == typeof(TsEnum));
			var enumtype = property.PropertyType as TsEnum;
			Assert.NotNull(enumtype);
			Assert.True(enumtype.Values.Any());
			Assert.True(enumtype.Values.Any(a => a.Name == "Book" && a.Value == ((int)ItemType.Book).ToString()));
			Assert.True(enumtype.Values.Any(a => a.Name == "Music" && a.Value == ((int)ItemType.Music).ToString()));
			Assert.True(enumtype.Values.Any(a => a.Name == "Clothing" && a.Value == ((int)ItemType.Clothing).ToString()));
			Assert.Null(target.BaseType);
		}

		#endregion
	}
}
