using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerProjectDemo.Implements
{
    public class AppSettings
    {
        public DefaultConn ConnectionStrings { get; set; }
        public string AllowedHosts { get; set; }
    }
    public class DefaultConn
    {
        public string DefaultConnection { get; set; }
    }
}

