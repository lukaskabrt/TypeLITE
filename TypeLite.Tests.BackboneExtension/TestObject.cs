using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLite.Tests.BackboneExtension
{
    class TestObject
    {
        public string Thing { get; set; }

        public Guid That { get; set; }

        public ReferencedObject RefObj { get; set; }
    }

    class ReferencedObject
    {
        public string TheOtherThing { get; set; }

        public EnumThing ThingOfEnum { get; set; }

    }

    enum EnumThing
    {
        This,
        That
    }
}
