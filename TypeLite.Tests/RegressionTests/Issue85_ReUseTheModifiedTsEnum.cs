﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TypeLite.Tests.RegressionTests
{
    public class Issue85_ReUseTheModifiedTsEnums
    {
        [Fact]
        public void WhenToModuleTheEnum()
        {
            var ts = TypeScript.Definitions()
                .For<MyTestEnum>().ToModule("Foo")
                .For<MyClass>()
                .Generate();
            Assert.Contains("MyTest: Foo.MyTestEnum;", ts);
        }

        enum MyTestEnum
        {
            One,
            Two,
            Three
        }

        class MyClass
        {
            public MyTestEnum MyTest { get; set; }
        }
    }
}
