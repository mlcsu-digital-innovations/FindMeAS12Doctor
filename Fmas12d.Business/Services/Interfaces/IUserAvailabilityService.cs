using Fmas12d.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Fmas12d.Business.Services
{
  public interface IUserAvailabilityService : IServiceBaseNoAutoMapper
  {
    Task<IUserAvailability> Create(IUserAvailability model);
    Task<IEnumerable<IUserAvailabilityDoctor>> GetAvailableDoctors(
      DateTimeOffset requiredDateTime,
      bool asNoTracking,
      bool activeOnly);
  }
}