﻿using Microsoft.Extensions.DependencyInjection;
using Paukertj.Autoconverter.Primitives.Services.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paukertj.Autoconverter.Demo
{
    internal class TestConverter : IConverter<InfrastructureObject, DomainObject>
    {
        public DomainObject Convert(InfrastructureObject from)
        {
            return new DomainObject
            {
                FirstName = from.FirstName,
                LastName = from.LastName
            };
        }
    }
}


