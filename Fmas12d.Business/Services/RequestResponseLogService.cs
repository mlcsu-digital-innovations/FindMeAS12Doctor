using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Fmas12d.Business.Services
{
  public class RequestResponseLogService : IRequestResponseLogService
  {
    private readonly ApplicationContext _context;
    public RequestResponseLogService(
      ApplicationContext context)
    {
      _context = context;
    }

    public Task<int> ActivateAsync(int id)
    {
      throw new NotImplementedException();
    }

    public async Task<bool> CreateAsync(IRequestResponseLog model)
    {
      Entities.RequestResponseLog entity = new Entities.RequestResponseLog();
      model.MapToEntity(entity);
      _context.Add(entity);

      await _context.SaveChangesAsync();

      return true;
    }

    public Task<int> DeactivateAsync(int id)
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<IRequestResponseLog>> Get(
      DateTimeOffset start,
      DateTimeOffset end
    )
    {
      return await _context.RequestResponseLog
                           .Where(rrl => rrl.RequestAt >= start)
                           .Where(rrl => rrl.RequestAt <= end)
                           .AsNoTracking()
                           .Select(RequestResponseLog.ProjectFromEntity)
                           .ToListAsync();

    }

  }
}