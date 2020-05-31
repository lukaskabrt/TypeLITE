using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLite.Tests.TestModels.Namespace2
{
    [TsClass]
    public class DifferentNamespaces_Class2 
    {
        public string Property2 { get; set; }
        public string Property1 { get; set; }
    }
}

namespace TypeLite.Tests.TestModels.Namespace1
{
    [TsClass]
    public class DifferentNamespaces_Class3
    {
        
    }
}

namespace TypeLite.Tests.TestModels.Namespace2 
{
    [TsClass]
    public class DifferentNamespaces_Class1
    {

    }
}
