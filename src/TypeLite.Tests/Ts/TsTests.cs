using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TypeLite.Ts;
using TypeLite.TsConfiguration;

namespace TypeLite.Tests.Ts {
    public abstract class TsTests {
        protected Mock<ITsConfigurationProvider> _configurationProviderMock;
        protected Mock<TypeResolver> _typeResolverMock;

        public TsTests() {
            _configurationProviderMock = new Mock<ITsConfigurationProvider>();
            _typeResolverMock = new Mock<TypeResolver>();
        }

        protected TsBasicType SetupTypeResolverFor<T>() {
            var classType = typeof(T);

            var classResolvedType = new TsBasicType() { Context = classType, TypeName = classType.Name };
            _typeResolverMock
                .Setup(o => o.ResolveType(It.Is<Type>(t => t == classType)))
                .Returns(classResolvedType);

            return classResolvedType;
        }

        protected TsMemberConfiguration SetupConfigurationForMember<T>(string memberName) {
            var classType = typeof(T);
            var memberInfo = classType.GetTypeInfo().GetMembers().Where(o => o.Name == memberName).Single();

            var memberConfiguration = new TsMemberConfiguration() { Name = memberName };
            _configurationProviderMock
                .Setup(o => o.GetMemberConfiguration(It.Is<MemberInfo>(m => m == memberInfo)))
                .Returns(memberConfiguration);

            return memberConfiguration;
        }
    }
}
