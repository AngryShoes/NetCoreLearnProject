using SwaggerProjectDemo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerProjectDemo.Implements
{
    public class TransientService : ITransientService
    {
        public Guid Guid => Guid.NewGuid();
    }
}