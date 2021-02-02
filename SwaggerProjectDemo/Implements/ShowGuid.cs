using SwaggerProjectDemo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerProjectDemo.Implements
{
    public class ShowGuid : ISingletonService, IScopedService, ITransientService
    {
        public ShowGuid() : this(Guid.NewGuid())
        {

        }
        public ShowGuid(Guid guid)
        {
            Guid = guid;
        }
        public Guid Guid { get; private set; }
    }
}
