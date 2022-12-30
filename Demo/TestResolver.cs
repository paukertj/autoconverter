using Paukertj.Autoconverter.Primitives.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paukertj.Autoconverter.Demo
{
    internal class TestResolver : Resolver
    {
        public override void Resolve()
        {
            Map<DomainObject, InfrastructureObject>()
                .Ignore(d => d.Id)
                .Resolve(s => s.LastName, d => d.LastName);
        }
    }
}
