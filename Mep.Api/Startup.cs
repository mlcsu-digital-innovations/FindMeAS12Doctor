using System;
using AutoMapper;
using Mep.Business;
using Mep.Business.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mep.Api
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
      string connection = 
        "Server=localhost" +
        ";DataBase=MedicalExaminationsPortal;" +
        ";Trusted_Connection=true" +
        ";MultipleActiveResultSets=true;" +
        ";Application Name=MedicalExaminationsPortal;";

      services.AddDbContext<ApplicationContext>
        (options => options.UseSqlServer(
          connection));

      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

      services.AddScoped<ISpecialityService, SpecialityService>();     
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

      app.UseExceptionHandler("/Error");
      app.UseHttpsRedirection();
      app.UseMvc();
    }
  }
}
