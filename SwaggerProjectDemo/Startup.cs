using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SwaggerProjectDemo.Implements;
using SwaggerProjectDemo.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerProjectDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //   memory configuration
            /*内存配置一般用在一次请求中需要暂存数据的情况*/
            Configuration = configuration;
            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection();
            var config = builder.Build();
            config["key"] = "value";
            var value = config["key"];

            // 读取层次结构的配置 appsetings
            var connectionString1 = configuration.GetConnectionString("DefaultConnection");
            var connectionString2 = configuration["ConnectionStrings:DefaultConnection"];

            // 配置文件提供程序
            var config2 = new ConfigurationBuilder().
                SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json",true,true).Build();
            var conn = config2.GetSection("ConnectionStrings:DefaultConnection").Value;

        }

        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 注册------提供实例(IServiceProvider)

            // Transient--每一次GetService都会创建一个新的实例
            // Scoped--同一个Scope内只初始化一个实例 ，可以理解为（ 每一个request级别只创建一个实例，同一个http request会在一个 scope内）
            // Singleton---整个应用程序生命周期以内只创建一个实例 
            services.AddTransient<IProjectManager, ProjectManager>();
            services.AddSingleton<ISingletonService, ShowGuid>();
            services.AddScoped<IScopedService, ShowGuid>();
            services.AddTransient<ITransientService, ShowGuid>();
            //serviceProvider.GetService<ProjectManager>().WriteProjectName();
            services.AddControllers();
            services.AddRouting();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SwaggerProjectDemo", Version = "v1" });
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlPath = Path.Combine(basePath, "SwaggerProjectDemo.xml");
                c.IncludeXmlComments(xmlPath);
            });
            // services.AddOptions();
            // 绑定自定义配置对象

            services.Configure<AppSettings>(Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SwaggerProjectDemo v1"));
            }
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
