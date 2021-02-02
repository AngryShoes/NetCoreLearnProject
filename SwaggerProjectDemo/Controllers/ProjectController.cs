using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SwaggerProjectDemo.Implements;
using SwaggerProjectDemo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerProjectDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectManager _projectManager;
        private readonly IOptions<AppSettings> _options;
        public ProjectController(IProjectManager projectManager,IOptions<AppSettings> options)
        {
            _projectManager = projectManager;
            _options = options;
        }
        /// <summary>
        /// Get Project by Id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet(nameof(GetProject))]
        public async Task<string> GetProject(string projectId)
        {
            _projectManager.WriteProjectName();
            // do something
            return await Task.FromResult($"Project Id: {projectId}");
        }
        /// <summary>
        /// Set Project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost(nameof(SetProject))]
        public async Task<string> SetProject(string projectId)
        {
            // do something
            return await Task.FromResult($"Project has been set. Id: {projectId}");
        }
        /// <summary>
        /// Property injection
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// 
        [HttpGet(nameof(PrintProjectName))]
        public async Task<string> PrintProjectName([FromServices]IProjectManager projectManager)
        {
            projectManager.WriteProjectName();
            return await Task.FromResult("parameter injection");
        }
        /// <summary>
        /// Get Service in different life cycle
        /// </summary>
        /// <param name="singletonService1"></param>
        /// <param name="singletonService2"></param>
        /// <param name="transientService1"></param>
        /// <param name="transientService2"></param>
        /// <param name="scopedService1"></param>
        /// <param name="scopedService2"></param>
        /// <returns></returns>
        [HttpGet(nameof(GetServices))]
        public string GetServices([FromServices]ISingletonService singletonService1, [FromServices] ISingletonService  singletonService2,
            [FromServices]ITransientService transientService1,[FromServices] ITransientService transientService2,
            [FromServices]IScopedService scopedService1,[FromServices] IScopedService scopedService2)
        {
            Console.WriteLine("***************************************************************");
            Console.WriteLine($"Scoped:   {scopedService1.Guid}");
            Console.WriteLine($"Scoped:   {scopedService2.Guid}");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"Transient:   {transientService1.Guid}");
            Console.WriteLine($"Transient:   {transientService2.Guid}");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"Singleton:   {singletonService1.Guid}");
            Console.WriteLine($"Singleton:   {singletonService2.Guid}");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("***************************************************************");

            return "success";
        }

        /// <summary>
        /// Get configuration by binding model
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetConfiguration))]
        public string GetConfiguration()
        {
            var allowedHosts = _options.Value.AllowedHosts;
            var conn = _options.Value.ConnectionStrings.DefaultConnection;
            return conn;
        }
    }
}
