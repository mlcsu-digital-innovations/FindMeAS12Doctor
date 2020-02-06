using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fmas12d.Business.Models;

namespace Fmas12d.Business.Services
{
  public interface IRequestResponseLogService : IServiceBase
  {
    Task<bool> CreateAsync(IRequestResponseLog model);
    Task<IEnumerable<IRequestResponseLog>> Get(DateTimeOffset start, DateTimeOffset end);
  }
}