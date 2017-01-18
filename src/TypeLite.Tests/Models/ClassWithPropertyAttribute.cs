using System;
using System.Collections.Generic;
using System.Text;
using TypeLite.TsConfiguration.Attributes;

namespace TypeLite.Tests.Models
{
    class ClassWithPropertyAttribute
    {
        [TsMember(Name = "PropertyWithAttributeName")]
        public int PropertyWithAttribute { get; set; }
        public int PropertyWithoutAttribute { get; set; }
    }
}
