using System;
using AutoMapper;
using Mep.Business;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace mep.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //TODO: Add this to a config file
            string connection = "Data Source=MedicalExaminationsPortal.db";

            services.AddDbContext<ApplicationContext>
                (options => options.UseSqlite(
                    connection, 
                    builder => builder.MigrationsAssembly(typeof(Startup).Assembly.FullName)));

            services.AddDbContext<AuditContext>
                (options => options.UseSqlite(
                    connection, 
                    builder => builder.MigrationsAssembly(typeof(Startup).Assembly.FullName)));

            services.AddDbContext<MigrationContext>
                (options => options.UseSqlite(
                    connection, 
                    builder => builder.MigrationsAssembly(typeof(Startup).Assembly.FullName)));                    

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
