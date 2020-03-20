using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Exceptions;
using Fmas12d.Business.Extensions;
using Fmas12d.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public class UserService :
    ServiceBase<Entities.User>,
    IUserService
  {
    public UserService(
      ApplicationContext context,
      IUserClaimsService userClaimsService)
      : base(context, userClaimsService)
    {
    }

    public async Task<User> CreateAsync(User model)
    {
      Entities.User entity = model.MapToEntity();

      entity.Id = 0;
      entity.IsActive = true;

      UpdateModified(entity);

      _context.Add(entity);

      await _context.SaveChangesAsync();

      model = _context.Users
                      .Where(e => e.Id == entity.Id)
                      .WhereIsActiveOrActiveOnly(true)
                      .AsNoTracking(true)
                      .Select(User.ProjectFromEntity)
                      .Single();
      return model;
    }

    public async Task<User> CheckIsAmhpAsync(
      int id,
      string modelPropertyName,
      bool asNoTracking = true,
      bool activeOnly = true)
    {
      User user = await CheckUserIsAsync(id, modelPropertyName, asNoTracking, activeOnly);
      if (!user.IsAmhp)
      {
        throw new ModelStateException(
          modelPropertyName,
          $"The User with an Id of {id} must be an AMHP but is a {user.ProfileType.Name}.");
      }
      return user;
    }

    public async Task<User> CheckIsADoctorAsync(
      int id,
      string modelPropertyName,
      bool asNoTracking = true,
      bool activeOnly = true)
    {
      User user = await CheckUserIsAsync(id, modelPropertyName, asNoTracking, activeOnly);
      if (!user.IsDoctor)
      {
        throw new ModelStateException(
          modelPropertyName,
          $"The User with an Id of {id} must be a Doctor but is a {user.ProfileType.Name}.");
      }
      return user;
    }

    public async Task<IEnumerable<User>> GetAllByAmhpNameAsync(
      string amhpName,
      bool asNoTracking = true,
      bool activeOnly = true)
    {
      return await GetAllByNameAndProfileTypeIdAsync(
        amhpName, ProfileType.AMHP, asNoTracking, activeOnly);
    }

    public async Task<IEnumerable<User>> GetAllByDoctorNameAsync(
      string doctorName,
      bool asNoTracking = true,
      bool activeOnly = true,
      bool includeUnregisteredDoctors = false)
    {
      List<int> profileTypes = ProfileType.DoctorProfileTypes(includeUnregisteredDoctors);

      IEnumerable<User> models = await _context.Users
       .WhereIsActiveOrActiveOnly(activeOnly)
       .Where(u => u.DisplayName.Contains(doctorName))
       .Where(u => profileTypes.Contains(u.ProfileTypeId))
       .AsNoTracking(asNoTracking)
       .Select(User.ProjectFromEntity)
       .ToListAsync();

      return models;
    }

    public async Task<IEnumerable<User>> GetAllByGmcNumberAsync(
      int gmcNumber,
      bool asNoTracking = true,
      bool activeOnly = true,
      bool includeUnregisteredDoctors = false)
    {
      List<int> profileTypes = ProfileType.DoctorProfileTypes(includeUnregisteredDoctors);

      IEnumerable<User> models = await _context.Users
       .WhereIsActiveOrActiveOnly(activeOnly)
       .Where(u => u.GmcNumber.ToString().Contains(gmcNumber.ToString()))
       .Where(u => profileTypes.Contains(u.ProfileTypeId))
       .AsNoTracking(asNoTracking)
       .Select(User.ProjectFromEntity)
       .ToListAsync();

      return models;
    }

    public async Task<User> GetAsync(
      int id,
      bool asNoTracking,
      bool activeOnly)
    {
      User model = await _context
        .Users
        .Include(u => u.BankDetails)
          .ThenInclude(bd => bd.Ccg)
        .Include(u => u.ContactDetails)
          .ThenInclude(cd => cd.ContactDetailType)
        .Include(u => u.GenderType)
        .Include(u => u.Organisation)
        .Include(u => u.ProfileType)
        .Include(u => u.UserSpecialities)
          .ThenInclude(us => us.Speciality)
        .Where(u => u.Id == id)
        .WhereIsActiveOrActiveOnly(activeOnly)
        .AsNoTracking(asNoTracking)
        .Select(User.ProjectFromEntity)
        .SingleOrDefaultAsync();

      if (model == null)
      {
        throw new ModelStateException(
          "id",
          $"Unable to find an {(activeOnly ? "" : "in")}active User Id of {id}."
        );
      }

      return model;
    }

    public async Task<User> GetByIdentityServerIdentifierAsync(
      string identityServerIdentifier,
      bool asNoTracking = true,
      bool activeOnly = true
    )
    {
      User model = await _context.Users
        .Include(u => u.ContactDetails)
        .Include(u => u.GenderType)
        .Include(u => u.ProfileType)
        .Include(u => u.UserSpecialities)
          .ThenInclude(us => us.Speciality)
       .WhereIsActiveOrActiveOnly(activeOnly)
       .Where(u => u.IdentityServerIdentifier == identityServerIdentifier)
       .AsNoTracking(asNoTracking)
       .Select(User.ProjectFromEntity)
       .SingleOrDefaultAsync();

      return model;
    }

    public async Task<int> GetByProfileTypeIdAsync(
      int id,
      bool asNoTracking,
      bool activeOnly
    )
    {
      int profileTypeId = await _context
        .Users
        .Where(u => u.Id == id)
        .AsNoTracking(asNoTracking)
        .WhereIsActiveOrActiveOnly(activeOnly)
        .Select(u => u.ProfileTypeId)
        .SingleOrDefaultAsync();

      return profileTypeId;
    }

    public async Task<bool> RefreshFcmToken(
      int id,
      string token
    )
    {
      Entities.User entity = await _context
      .Users
      .Where(u => u.Id == id)
      .WhereIsActiveOrActiveOnly(true)
      .SingleOrDefaultAsync();

      if (entity == null)
      {
        throw new ModelStateException(
          "id",
          $"Unable to find an active User Id of {id}."
        );
      }

      if (entity.FcmToken != token) 
      {
        entity.FcmToken = token;
        UpdateModified(entity);

        await _context.SaveChangesAsync();
      }

      return true;
    }

    public async Task<User> UpdateVsrAsync(
      VsrUpdate vsrDetails 
    )
    {
      Entities.BankDetail entity = await _context
      .BankDetails
      .Where(bd => bd.UserId == vsrDetails.UserId)
      .Where(bd => bd.CcgId == vsrDetails.CcgId)
      .SingleOrDefaultAsync();

      if (entity == null) {
        Entities.BankDetail bankDetail = new Entities.BankDetail(){
          UserId = vsrDetails.UserId,
          CcgId = vsrDetails.CcgId,
          VsrNumber = vsrDetails.VsrNumber,
          ModifiedByUserId = _userClaimsService.GetUserId(),
          ModifiedAt = DateTimeOffset.Now,
          IsActive = true
        };
        _context.Add(bankDetail);
      } else {
        entity.VsrNumber = vsrDetails.VsrNumber;
        entity.ModifiedByUserId = _userClaimsService.GetUserId();
        entity.ModifiedAt = DateTimeOffset.Now;
      }

      await _context.SaveChangesAsync();

      return await GetAsync(vsrDetails.UserId, true, true);
    }

    private async Task<User> CheckUserIsAsync(
      int id,
      string modelPropertyName,
      bool asNoTracking = true,
      bool activeOnly = true)
    {
      User user = await _context.Users
                                .Include(u => u.ProfileType)
                                .Where(u => u.Id == id)
                                .WhereIsActiveOrActiveOnly(activeOnly)
                                .AsNoTracking(asNoTracking)
                                .Select(User.ProjectFromEntity)
                                .SingleOrDefaultAsync();
      if (user == null)
      {
        throw new ModelStateException(
          modelPropertyName,
          $"A{(activeOnly ? "n active" : "")} User with an Id of {id} does not exist."
        );
      }

      return user;
    }


    private async Task<IEnumerable<User>> GetAllByNameAndProfileTypeIdAsync(
      string name,
      int profileTypeId,
      bool asNoTracking = true,
      bool activeOnly = true)
    {
      IEnumerable<User> models = await _context.Users
       .WhereIsActiveOrActiveOnly(activeOnly)
       .Where(u => u.DisplayName.Contains(name))
       .Where(u => u.ProfileTypeId == profileTypeId)
       .AsNoTracking(asNoTracking)
       .Select(User.ProjectFromEntity)
       .ToListAsync();

      return models;
    }

    public async Task<User> UpdateAsync(IUserProfileUpdate model) {
      Entities.User entity = _context
        .Users
        .Include(u => u.BankDetails)
        .Include(u => u.ContactDetails)
        .Include(u => u.UserSpecialities)
        .WhereIsActiveOrActiveOnly(true)
        .AsNoTracking(false)
        .SingleOrDefault(u => u.Id == model.Id);

      if (entity == null)
      {
        throw new ModelStateException("id",
          $"Unable to find a User with an Id of {model.Id}");
      }

      model.MapToEntity(entity);
      UpdateModifiedAll(entity);      

      await _context.SaveChangesAsync();

      User userModel = await _context.Users
                      .Include(u => u.BankDetails)
                      .ThenInclude(bd => bd.Ccg)
                      .Include(u => u.ContactDetails)
                        .ThenInclude(cd => cd.ContactDetailType)
                      .Include(u => u.GenderType)
                      .Include(u => u.Organisation)
                      .Include(u => u.ProfileType)
                      .Include(u => u.UserSpecialities)
                        .ThenInclude(us => us.Speciality)
                      .Where(u => u.IsActive)
                      .Where(u => u.Id == entity.Id)
                      .Select(User.ProjectFromEntity)
                      .SingleAsync();

      return userModel;
    }

    private void UpdateModifiedAll(Data.Entities.User entity) {
      UpdateModified(entity);
      foreach (Data.Entities.BankDetail bankDetailEntity in entity.BankDetails) {
        bankDetailEntity.ModifiedByUserId = entity.ModifiedByUserId;
        bankDetailEntity.ModifiedAt = entity.ModifiedAt;
      }
      foreach (Data.Entities.ContactDetail contactDetailEntity in entity.ContactDetails) {
        contactDetailEntity.ModifiedByUserId = entity.ModifiedByUserId;
        contactDetailEntity.ModifiedAt = entity.ModifiedAt;
      }
      foreach (Data.Entities.UserSpeciality userSpeciality in entity.UserSpecialities) {
        userSpeciality.ModifiedByUserId = entity.ModifiedByUserId;
        userSpeciality.ModifiedAt = entity.ModifiedAt;
      }
    } 
  }
}