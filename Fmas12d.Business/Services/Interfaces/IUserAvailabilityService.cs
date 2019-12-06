using Fmas12d.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Fmas12d.Business.Services
{
  public interface IUserAvailabilityService : IServiceBaseNoAutoMapper
  {
    Task<IUserAvailability> CreateAsync(IUserAvailability model);
    Task<IEnumerable<IUserAvailability>> GetAsync(
      int userId,
      DateTimeOffset from,
      bool asNoTracking,
      bool activeOnly);
    Task<IEnumerable<IUserAvailabilityDoctor>> GetAvailableDoctorsAsync(
      DateTimeOffset requiredDateTime,
      bool asNoTracking,
      bool activeOnly);
    Task<Dictionary<int, Location>> GetDoctorsPostcodeAtAsync(
      List<int> userIds, 
      DateTimeOffset dateTime, 
      bool asNoTracking,
      bool activeOnly);
    Task<IUserAvailability> UpdateAsync(IUserAvailability businessModel);
  }
}