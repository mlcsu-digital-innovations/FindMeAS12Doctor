using System;
using System.Linq;
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
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Serilog;

namespace Mep.Api
{
  public class Startup
  {
    private const string ENV_AZURE_DEVELOPMENT = "AzureDevelopment";

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc()
              .AddNewtonsoftJson(options =>
              {
                options.SerializerSettings.PreserveReferencesHandling =
                  PreserveReferencesHandling.Objects;
              });

      // log all api bad requests
      services.PostConfigure<ApiBehaviorOptions>(options =>
      {
        var builtInFactory = options.InvalidModelStateResponseFactory;
        options.InvalidModelStateResponseFactory = context =>
        {
          Serilog.Log.Warning(
            "Bad Request {ActionName}: {ModelStateErrors}",
            context.ActionDescriptor.DisplayName,
            context.ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)));
          return builtInFactory(context);
        };
      });

      services.AddDbContext<ApplicationContext>
      (options =>
      {
        options.UseSqlServer(Configuration.GetConnectionString("MedicalExaminationsPortal"),
                             // https://docs.microsoft.com/en-us/azure/architecture/best-practices/retry-service-specific#sql-database-using-entity-framework-core
                             opt => opt.EnableRetryOnFailure());

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
          options.EnableSensitiveDataLogging();
          options.EnableDetailedErrors();
        }
      });

      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

      //services.AddScoped<IModelService<Ccg>, CcgService>();
      services.AddScoped<ICcgService, CcgService>();
      services.AddScoped<IModelService<DoctorStatus>, DoctorStatusService>();
      services.AddScoped<IModelService<Examination>, ExaminationService>();
      services.AddScoped<IModelService<ExaminationDetailType>, ExaminationDetailTypeService>();
      services.AddScoped<IModelService<GenderType>, GenderTypeService>();
      //services.AddScoped<IModelService<GpPractice>, GpPracticeService>();
      services.AddScoped<IGpPracticeService, GpPracticeService>();
      // services.AddScoped<IModelSearchService<Patient, Business.Models.SearchModels.PatientSearch>, PatientService>();
      services.AddScoped<IPatientService, PatientService>();
      services.AddScoped<IReferralService, ReferralService>();
      services.AddScoped<IModelService<ReferralStatus>, ReferralStatusService>();
      services.AddScoped<IModelService<Speciality>, SpecialityService>();
      //services.AddScoped<IModelService<User>, UserService>();
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IUnsuccessfulExaminationTypeService, UnsuccessfulExaminationTypeService>();

      services.AddScoped<IModelSimpleSearchService<AvailableDoctor, Business.Models.SearchModels.AvailableDoctorSearch>, AvailableDoctorService>();

      services.AddScoped<IModelGeneralSearchService<Ccg>, CcgSearchService>();
      services.AddScoped<IModelGeneralSearchService<GpPractice>, GpPracticeSearchService>();
      services.AddScoped<IModelGeneralSearchService<UserAmhp>, UserAmhpSearchService>();
      services.AddScoped<IModelGeneralSearchService<UserDoctor>, UserDoctorSearchService>();

      services.AddCors(options =>
      {
        options.AddPolicy("AllowAnyOrigin",
          builder =>
          {
            builder.AllowAnyOrigin();
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
          }
        );
      });
    }

    // This method gets called by the runtime. 
    // Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment() || env.IsEnvironment(ENV_AZURE_DEVELOPMENT))
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, 
        // see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();        
      }      
      app.UseExceptionHandler("/Error");
      app.UseSerilogRequestLogging();
      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseCors("AllowAnyOrigin");
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
