using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("StudentDbConnection")));
            services.AddMvc(options => options.EnableEndpointRouting = false).AddXmlSerializerFormatters();
            services.AddScoped<IStudent, SQLStudentRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            //FileServerOptions fileserverobject = new FileServerOptions();
            //fileserverobject.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileserverobject.DefaultFilesOptions.DefaultFileNames.Add("home.html");
            //app.UseFileServer(fileserverobject);//middleware


            //app.UseDefaultFiles();//middle ware
            //app.UseStaticFiles();//mioddle ware
            //app.UseFileServer();
            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();//for default route of mvc
            app.UseMvc(router =>
            {
                router.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {

            //        await context.Response.WriteAsync("Hello World: " + env.EnvironmentName);
            //    });
            //});
        }
    }
}
