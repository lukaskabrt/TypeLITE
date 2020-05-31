using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TypeLite;
using TypeLite.TsModels;
using TypeLite.Tests.TestModels;

using Xunit;
using Moq;

namespace TypeLite.Tests {
	public class TsModelBuilderTests {

		#region Add tests

		[Fact]
		public void WhenAddTypeThatIsntClassStructOrEnum_ExceptionIsThrown() {
			var target = new TsModelBuilder();

			Assert.Throws<ArgumentException>(() => target.Add(typeof(string)));
		}

        [Fact]
        public void WhenAddEnum_EnumIsAddedToModel() {
            var target = new TsModelBuilder();
            target.Add(typeof(CustomerKind));

            Assert.Single(target.Enums.Values.Where(o => o.Type == typeof(CustomerKind)));
        }

		[Fact]
		public void WhenAdd_ClassIsAddedToModel() {
			var target = new TsModelBuilder();

			target.Add(typeof(Address), true);

			Assert.Single(target.Classes.Values.Where(o => o.Type == typeof(Address)));
		}

		[Fact]
		public void WhenAddAndIncludeReferencesIsFalse_ReferencedClassesAreNotAddedToModel() {
			var target = new TsModelBuilder();

			target.Add(typeof(Person), false);

			Assert.Single(target.Classes.Values.Where(o => o.Type == typeof(Person)));
			Assert.Empty(target.Classes.Values.Where(o => o.Type == typeof(Address)));
		}

		[Fact]
		public void WhenAddAndIncludeReferencesIsTrue_ReferencedClassesAreAddedToModel() {
			var target = new TsModelBuilder();

			target.Add(typeof(Person), true);

			Assert.Single(target.Classes.Values.Where(o => o.Type == typeof(Person)));
			Assert.Single(target.Classes.Values.Where(o => o.Type == typeof(Address)));
		}

		[Fact]
		public void WhenAddAndClassHasBaseClass_BaseClassIsAddedToModel() {
			var target = new TsModelBuilder();

			target.Add(typeof(Employee), false);

			Assert.Single(target.Classes.Values.Where(o => o.Type == typeof(Employee)));
			Assert.Single(target.Classes.Values.Where(o => o.Type == typeof(Person)));
		}

		[Fact]
		public void WhenAddClassWithReferenceAndReferenceIsCollectionOfCustomType_CustomTypeIsAddedToModel() {
			var target = new TsModelBuilder();

			target.Add<CustomTypeCollectionReference>(true);

			Assert.Single(target.Classes.Values.Where(o => o.Type == typeof(CustomTypeCollectionReference)));
			Assert.Single(target.Classes.Values.Where(o => o.Type == typeof(Product)));
		}

		[Fact]
		public void WhenAddClassWithReferenceAndReferenceIsIEnumerableOfCustomType_CustomTypeIsAddedToModel() {
			var target = new TsModelBuilder();

			target.Add<CustomTypeCollectionReference>(true);

			Assert.Single(target.Classes.Values.Where(o => o.Type == typeof(CustomTypeCollectionReference)));
			Assert.Single(target.Classes.Values.Where(o => o.Type == typeof(Person)));
		}

        [Fact]
        public void WhenInterfaceIsAdded_InterfaceIsAddedAsClass() {
            var target = new TsModelBuilder();

            target.Add<IShippingService>(true);

            Assert.Single(target.Classes.Values.Where(o => o.Type == typeof(IShippingService)));
        }

		#endregion

		#region Add(Assembly) tests

		[Fact]
		public void WhenAdd_AllClassesWithTsClassAttributeAreAdded() {
			var target = new TsModelBuilder();
			target.Add(typeof(Product).Assembly);

			Assert.Single(target.Classes.Values.Where(o => o.Type == typeof(Product)));
		}

		#endregion

		#region Build tests

		[Fact]
		public void WhenBuild_ModelWithAddedClassesIsReturned() {
			var target = new TsModelBuilder();
			target.Add(typeof(Person), true);

			var model = target.Build();

			Assert.Equal(target.Classes.Values, model.Classes);
		}

		[Fact]
		public void WhenBuild_ModelWithModulesIsReturned() {
			var target = new TsModelBuilder();
			target.Add(typeof(Person), true);

			var model = target.Build();

			var module = model.Modules.Where(m => m.Name == "TypeLite.Tests.TestModels").Single();
			var personClass = model.Classes.Where(o => o.Type == typeof(Person)).Single();

			Assert.Same(personClass.Module, module);
		}

		[Fact]
		public void WhenBuild_TypeReferencesInModelAreResolved() {
			var target = new TsModelBuilder();
			target.Add(typeof(Person), true);

			var model = target.Build();

			var personClass = model.Classes.Where(o => o.Type == typeof(Person)).Single();
			var addressClass = model.Classes.Where(o => o.Type == typeof(Address)).Single();

			Assert.Same(addressClass, personClass.Properties.Where(p => p.Name == "PrimaryAddress").Single().PropertyType);
			Assert.IsType<TsSystemType>(personClass.Properties.Where(p => p.Name == "Name").Single().PropertyType);
			Assert.IsType<TsCollection>(personClass.Properties.Where(p => p.Name == "Addresses").Single().PropertyType);

            Assert.IsType<TsSystemType>(personClass.Fields.Where(f => f.Name == "PhoneNumber").Single().PropertyType);

            Assert.IsType<TsSystemType>(personClass.Constants.Where(c => c.Name == "MaxAddresses").Single().PropertyType);
        }

		[Fact]
		public void WhenBuild_ModulesInModelAreResolved() {
			var target = new TsModelBuilder();
			target.Add(typeof(Person));

			var model = target.Build();

			var personClass = model.Classes.Where(o => o.Type == typeof(Person)).Single();
			var addressClass = model.Classes.Where(o => o.Type == typeof(Address)).Single();

			Assert.Same(personClass.Module, addressClass.Module);
		}

		#endregion
	}
}
