using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Mep.Business.Models;
using Entities = Mep.Data.Entities;
using Mep.Business.Extensions;

namespace Mep.Business.Services
{
  public class ExaminationService
    : ServiceBase<Examination, Entities.Examination>, IModelService<Examination>
  {
    public ExaminationService(ApplicationContext context, IMapper mapper)
      : base("Examination", context, mapper)
    {
    }

    public async Task<IEnumerable<Models.Examination>> GetAllAsync(
      bool activeOnly)
    {

      IEnumerable<Entities.Examination> entities =
        await _context.Examinations
                .Include(r => r.CreatedByUser)
                .Include(e => e.Details)
                  .ThenInclude(d => d.ExaminationDetailType)
                .WhereIsActiveOrActiveOnly(activeOnly)
                .ToListAsync();

      IEnumerable<Models.Examination> models =
        _mapper.Map<IEnumerable<Models.Examination>>(entities);

      return models;
    }

    protected override async Task<Entities.Examination> GetEntityLinkedObjectsAsync(Examination model, Entities.Examination entity) {
        entity.CreatedByUser =await GetLinkedObjectAsync<Entities.User>(_context.Users, model.CreatedByUserId);
        return entity;
    }

    protected override async Task<Entities.Examination> GetEntityByIdAsync(
      int entityId,
      bool asNoTracking,
      bool activeOnly)
    {
      Entities.Examination entity = await
        _context.Examinations
                .Include(e => e.CreatedByUser)
                .Include(e => e.Details)
                  .ThenInclude(d => d.ExaminationDetailType)
                .WhereIsActiveOrActiveOnly(activeOnly)
                .AsNoTracking(asNoTracking)
                .SingleOrDefaultAsync(u => u.Id == entityId);

      return entity;
    }

    protected override async Task<bool> InternalCreateAsync(Examination model, Entities.Examination entity)
    {
      // the CCG id is set from the referral patient's ccg, it's duplicated on the examination so that
      // the patient's CCG is fixed at the time of the examination so if they change their CCG
      // after the examination has taken place to won't change the CCG of the claim 
      ReferralService service = new ReferralService(_context, _mapper);
      Referral referral = await service.GetByIdAsync(model.ReferralId, true);
      entity.CcgId = (int)referral.Patient.CcgId;

      // the modified user id has already been set from the service base
      entity.CreatedByUserId = (int)entity.ModifiedByUserId;

      return true;
    }

    protected override Task<bool> InternalUpdateAsync(Examination model, Entities.Examination entity)
    {
      return Task.FromResult<bool>(true);
    }
  }
}
