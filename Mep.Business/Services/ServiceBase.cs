using System;
using System.Threading.Tasks;
using AutoMapper;
using Mep.Business.Models;
using Mep.Data.Entities;

namespace Mep.Business.Services
{
    public abstract class ServiceBase<TBusinessModel, TEntity> where TBusinessModel: BaseModel
                                                               where TEntity: BaseEntity
    {
        protected readonly ApplicationContext _context;
        protected readonly IMapper _mapper;

        protected abstract Task<TEntity> GetEntityByIdAsync(
            int userId,
            bool asNoTracking,
            bool activeOnly);

        public abstract Task<TBusinessModel> GetByIdAsync(
            int userId,
            bool activeOnly);

        public ServiceBase(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> ActivateAsync(int id)
        {
            return await SetActiveStatus(id, true);
        }

        public async Task<int> DeactivateAsync(int id)
        {
            return await SetActiveStatus(id, false);
        } 

        protected void UpdateModified(TEntity entity)
        {
            //TODO: Get the current users sub claim
            entity.ModifiedByUserId = 1;
            entity.ModifiedAt = DateTimeOffset.Now;  
        }  

        public async Task<TBusinessModel> UpdateEntityAsync(TBusinessModel model)
        {
            TEntity entity = 
                await GetEntityByIdAsync(model.Id, false, false);

            if (entity == null)
            {
                //TODO: Create a specific exception
                throw new Exception($"{typeof(TEntity).Name} with an id of {model.Id} does not exist.");
            }
            else
            {
                _mapper.Map<TBusinessModel, TEntity>(model, entity);
                UpdateModified(entity);
                await _context.SaveChangesAsync();

                model = await GetByIdAsync(model.Id, model.IsActive);
                return model;
            }
        }        

        private async Task<int> SetActiveStatus(int id, bool isActivating)
        {
            TEntity entity = await _context.Set<TEntity>()
                                           .FindAsync(id);

            if (entity == null)
            {
                //TODO: Create a specific exception
                throw new Exception($"{typeof(TEntity).Name} with an id of {id} does not exist.");
            }
            else if (entity.IsActive == isActivating)
            {
                //TODO: Create a specific exception
                throw new Exception(
                    $"{typeof(TEntity).Name} with an id of {id} is already {(isActivating ? "active" : "inactive" )}.");
            }
            else
            {
                entity.IsActive = isActivating;
                UpdateModified(entity);
                return await _context.SaveChangesAsync();
            }        
        }           
    }
}