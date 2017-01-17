﻿using Moq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TypeLite.Tests.Models;
using TypeLite.Ts;
using TypeLite.TsConfiguration;
using Xunit;

namespace TypeLite.Tests.Ts {
    public class TsClassTests {
        private Mock<ITsConfigurationProvider> _configurationProviderMock;
        private Mock<TypeResolver> _typeResolverMock;

        public TsClassTests() {
            _configurationProviderMock = new Mock<ITsConfigurationProvider>();
            _typeResolverMock = new Mock<TypeResolver>();
        }

        [Fact]
        public void WhenInitialized_InterfacesCollectionIsEmpty() {
            var typeName = new TsBasicType() { Context = typeof(ClassWithoutAttribute), TypeName = "ClassWithoutAttribute" };
            var sut = new TsClass(typeName);

            Assert.Empty(sut.Interfaces);
        }

        [Fact]
        public void WhenInitialized_NameIsSet() {
            var typeName = new TsBasicType() { Context = typeof(ClassWithoutAttribute), TypeName = "ClassWithoutAttribute" };
            var sut = new TsClass(typeName);

            Assert.Same(typeName, sut.Name);
        }

        [Fact]
        public void WhenCreateFromClass_NameIsSetToTypeNameReturnedByTypeResolver() {
            var classResolvedType = this.SetupTypeResolverFor<ClassWithoutAttribute>();

            var @class = TsClass.CreateFrom<ClassWithoutAttribute>(_typeResolverMock.Object, _configurationProviderMock.Object);

            Assert.Same(classResolvedType, @class.Name);
        }

        [Fact]
        public void WhenCreateFromClassWithBaseClass_BaseTypeIsSetToTypeNameReturnedByTypeResolver() {
            var classResolvedType = this.SetupTypeResolverFor<DerivedClass>();
            var baseClassResolvedType = this.SetupTypeResolverFor<BaseClass>();

            var @class = TsClass.CreateFrom<DerivedClass>(_typeResolverMock.Object, _configurationProviderMock.Object);

            Assert.Same(baseClassResolvedType, @class.BaseType);
        }

        [Fact]
        public void WhenCreateFromClassWithoutSpecificBaseClass_BaseTypeIsNull() {
            var baseClassResolvedType = this.SetupTypeResolverFor<BaseClass>();
            var objectClassResolvedType = this.SetupTypeResolverFor<object>();

            var @class = TsClass.CreateFrom<BaseClass>(_typeResolverMock.Object, _configurationProviderMock.Object);

            Assert.Null(@class.BaseType);
        }

        [Fact]
        public void WhenCreateFromClassWitoutInterfaces_InterfacesCollectionIsEmpty() {
            var classResolvedType = this.SetupTypeResolverFor<BaseClass>();

            var @class = TsClass.CreateFrom<DerivedClass>(_typeResolverMock.Object, _configurationProviderMock.Object);

            Assert.Empty(@class.Interfaces);
        }

        [Fact]
        public void WhenCreateFromClassWitInterfaces_InterfacesAreSetToTypesReturnedByTypeResolver() {
            var classResolvedType = this.SetupTypeResolverFor<ClassWithIntefaces>();
            var interface1ResolvedType = this.SetupTypeResolverFor<IInterfaceForClassWithInterfaces>();
            var interface1ResolvedType2 = this.SetupTypeResolverFor<IInterfaceForClassWithInterfaces2>();

            var @class = TsClass.CreateFrom<ClassWithIntefaces>(_typeResolverMock.Object, _configurationProviderMock.Object);

            Assert.Equal(2, @class.Interfaces.Count);
            Assert.Contains(interface1ResolvedType, @class.Interfaces);
            Assert.Contains(interface1ResolvedType2, @class.Interfaces);
        }

        [Fact]
        public void WhenCreateFromClassWithInterfaces_InterfacesCollectionDoesNotContainsInterfacesNotResolvedByTypeResolver() {
            var classResolvedType = this.SetupTypeResolverFor<ClassWithIntefaces>();
            var interface1ResolvedType = this.SetupTypeResolverFor<IInterfaceForClassWithInterfaces>();

            var @class = TsClass.CreateFrom<ClassWithIntefaces>(_typeResolverMock.Object, _configurationProviderMock.Object);

            Assert.Equal(1, @class.Interfaces.Count);
            Assert.Contains(interface1ResolvedType, @class.Interfaces);
        }

        [Fact]
        public void WhenCreateFromClassWithDerivedInterface_InterfacesCollectionDoesNotContainsBaseInterface() {
            var classResolvedType = this.SetupTypeResolverFor<ClassWithDerivedInterface>();
            var interfaceResolvedType = this.SetupTypeResolverFor<IDerivedInterface>();
            this.SetupTypeResolverFor<IBaseInterface>();
            
            var @class = TsClass.CreateFrom<ClassWithDerivedInterface>(_typeResolverMock.Object, _configurationProviderMock.Object);

            Assert.Equal(1, @class.Interfaces.Count);
            Assert.Contains(interfaceResolvedType, @class.Interfaces);
        }

        private TsBasicType SetupTypeResolverFor<T>() {
            var classType = typeof(T);

            var classResolvedType = new TsBasicType() { Context = classType, TypeName = classType.Name };
            _typeResolverMock
                .Setup(o => o.ResolveType(It.Is<Type>(t => t == classType)))
                .Returns(classResolvedType);

            return classResolvedType;
        }
    }
}
