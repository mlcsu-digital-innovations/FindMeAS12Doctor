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

namespace Mep.Business.Services
{
  public class DoctorStatusService
    : SearchServiceBase<DoctorStatus, Entities.DoctorStatus, DoctorStatusSearchModel>, IModelSearchService<DoctorStatus, DoctorStatusSearchModel>
  {
    public DoctorStatusService(ApplicationContext context, IMapper mapper)
      : base("DoctorStatus", context, mapper)
    {
    }

    public override async Task<IEnumerable<DoctorStatus>> SearchAsync(DoctorStatusSearchModel model)
    {
      // build up the where statement
      var param = Expression.Parameter(typeof(Entities.DoctorStatus), "ds");

      Expression defaultExpression = Expression.GreaterThan(Expression.Property(param, "Id"), Expression.Constant(0));

      Expression searchExpression = GetSearchExpression(model, param);

      if (searchExpression != null)
      {
        defaultExpression = Expression.And(defaultExpression, searchExpression);
      }

      var whereExpression = Expression.Lambda<Func<Entities.DoctorStatus, bool>>(
        defaultExpression, param
      );

      IEnumerable<Entities.DoctorStatus> entities =
        await _context.DoctorStatuses
        .Where(whereExpression)
        .WhereIsActiveOrActiveOnly(true)
        .ToListAsync();

      IEnumerable<Models.DoctorStatus> models =
        _mapper.Map<IEnumerable<Models.DoctorStatus>>(entities);

      return models;
    }

    public async Task<IEnumerable<Models.DoctorStatus>> GetAllAsync(
      bool activeOnly)
    {

      IEnumerable<Entities.DoctorStatus> entities =
        await _context.DoctorStatuses
                .WhereIsActiveOrActiveOnly(activeOnly)
                .ToListAsync();

      IEnumerable<Models.DoctorStatus> models =
        _mapper.Map<IEnumerable<Models.DoctorStatus>>(entities);

      return models;
    }

    protected override async Task<Entities.DoctorStatus> GetEntityByIdAsync(
      int id,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.DoctorStatus entity = await
        _context.DoctorStatuses
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(u => u.Id == id);

      return entity;
    }

    protected override Task<Entities.DoctorStatus> GetEntityLinkedObjectsAsync(DoctorStatus model, Entities.DoctorStatus entity)
    {
      // if (model.CcgId.HasValue)
      // {
      //   entity.Ccg = await GetLinkedObjectAsync<Entities.Ccg>(_context.Ccgs, model.CcgId.Value);
      // }

      // if (model.GpPracticeId.HasValue)
      // {
      //   entity.GpPractice = await GetLinkedObjectAsync<Entities.GpPractice>(_context.GpPractices, model.GpPracticeId.Value);
      // }

      // return entity;
      return Task.FromResult(entity);
    }

    protected override Task<bool> InternalCreateAsync(DoctorStatus model, Entities.DoctorStatus entity)
    {
      return Task.FromResult<bool>(true);
    }

    protected override Task<bool> InternalUpdateAsync(DoctorStatus model, Entities.DoctorStatus entity)
    {
      return Task.FromResult<bool>(true);
    }
  }
}