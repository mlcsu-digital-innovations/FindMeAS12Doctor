using Fmas12d.Api.Extensions;
using Fmas12d.Api.SignalR;
using Fmas12d.Business;
using Fmas12d.Business.Models;
using Fmas12d.Business.Services;
using Fmas12d.Business.Helpers;
using Fmas12d.Models;
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
using System;
using System.Linq;

namespace Fmas12d.Api
{
  public class Startup
  {
    internal const string ENV_AIMES_UAT = "AimesUat";
    internal const string ENV_AZURE_DEVELOPMENT = "AzureDevelopment";
    internal const string ENV_DEVELOPMENT = "Development";
    internal const string ENV_DISABLEAUTHENTICATION = "DisableAuthentication";

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
        aspNetCoreEnvironment == ENV_DISABLEAUTHENTICATION;

      if (aspNetCoreEnvironment == ENV_DISABLEAUTHENTICATION)
      {
        services.AddSingleton<IPolicyEvaluator, DisableAuthenticationPolicyEvaluator>();
      }

      services.AddSignalR();

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

        options.AddPolicy("Admin", policy =>
          policy.RequireClaim(
            UserClaimsService.CLAIM_PROFILETYPEID,
            ProfileType.ADMIN.ToString(),
            ProfileType.SYSTEM.ToString()
          )
        );

        options.AddPolicy("AMHP", policy =>
          policy.RequireClaim(
            UserClaimsService.CLAIM_PROFILETYPEID,
            ProfileType.ADMIN.ToString(),
            ProfileType.AMHP.ToString(),
            ProfileType.SYSTEM.ToString()
          )
        );

        options.AddPolicy("Doctor", policy =>
          policy.RequireClaim(
            UserClaimsService.CLAIM_PROFILETYPEID,
            ProfileType.ADMIN.ToString(),
            ProfileType.GP.ToString(),
            ProfileType.PSYCHIATRIST.ToString(),
            ProfileType.SYSTEM.ToString()
          )
        );

        options.AddPolicy("Finance", policy =>
          policy.RequireClaim(
            UserClaimsService.CLAIM_PROFILETYPEID,
            ProfileType.ADMIN.ToString(),
            ProfileType.FINANCE.ToString(),
            ProfileType.SYSTEM.ToString()
          )
        );

        options.AddPolicy("SystemAdmin", policy =>
          policy.RequireClaim(
            UserClaimsService.CLAIM_PROFILETYPEID,
            ProfileType.SYSTEM.ToString()
          )
        );

        options.AddPolicy("User", policy =>
          policy.RequireClaim(
            UserClaimsService.CLAIM_PROFILETYPEID,
            ProfileType.ADMIN.ToString(),
            ProfileType.AMHP.ToString(),
            ProfileType.GP.ToString(),
            ProfileType.PSYCHIATRIST.ToString(),
            ProfileType.FINANCE.ToString(),
            ProfileType.SYSTEM.ToString()
          )
        );
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

      services.AddScoped<IAssessmentDetailTypeService, AssessmentDetailTypeService>();
      services.AddScoped<IAssessmentService, AssessmentService>();
      services.AddScoped<ICcgService, CcgService>();
      services.AddScoped<IContactDetailTypeService, ContactDetailTypeService>();
      services.AddScoped<IContactDetailsService, ContactDetailsService>();
      services.AddScoped<IGenderTypeService, GenderTypeService>();
      services.AddScoped<IGpPracticeService, GpPracticeService>();
      services.AddScoped<ILocationDetailService, LocationDetailService>();
      services.AddScoped<IPatientService, PatientService>();
      services.AddScoped<IReferralService, ReferralService>();
      services.AddScoped<IReferralStatusService, ReferralStatusService>();
      services.AddScoped<IRequestResponseLogService, RequestResponseLogService>();
      services.AddScoped<ISection12LiveRegisterService, Section12LiveRegisterService>();
      services.AddScoped<ISpecialityService, SpecialityService>();
      services.AddScoped<IUnsuccessfulAssessmentTypeService, UnsuccessfulAssessmentTypeService>();
      services.AddScoped<IUserAvailabilityService, UserAvailabilityService>();
      services.AddScoped<IUserClaimsService, UserClaimsService>();
      services.AddScoped<IUserNotificationService, UserNotificationService>();
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IUserAssessmentClaimService, UserAssessmentClaimService>();
      services.AddScoped<IFinanceAssessmentClaimService, FinanceAssessmentClaimService>();
      services.AddScoped<IDistanceCalculationService, DistanceCalculationService>();

      services.AddHttpContextAccessor();

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
          env.IsEnvironment(ENV_AZURE_DEVELOPMENT))
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

      app.UseCors("AllowAnyOrigin");      
      app.UseAuthentication();
      app.UseRouting();
      app.UseUserClaims();
      app.UseMiddleware<RequestResponseLoggingMiddleware>();
      app.UseAuthorization();            
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        endpoints.MapHub<signalRHub>("/signalRHub");
      });

      Serilog.Log.Information(Configuration["ConnectionStrings:fmas12d"]);
    }
  }
}
