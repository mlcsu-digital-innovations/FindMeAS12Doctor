using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Mep.Business.Models;
using Entities = Mep.Data.Entities;
using Mep.Business.Extensions;
using Mep.Business.Models.SearchModels;
using System.Linq.Expressions;
using System;
using System.Linq;
using Mep.Business.Exceptions;

namespace Mep.Business.Services
{
  public class PatientService
    : SearchServiceBase<Patient, Entities.Patient, PatientSearch>, IModelSearchService<Patient, PatientSearch>
  {
    private readonly IModelService<GpPractice> _gpPracticeService;

    public PatientService(
      ApplicationContext context,
      IMapper mapper,
      IModelService<GpPractice> gpPracticeService)
      : base("Patient", context, mapper)
    {
      _gpPracticeService = gpPracticeService;
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

    protected override async Task<bool> InternalCreateAsync(Patient model, Entities.Patient entity)
    {
      // TODO: Residential Postcode => CCG Id
      await PopulateCcgIdFromGpPracticeIdIfPresent(model, entity);
      await CheckForDuplicateNhsNumberAndAlternativeIdentifier(model);
      return true;
    }

    protected override async Task<bool> InternalUpdateAsync(Patient model, Entities.Patient entity)
    {
      await CheckForDuplicateNhsNumberAndAlternativeIdentifier(model);

      entity.AlternativeIdentifier = model.AlternativeIdentifier;
      entity.CcgId = model.CcgId;
      await PopulateCcgIdFromGpPracticeIdIfPresent(model, entity);
      entity.GpPracticeId = model.GpPracticeId;
      entity.NhsNumber = model.NhsNumber;
      entity.ResidentialPostcode = model.ResidentialPostcode;
      // TODO: Residential Postcode => CCG Id
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
            $"patient with an AlternativeIdentifier of {model.AlternativeIdentifier} already exists");
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
        GpPractice gpPractice = await _gpPracticeService.GetByIdAsync((int)model.GpPracticeId, true);
        if (gpPractice == null)
        {
          throw new ModelStateException("GpPracticeId",
            $"An active GP Practice with an Id of {model.GpPracticeId} does not exist.");
        }
        entity.CcgId = gpPractice.CcgId;
        return true;
      }
      else
      {
        return false;
      }      
    }
  }
}