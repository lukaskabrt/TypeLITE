using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TypeLite.Tests.TestModels;
using TypeLite.TsModels;
using Xunit;

namespace TypeLite.Tests {
	public class TsModelTests {
		[Fact]
		public void WhenInitialized_ClassesCollectionIsEmpty() {
			var target = new TsModel();

			Assert.NotNull(target.Classes);
			Assert.Empty(target.Classes);
		}

		[Fact]
		public void WhenInitialized_ReferencesCollectionIsEmpty() {
			var target = new TsModel();

			Assert.NotNull(target.References);
			Assert.Empty(target.References);
		}

		[Fact]
		public void WhenInitialized_ModulesCollectionIsEmpty() {
			var target = new TsModel();

			Assert.NotNull(target.Modules);
			Assert.Empty(target.Modules);
		}

		[Fact]
		public void WhenInitializedWithCollectionOfClasses_ClassesAreAddedToModel() {
			var classes = new[] { new TsClass(typeof(Person)) };

			var target = new TsModel(classes);

			Assert.Equal(classes, target.Classes);
		}

		#region RunVisitor tests

		[Fact]
		public void WhenRunVisitor_VisitModelIsCalledForModel() {
			var visitor = new Mock<TsModelVisitor>();
			var builder = new TsModelBuilder();
			builder.Add(typeof(Person), true);

			var target = builder.Build();

			visitor.Setup(o => o.VisitModel(target)).Verifiable();

			target.RunVisitor(visitor.Object);

			visitor.VerifyAll();
		}

		[Fact]
		public void WhenRunVisitor_VisitModulIsCalledForModules() {
			var visitor = new Mock<TsModelVisitor>();
			visitor.Setup(o => o.VisitModule(It.Is<TsModule>(m => m.Name == typeof(Person).Namespace))).Verifiable();

			var builder = new TsModelBuilder();
			builder.Add(typeof(Person), true);

			var target = builder.Build();
			target.RunVisitor(visitor.Object);

			visitor.VerifyAll();
		}

		[Fact]
		public void WhenRunVisitor_VisitClassIsCalledForClassesOfModel() {
			var visitor = new Mock<TsModelVisitor>();
			visitor.Setup(o => o.VisitClass(It.Is<TsClass>(c => c.Type == typeof(Person)))).Verifiable();
			visitor.Setup(o => o.VisitClass(It.Is<TsClass>(c => c.Type == typeof(Address)))).Verifiable();

			var builder = new TsModelBuilder();
			builder.Add(typeof(Person), true);

			var target = builder.Build();
			target.RunVisitor(visitor.Object);

			visitor.VerifyAll();
		}

		[Fact]
		public void WhenRunVisitor_VisitPropertyIsCalledForPropertiesOfModelClasses() {
			var visitor = new Mock<TsModelVisitor>();
			visitor.Setup(o => o.VisitProperty(It.Is<TsProperty>(p => p.Name == "Street"))).Verifiable();
			visitor.Setup(o => o.VisitProperty(It.Is<TsProperty>(p => p.Name == "Town"))).Verifiable();

			var builder = new TsModelBuilder();
			builder.Add(typeof(Address), true);

			var target = builder.Build();
			target.RunVisitor(visitor.Object);

			visitor.VerifyAll();
		}

		[Fact]
		public void WhenRunVisitor_VisitPropertyIsCalledForFieldsOfModelClasses() {
			var visitor = new Mock<TsModelVisitor>();
			visitor.Setup(o => o.VisitProperty(It.Is<TsProperty>(p => p.Name == "PostalCode"))).Verifiable();

			var builder = new TsModelBuilder();
			builder.Add(typeof(Address), true);

			var target = builder.Build();
			target.RunVisitor(visitor.Object);

			visitor.VerifyAll();
		}

		[Fact]
		public void WhenRunVisitor_VisitEnumIsCalledForEnumsOfModel() {
			var visitor = new Mock<TsModelVisitor>();
			visitor.Setup(o => o.VisitEnum(It.Is<TsEnum>(c => c.Type == typeof(ContactType)))).Verifiable();

			var builder = new TsModelBuilder();
			builder.Add(typeof(Address), true);

			var target = builder.Build();
			target.RunVisitor(visitor.Object);

			visitor.VerifyAll();
		}

		#endregion
	}
}
