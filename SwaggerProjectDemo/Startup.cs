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
            /*�ڴ�����һ������һ����������Ҫ�ݴ����ݵ����*/
            Configuration = configuration;
            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection();
            var config = builder.Build();
            config["key"] = "value";
            var value = config["key"];

            // ��ȡ��νṹ������ appsetings
            var connectionString1 = configuration.GetConnectionString("DefaultConnection");
            var connectionString2 = configuration["ConnectionStrings:DefaultConnection"];

            // �����ļ��ṩ����
            var config2 = new ConfigurationBuilder().
                SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json",true,true).Build();
            var conn = config2.GetSection("ConnectionStrings:DefaultConnection").Value;

        }

        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // ע��------�ṩʵ��(IServiceProvider)

            // Transient--ÿһ��GetService���ᴴ��һ���µ�ʵ��
            // Scoped--ͬһ��Scope��ֻ��ʼ��һ��ʵ�� ���������Ϊ�� ÿһ��request����ֻ����һ��ʵ����ͬһ��http request����һ�� scope�ڣ�
            // Singleton---����Ӧ�ó���������������ֻ����һ��ʵ�� 
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
            // ���Զ������ö���

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
