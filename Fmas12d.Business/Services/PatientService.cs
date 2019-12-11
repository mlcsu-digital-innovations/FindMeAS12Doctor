using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Exceptions;
using Fmas12d.Business.Extensions;
using Fmas12d.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Fmas12d.Business.Services
{

  public class PatientService
    : ServiceBase<Entities.Patient>,
      IPatientService
  {
    private readonly IGpPracticeService _gpPracticeService;

    public PatientService(
      ApplicationContext context,
      IGpPracticeService gpPracticeService,
      IUserClaimsService userClaimsService)
      : base(context, userClaimsService)
    {
      _gpPracticeService = gpPracticeService;
    }

    public async Task<bool> CheckExists(
      int id, 
      string modelPropertyName, 
      bool asNoTracking = true,
      bool activeOnly = true)
    {
      bool exists = await _context.Patients
                              .Where(u => u.Id == id)
                              .WhereIsActiveOrActiveOnly(activeOnly)
                              .AsNoTracking(asNoTracking)
                              .AnyAsync();
      if (!exists)
      {
        throw new ModelStateException(
          modelPropertyName, 
          $"A{(activeOnly ? "n active" : "")} Patient with an Id of {id} does not exist."
        );
      }

      return exists;
    }

    public async Task<Patient> CreateAsync(Patient model)
    {
      Entities.Patient entity = model.MapToEntity();

      entity.Id = 0;
      entity.IsActive = true;

      UpdateModified(entity);

      await PopulateCcgIdFromGpPracticeIdIfPresentAsync(model, entity);
      await CheckForDuplicateNhsNumberAndAlternativeIdentifierAsync(model);

      _context.Add(entity);

      await _context.SaveChangesAsync();

      model = _context.Patients
                      .Where(e => e.Id == entity.Id)
                      .WhereIsActiveOrActiveOnly(true)
                      .AsNoTracking(true)
                      .Select(Patient.ProjectFromEntity)
                      .Single();
      return model;
    }

    public async Task<Models.Patient> GetByNhsNumberAsync(
      long nhsNumber,
      bool asNoTracking = true,
      bool activeOnly = true,
      bool onlyCurrentReferral = true)
    {
      IQueryable<Entities.Patient> query = _context.Patients
        .Include(p => p.Ccg)
        .Include(p => p.GpPractice)
        .Include(p => p.Referrals)
        .WhereIsActiveOrActiveOnly(activeOnly)
        .Where(p => p.NhsNumber == nhsNumber)
        .AsNoTracking(asNoTracking);

      // Awaiting a fix in EF Core 3 to implement this.
      // https://github.com/aspnet/EntityFrameworkCore/pull/18625
      Models.Patient model;
      // if (onlyCurrentReferral)
      // {
      //   var PatientAndCurrentReferral =
      //   await query.Select(p => new 
      //   {
      //     Patient = p // new Patient(p),
      //     //Referral = p.Referrals
      //       // .Where(r => r.IsActive)  
      //       // .Where(r => r.ReferralStatusId != Models.ReferralStatus.CLOSED)
      //       // .OrderByDescending(r => r.CreatedAt)
      //       // .FirstOrDefault()
      //   }).SingleOrDefaultAsync();

      //   Serilog.Log.Information("{@PatientAndCurrentReferral}", PatientAndCurrentReferral);
      //   model = null;
      //   //model = PatientAndCurrentReferral.Merge();
      // }
      // else
      // {
      model = await query
        .Select(Patient.ProjectFromEntity)
        .SingleOrDefaultAsync();
      // }

      return model;
    }

    public async Task<Models.Patient> GetByAlternativeIdentifierAsync(
      string alternativeIdentifier,
      bool asNoTracking = true,
      bool activeOnly = true,
      bool onlyCurrentReferral = true)
    {
      if (string.IsNullOrWhiteSpace(alternativeIdentifier))
      {
        throw new ModelStateException(
          "AlternativeIdentifier",
          "The field AlternativeIdentifier must have a value.");
      }

      Models.Patient model = await _context.Patients
        .Include(p => p.Ccg)
        .Include(p => p.GpPractice)
        .Include(p => p.Referrals)
        .WhereIsActiveOrActiveOnly(activeOnly)
        .Where(p => p.AlternativeIdentifier == alternativeIdentifier)
        .AsNoTracking(asNoTracking)
        .Select(Patient.ProjectFromEntity)
        .SingleOrDefaultAsync();

      return model;
    }

    private async Task<bool> CheckForDuplicateNhsNumberAndAlternativeIdentifierAsync(Patient model)
    {
      if (model.NhsNumber.HasValue)
      {
        Entities.Patient patient = await _context
          .Patients
          .Where(p => p.Id != model.Id)
          .FirstOrDefaultAsync(p => p.NhsNumber == model.NhsNumber);

        if (patient != null)
        {
          throw new ModelStateException("NhsNumber",
            $"An {(patient.IsActive ? "active" : "inactive")} " +
            $"patient with an NhsNumber of {model.NhsNumber} already exists.");
        }
      }
      
      if (!string.IsNullOrWhiteSpace(model.AlternativeIdentifier))
      {
        Entities.Patient patient = await _context
          .Patients
          .Where(p => p.Id != model.Id)
          .FirstOrDefaultAsync(p => p.AlternativeIdentifier == model.AlternativeIdentifier);

        if (patient != null)
        {
          throw new ModelStateException("AlternativeIdentifier",
            $"An {(patient.IsActive ? "active" : "inactive")} " +
            "patient with an AlternativeIdentifier of " +
            $"{model.AlternativeIdentifier} already exists");
        }
      }
      return true;
    }

    private async Task<bool> PopulateCcgIdFromGpPracticeIdIfPresentAsync(
      Patient model,
      Entities.Patient entity)
    {

      if (model.CcgId == null &&
          model.GpPracticeId != null)
      {
        entity.CcgId = await _gpPracticeService.GetCcgIdById((int)model.GpPracticeId);
        return true;
      }
      else
      {
        return false;
      }
    }

    public async Task<Patient> UpdateAsync(Patient model)
    {
      await CheckForDuplicateNhsNumberAndAlternativeIdentifierAsync(model);

      Entities.Patient entity = await _context
        .Patients
        .Where(p => p.Id == model.Id)
        .WhereIsActiveOrActiveOnly(true)
        .SingleOrDefaultAsync();

      if (entity == null)
      {
        throw new ModelStateException("id",
        $"Unable to find an active patient with an id of {model.Id}");
      }

      entity.AlternativeIdentifier = model.AlternativeIdentifier;
      entity.CcgId = model.CcgId;
      entity.GpPracticeId = model.GpPracticeId;
      entity.NhsNumber = model.NhsNumber;
      entity.ResidentialPostcode = model.ResidentialPostcode;      
      UpdateModified(entity);

      await PopulateCcgIdFromGpPracticeIdIfPresentAsync(model, entity);      
      await _context.SaveChangesAsync();

      model = _context.Patients
                      .Where(e => e.Id == entity.Id)
                      .WhereIsActiveOrActiveOnly(true)
                      .AsNoTracking(true)
                      .Select(Patient.ProjectFromEntity)
                      .Single();
      return model;
    }
  }
}
