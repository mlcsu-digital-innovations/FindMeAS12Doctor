using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Fmas12d.Business.Models;
using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Extensions;
using Fmas12d.Business.Models.SearchModels;
using System.Linq.Expressions;
using System;
using System.Linq;
using Fmas12d.Business.Exceptions;

namespace Fmas12d.Business.Services
{

  public class PatientService
    : SearchServiceBase<Patient, Entities.Patient, PatientSearch>,
      IModelSearchService<Patient, PatientSearch>,
      IPatientService
  {
    private readonly IGpPracticeService _gpPracticeService;

    public PatientService(
      ApplicationContext context,
      IMapper mapper,
      IGpPracticeService gpPracticeService)
      : base("Patient", context, mapper)
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

    public override async Task<Patient> CreateAsync(Patient model)
    {
      Entities.Patient entity = model.MapToEntity();

      entity.Id = 0;
      entity.IsActive = true;

      UpdateModified(entity);

      await PopulateCcgIdFromGpPracticeIdIfPresent(model, entity);
      await CheckForDuplicateNhsNumberAndAlternativeIdentifier(model);

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

    public async Task<IEnumerable<Models.Patient>> GetAllAsync(
      bool activeOnly)
    {

      IEnumerable<Entities.Patient> entities =
        await _context.Patients
                .Include(p => p.Ccg)
                .Include(p => p.GpPractice)
                .WhereIsActiveOrActiveOnly(activeOnly)
                .ToListAsync();

      IEnumerable<Models.Patient> models =
        _mapper.Map<IEnumerable<Models.Patient>>(entities);

      return models;
    }

    public async Task<Models.Patient> GetByNhsNumber(
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

    public async Task<Models.Patient> GetByAlternativeIdentifier(
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

    protected override async Task<Entities.Patient> GetEntityByIdAsync(
      int entityId,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.Patient entity = await
        _context.Patients
                .Include(p => p.Ccg)
                .Include(p => p.GpPractice)
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(patient => patient.Id == entityId);

      return entity;
    }

    protected override async Task<Entities.Patient> GetEntityWithNoIncludesByIdAsync(
      int entityId,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.Patient entity = await
        _context.Patients
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(patient => patient.Id == entityId);

      return entity;
    }

    /// <summary>
    /// TODO: Residential Postcode => CCG Id
    /// </summary>
    protected override async Task<bool> InternalCreateAsync(Patient model, Entities.Patient entity)
    {
      await PopulateCcgIdFromGpPracticeIdIfPresent(model, entity);
      await CheckForDuplicateNhsNumberAndAlternativeIdentifier(model);
      return true;
    }

    /// <summary>
    /// TODO: Residential Postcode => CCG Id
    /// </summary>
    protected override async Task<bool> InternalUpdateAsync(Patient model, Entities.Patient entity)
    {
      await CheckForDuplicateNhsNumberAndAlternativeIdentifier(model);

      entity.AlternativeIdentifier = model.AlternativeIdentifier;
      entity.CcgId = model.CcgId;
      await PopulateCcgIdFromGpPracticeIdIfPresent(model, entity);
      entity.GpPracticeId = model.GpPracticeId;
      entity.NhsNumber = model.NhsNumber;
      entity.ResidentialPostcode = model.ResidentialPostcode;

      return true;
    }

    private async Task<bool> CheckForDuplicateNhsNumberAndAlternativeIdentifier(Patient model)
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
      else
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

    private async Task<bool> PopulateCcgIdFromGpPracticeIdIfPresent(
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

    public override async Task<IEnumerable<Patient>> SearchAsync(PatientSearch searchModel)
    {
      // build up the where statement
      var param = Expression.Parameter(typeof(Entities.Patient), "p");

      Expression searchExpression = GetSearchExpression(searchModel, param);

      if (searchExpression == null)
      {
        throw new MissingSearchParameterException();
      }
      else
      {
        var whereExpression = Expression.Lambda<Func<Entities.Patient, bool>>(
          searchExpression, param
        );

        IEnumerable<Entities.Patient> entities =
          await _context.Patients
          .Include(p => p.Ccg)
          .Include(p => p.GpPractice)
          .Include(p => p.Referrals)
          .Where(whereExpression)
          .WhereIsActiveOrActiveOnly(true)
          .ToListAsync();

        IEnumerable<Models.Patient> models =
          _mapper.Map<IEnumerable<Models.Patient>>(entities);

        return models;
      }
    }

    private class PatientReferral
    {
      public Patient Patient { get; set; }
      public Referral Referral { get; set; }

      public Patient Merge()
      {
        if (Patient != null && Referral != null)
        {
          Patient.Referrals = new List<Referral>() { Referral };
        }
        return Patient;
      }
    }
  }
}
