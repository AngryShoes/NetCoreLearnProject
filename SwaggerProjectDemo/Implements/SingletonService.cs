using SwaggerProjectDemo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerProjectDemo.Implements
{
    public class SingletonService : ISingletonService
    {
        public Guid Guid => Guid.NewGuid();
    }
}
