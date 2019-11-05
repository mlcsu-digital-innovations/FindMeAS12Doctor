using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fmas12d.Business.Exceptions;
using Fmas12d.Business.Models;
using Microsoft.EntityFrameworkCore;
using Entities = Fmas12d.Data.Entities;

namespace Fmas12d.Business.Services
{
  public class UserAvailabilityService : ServiceBaseNoAutoMapper<Entities.UserAvailability>,
    IUserAvailabilityService
  {
    private readonly ILocationDetailService _locationDetailService;
    public UserAvailabilityService(
      ApplicationContext context, 
      ILocationDetailService locationDetailService) 
      : base(context)
    {
      _locationDetailService = locationDetailService;
    }

    /// <summary>
    /// TODO Check for overlapping availabilities
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<IUserAvailability> Create(IUserAvailability model)
    {

      if (CheckLatitudeLongitude(model) &&
          !model.Latitude.HasValue &&
          !model.Longitude.HasValue)
      {
        await SetLatitudeLongitude(model);
      }

      await CheckForOverlappingAvailability(model);

      Entities.IUserAvailability entity = new Entities.UserAvailability();
      model.MapToEntity(entity);
      entity.IsActive = true;
      UpdateModified(entity);

      _context.Add(entity);
      await _context.SaveChangesAsync();

      model = await _context.UserAvailabilities
                      .Where(u => u.IsActive)
                      .Where(u => u.Id == entity.Id)
                      .Select(UserAvailability.ProjectFromEntity)
                      .SingleAsync();

      return model;
    }

    private async Task<bool> CheckForOverlappingAvailability(IUserAvailability model)
    {
      IEnumerable<int> existingAvailabilitiesIds = 
        await _context.UserAvailabilities
                      .Where(u => u.IsActive)
                      .Where(u => model.UserId == u.UserId)
                      .Where(u => model.Start <= u.End)
                      .Where(u => model.End >= u.Start)
                      .Select(u => u.Id)
                      .ToListAsync();

      if (existingAvailabilitiesIds.Count() > 0)
      {
          throw new ModelStateException(new string[] {"Start", "End"},
            $"There are existing availability records for the User with an id of {model.UserId} " +
            $"between {model.Start} and {model.End} " +
            $"UserAvailabilities Ids [{string.Join(",", existingAvailabilitiesIds)}]");
      }
      return true;
    }

    /// <summary>
    /// Check that both latitude and longitude are set if one is
    /// </summary>
    private bool CheckLatitudeLongitude(IUserAvailability model)
    {
      if (model.Latitude.HasValue)
      {
        if (!model.Longitude.HasValue)
        {
          throw new ModelStateException("Longitude",
            "The field longitude must have a value if latitude is provided.");
        }
      }
      else
      {
        if (model.Longitude.HasValue)
        {
          throw new ModelStateException("Latitude",
            "The field latitude must have a value if longitude is provided.");
        }
      }
      return true;
    }

    /// <summary>
    /// Gets the latitude and longitude from either the postcode or contact details
    /// </summary>
    private async Task<bool> SetLatitudeLongitude(IUserAvailability model)
    {
      if (!string.IsNullOrWhiteSpace(model.Postcode))
      {
        Postcode postcodeModel = 
          await _locationDetailService.GetPostcodeDetailsAsync(model.Postcode);

        model.Latitude = postcodeModel.Latitude;
        model.Longitude = postcodeModel.Longitude;
        
      }
      else if (model.ContactDetailId.HasValue)
      {
        // TODO Move this to a separate contact detail service
        Entities.ContactDetail contactDetail = await _context.ContactDetails
          .Where(c => c.IsActive)
          .Where(c => c.Id == model.ContactDetailId)
          .SingleOrDefaultAsync();

        if (contactDetail == null)
        {
          throw new ModelStateException("ContactDetailId",
            $"There is no active ContactDetail with an Id of {model.ContactDetailId}");
        }
        if (contactDetail.UserId != model.UserId)
        {
          throw new ModelStateException("ContactDetailId",
            $"A User with an id of {model.UserId} does not have an active " +
            $"ContactDetailId of {model.ContactDetailId}");
        }

        model.Latitude = contactDetail.Latitude;
        model.Longitude = contactDetail.Longitude;
      }

      return true;
    }
  }
}