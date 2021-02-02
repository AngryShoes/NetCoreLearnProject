using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SwaggerProjectDemo.Interfaces;

namespace SwaggerProjectDemo.Implements
{
    public class ProjectManager : IProjectManager
    {
        private readonly ILogger<ProjectManager> _logger;
        public ProjectManager(ILogger<ProjectManager> logger)
        {
            _logger = logger;
        }
        public void WriteProjectName()
        {
            _logger.LogInformation("ProjectManager.WriteProjectName.");
            Console.WriteLine("Write project name");
        }
    }
}
