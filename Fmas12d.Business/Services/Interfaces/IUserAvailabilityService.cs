using Fmas12d.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Fmas12d.Business.Services
{
  public interface IUserAvailabilityService : IServiceBase
  {
    Task<IUserAvailability> CreateAsync(IUserAvailability model);
    Task<IUserOnCall> CreateOnCallAsync(IUserOnCall model);
    Task<IUserAvailability> GetAsync(
      int id,
      bool asNoTracking,
      bool activeOnly
    );

    Task<IEnumerable<IUserAvailability>> GetListAsync(
      int userId,
      DateTimeOffset from,
      bool asNoTracking,
      bool activeOnly
    );
    Task<IUserAvailability> GetAtAsync(
      int userId,
      DateTimeOffset at,
      bool asNoTracking,
      bool activeOnly
    );
    Task<UserAvailabilityOverlapping> CheckOverlapWithExisting(
      int userId,
      int userAvailabilityId,
      DateTimeOffset start,
      DateTimeOffset end
    );
    Task<IEnumerable<IUserAvailabilityDoctor>> GetAvailableDoctorsAsync(
      DateTimeOffset requiredDateTime,
      bool asNoTracking,
      bool activeOnly
    );
    Task<Dictionary<int, Location>> GetDoctorsPostcodeAtAsync(
      List<int> userIds, 
      DateTimeOffset dateTime, 
      bool asNoTracking,
      bool activeOnly
    );
    Task<IEnumerable<IUserOnCall>> GetOnCallAsync(
      DateTimeOffset from,
      DateTimeOffset to,
      bool asNoTracking,
      bool activeOnly
    );
    Task<IEnumerable<IUserOnCall>> GetOnCallByCurrentUserAsync(       
      DateTimeOffset to, 
      bool asNoTracking, 
      bool activeOnly
    );
    Task<IUserAvailability> UpdateAsync(IUserAvailability model);
    Task<IUserOnCall> UpdateOnCallAsync(IUserOnCall model);
    Task<IUserOnCall> UpdateOnCallConfirmationAsync(IUserOnCall businessModel);
  }
}