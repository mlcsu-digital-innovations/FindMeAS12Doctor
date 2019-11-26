using System;
using System.Linq;
using AutoMapper;
using Fmas12d.Business;
using Fmas12d.Business.Models;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using Serilog;

namespace Fmas12d.Api
{
  public class Startup
  {
    private const string ENV_AZURE_DEVELOPMENT = "AzureDevelopment";
    private const string ENV_DEVELOPMENT = "Development";
    private const string ENV_DISABLEAUTHENTICATION = "DisableAuthentication";
    private const string ENV_POSTMAN = "Postman";

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      string aspNetCoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

      bool IsDevelopment =
        aspNetCoreEnvironment == ENV_AZURE_DEVELOPMENT ||
        aspNetCoreEnvironment == ENV_DEVELOPMENT ||
        aspNetCoreEnvironment == ENV_DISABLEAUTHENTICATION ||
        aspNetCoreEnvironment == ENV_POSTMAN;

      if (aspNetCoreEnvironment == ENV_DISABLEAUTHENTICATION) {
        services.AddSingleton<IPolicyEvaluator, DisableAuthenticationPolicyEvaluator>();
      }

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(o =>
      {
        o.Authority = Configuration["jwtBearer:authority"];
        o.Audience = Configuration["jwtBearer:audience"];
        if (IsDevelopment)
        {
          o.RequireHttpsMetadata = false;
          o.SaveToken = true;
        };
      });

      services.AddAuthorization(
        options =>
      {


        // POSTMAN doesn't get the correct audience using the oauth2 endpoint
        // so until it does we'll have to fudge the User policy
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == ENV_POSTMAN)
        {

          options.AddPolicy("Admin", policy =>
            policy.RequireClaim("aud", "c898ea46-4e6e-4e55-b53b-8ae61c825507"));

          options.AddPolicy("AMHP", policy =>
            policy.RequireClaim("aud", "c898ea46-4e6e-4e55-b53b-8ae61c825507"));

          options.AddPolicy("Doctor", policy =>
            policy.RequireClaim("aud", "c898ea46-4e6e-4e55-b53b-8ae61c825507"));

          options.AddPolicy("Finance", policy =>
            policy.RequireClaim("aud", "c898ea46-4e6e-4e55-b53b-8ae61c825507"));

          options.AddPolicy("SystemAdmin", policy =>
            policy.RequireClaim("aud", "c898ea46-4e6e-4e55-b53b-8ae61c825507"));

          options.AddPolicy("User", policy =>
            policy.RequireClaim("aud", "c898ea46-4e6e-4e55-b53b-8ae61c825507"));
        }
        else
        {
          options.AddPolicy("Admin", policy =>
            policy.RequireClaim("JobTitle", "Admin", "SystemAdmin"));

          options.AddPolicy("AMHP", policy =>
          policy.RequireClaim("JobTitle", "Admin", "AMHP", "SystemAdmin"));

          options.AddPolicy("Doctor", policy =>
            policy.RequireClaim("JobTitle", "Admin", "Doctor", "SystemAdmin"));

          options.AddPolicy("Finance", policy =>
            policy.RequireClaim("JobTitle", "Admin", "Finance", "SystemAdmin"));

          options.AddPolicy("SystemAdmin", policy =>
            policy.RequireClaim("JobTitle", "SystemAdmin"));

          options.AddPolicy("User", policy =>
            policy.RequireClaim("JobTitle", "Admin", "AMHP", "Doctor", "Finance", "SystemAdmin"));
        }

      });

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
        options.UseSqlServer(Configuration.GetConnectionString("Fmas12d"),
                             // https://docs.microsoft.com/en-us/azure/architecture/best-practices/retry-service-specific#sql-database-using-entity-framework-core
                             opt => opt.EnableRetryOnFailure());

        if (IsDevelopment)
        {
          options.EnableSensitiveDataLogging();
          options.EnableDetailedErrors();
        }
      });

      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

      services.AddScoped<IAssessmentDetailTypeService, AssessmentDetailTypeService>();
      services.AddScoped<ICcgService, CcgService>();
      services.AddScoped<IContactDetailsService, ContactDetailsService>();
      services.AddScoped<IGenderTypeService, GenderTypeService>();
      services.AddScoped<IGpPracticeService, GpPracticeService>();
      services.AddScoped<ILocationDetailService, LocationDetailService>();
      services.AddScoped<IAssessmentService, AssessmentService>();
      services.AddScoped<IModelService<ReferralStatus>, ReferralStatusService>();
      services.AddScoped<IPatientService, PatientService>();
      services.AddScoped<IReferralService, ReferralService>();
      services.AddScoped<ISpecialityService, SpecialityService>();
      services.AddScoped<IUnsuccessfulAssessmentTypeService, UnsuccessfulAssessmentTypeService>();
      services.AddScoped<IUserAvailabilityService, UserAvailabilityService>();
      services.AddScoped<IUserNotificationService, UserNotificationService>();
      services.AddScoped<IUserService, UserService>();

      // services.AddScoped<IModelSimpleSearchService<AvailableDoctor, Business.Models.SearchModels.AvailableDoctorSearch>, AvailableDoctorService>();

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
      if (env.IsDevelopment() ||
          env.IsEnvironment(ENV_AZURE_DEVELOPMENT) ||
          env.IsEnvironment(ENV_POSTMAN))
      {
        app.UseDeveloperExceptionPage();
        IdentityModelEventSource.ShowPII = true;
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
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      Serilog.Log.Information(Configuration["ConnectionStrings:fmas12d"]);
    }
  }
}
