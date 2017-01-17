using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLite.Tests.Models
{
    class ClassWithDerivedInterface : IDerivedInterface
    {
    }

    interface IBaseInterface { }
    interface IDerivedInterface : IBaseInterface { }
}
