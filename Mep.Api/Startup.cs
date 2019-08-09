using System;
using AutoMapper;
using Mep.Business;
using Mep.Business.Models;
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

      services.AddDbContext<ApplicationContext>
      (options =>
      {
        options.UseSqlServer(Configuration.GetConnectionString("MedicalExaminationsPortal"),
                             // https://docs.microsoft.com/en-us/azure/architecture/best-practices/retry-service-specific#sql-database-using-entity-framework-core
                             opt => opt.EnableRetryOnFailure());
        // TODO: Add EnableSensitiveDataLogging to development only configuration
        options.EnableSensitiveDataLogging();
        // TODO: Add EnableDetailedErrors to development only configuration
        options.EnableDetailedErrors();
      });

      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

      services.AddScoped<IModelService<Ccg>, CcgService>();
      services.AddScoped<IModelService<DoctorStatus>, DoctorStatusService>();
      services.AddScoped<IModelService<GpPractice>, GpPracticeService>();
      services.AddScoped<IModelSearchService<Patient, Business.Models.SearchModels.PatientSearchModel>, PatientService>();
      services.AddScoped<IModelService<Referral>, ReferralService>();
      services.AddScoped<IModelService<ReferralStatus>, ReferralStatusService>();
      services.AddScoped<IModelService<Speciality>, SpecialityService>();
      services.AddScoped<IModelService<User>, UserService>();
      services.AddScoped<IModelSimpleSearchService<AvailableDoctor, Business.Models.SearchModels.AvailableDoctorSearch>, AvailableDoctorService>();
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
