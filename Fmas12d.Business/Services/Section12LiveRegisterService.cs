using CsvHelper;
using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Models;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using Fmas12d.Business.Extensions;

namespace Fmas12d.Business.Services
{
  public class Section12LiveRegisterService
    : ServiceBase<Entities.Section12LiveRegister>,
    ISection12LiveRegisterService
  {

    private readonly Section12LiveRegisterBatchUpdateResult _section12LiveRegisterEtl;

    public Section12LiveRegisterService(
      ApplicationContext context,
      IUserClaimsService userClaimsService)
      : base(context, userClaimsService)
    {
      _section12LiveRegisterEtl = new Section12LiveRegisterBatchUpdateResult()
      {
        LoadedDate = DateTimeOffset.Now
      };
    }

    public async Task<Section12LiveRegisterBatchUpdateResult> BatchUpdate(
      List<Section12LiveRegister> section12LiveRegisters)
    {
      await UpdateSection12LiveRegisters(section12LiveRegisters);
      await UpdateUserSection12ApprovalStatus();

      return _section12LiveRegisterEtl;      
    }    

    public async Task<Section12LiveRegister> GetByGmcNumber(
      int gmcNumber,
      bool activeOnly,
      bool asNoTracking
    )
    {
      Section12LiveRegister section12LiveRegister = await _context
        .Section12LiveRegisters
        .WhereIsActiveOrActiveOnly(activeOnly)
        .AsNoTracking(asNoTracking)
        .Where(s => s.GmcNumber == gmcNumber)
        .Select(Section12LiveRegister.ProjectFromEntity)
        .SingleOrDefaultAsync();

      return section12LiveRegister;
    }

    private async Task<bool> UpdateUserSection12ApprovalStatus()
    {

      // not sure how to do this with linq expressions
      var query = from user in _context.Set<Entities.User>()
                  join s12LiveRegister in _context.Set<Entities.Section12LiveRegister>()
                    on user.GmcNumber equals s12LiveRegister.GmcNumber
                  select new Entities.User()
                  {
                    Id = user.Id,
                    Section12ExpiryDate = s12LiveRegister.ExpiryDate
                  };
      List<Entities.User> updatedUsers = await query.ToListAsync();

      await _context.Users
                    .Where(u => u.GmcNumber != null)
                    .ForEachAsync(u =>
                    {
                      Entities.User updatedUser =
                        updatedUsers.SingleOrDefault(uu => uu.Id == u.Id);

                      if (updatedUser == null)
                      {
                        u.Section12ExpiryDate = null;
                        u.Section12ApprovalStatusId = Section12ApprovalStatus.NOT_APPROVED;
                      }
                      else
                      {
                        u.Section12ExpiryDate = updatedUser.Section12ExpiryDate;
                        u.Section12ApprovalStatusId = u.Section12ExpiryDate.HasValue ?
                                                      Section12ApprovalStatus.APPROVED :
                                                      Section12ApprovalStatus.NOT_APPROVED;
                      }
                      UpdateModified(u);
                    });

      await _context.SaveChangesAsync();

      return true;
    }

    private async Task<bool> UpdateSection12LiveRegisters(
      List<Section12LiveRegister> section12LiveRegisters)
    {
      _section12LiveRegisterEtl.NoOfRowsInReport = section12LiveRegisters.Count;

      List<Entities.Section12LiveRegister> currentMatchingRegisters =
        await _context.Section12LiveRegisters
                      .Where(s => section12LiveRegisters.Select(c => c.GmcNumber)
                                                        .Contains(s.GmcNumber))
                      .ToListAsync();
      _section12LiveRegisterEtl.NoOfRowsUpdated = currentMatchingRegisters.Count;

      currentMatchingRegisters.ForEach(cr =>
      {
        Section12LiveRegister updatedRegister =
          section12LiveRegisters.Single(nr => nr.GmcNumber == cr.GmcNumber);
        cr.ExpiryDate = updatedRegister.ExpiryDate;
        cr.FirstName = updatedRegister.FirstName;
        cr.GmcNumber = updatedRegister.GmcNumber;
        cr.IsActive = true;
        cr.LastName = updatedRegister.LastName;
        cr.Title = updatedRegister.Title;
        UpdateModified(cr);
      });

      List<Entities.Section12LiveRegister> newRegisters =
        section12LiveRegisters.Where(nr => !currentMatchingRegisters.Select(cr => cr.GmcNumber)
                                                                    .Contains(nr.GmcNumber))
                              .Select(nr => new Entities.Section12LiveRegister
                              {
                                ExpiryDate = nr.ExpiryDate,
                                FirstName = nr.FirstName,
                                GmcNumber = nr.GmcNumber,
                                IsActive = true,
                                LastName = nr.LastName,
                                Title = nr.Title
                              })
                              .ToList();
      _section12LiveRegisterEtl.NoOfRowsAdded = newRegisters.Count;

      newRegisters.ForEach(nr => { UpdateModified(nr); });

      _context.Section12LiveRegisters.AddRange(newRegisters);

      await _context.SaveChangesAsync();

      return true;

    }
  }
}
