using CsvHelper;
using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Models;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace Fmas12d.Business.Services
{
  public class Section12LiveRegisterService
    : ServiceBase<Entities.Section12LiveRegister>,
    ISection12LiveRegisterService
  {

    private readonly Section12LiveRegisterEtl _section12LiveRegisterEtl;

    public Section12LiveRegisterService(
      ApplicationContext context,
      IUserClaimsService userClaimsService)
      : base(context, userClaimsService)
    {
      _section12LiveRegisterEtl = new Section12LiveRegisterEtl()
      {
        LoadedDate = DateTimeOffset.Now
      };      
    }

    public async Task<Section12LiveRegisterEtl> PerformEtlAsync(string filePath)
    {
      await EtlSection12RegisterReport(filePath);
      await UpdateUserSection12ApprovalStatus();

      return _section12LiveRegisterEtl;
    }

    private async Task<bool> UpdateUserSection12ApprovalStatus()
    {

      // not sure how to do this with linq expressions
      var query = from user in _context.Set<Entities.User>()
                  join s12LiveRegister in _context.Set<Entities.Section12LiveRegister>()
                    on user.GmcNumber equals s12LiveRegister.GmcNumber
                  select new Entities.User(){
                    Id = user.Id,
                    Section12ExpiryDate = s12LiveRegister.ExpiryDate
                  };
      List<Entities.User> updatedUsers = await query.ToListAsync();

      await _context.Users
                    .Where(u => u.GmcNumber != null)
                    .ForEachAsync(u => {
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

    private async Task<bool> EtlSection12RegisterReport(string filePath)
    {
      using StreamReader streamReader = new StreamReader(filePath);
      using CsvReader csvReader = new CsvReader(streamReader);

      List<Section12LiveRegisterCsv> newRegistersFromCsv = csvReader
        .GetRecords<Section12LiveRegisterCsv>()
        .Where(r => !string.IsNullOrWhiteSpace(r.DateOfS12ExpiryString))
        .Where(r => r.Prn.All(c => char.IsNumber(c)))
        .ToList();
      _section12LiveRegisterEtl.NoOfRowsInReport = newRegistersFromCsv.Count;

      List<Entities.Section12LiveRegister> currentMatchingRegisters =
        await _context.Section12LiveRegisters
                      .Where(s => newRegistersFromCsv.Select(c => c.GmcNumber)
                                                     .Contains(s.GmcNumber))
                      .ToListAsync();                      
      _section12LiveRegisterEtl.NoOfRowsUpdated = currentMatchingRegisters.Count;

      currentMatchingRegisters.ForEach(cr =>
      {
        Section12LiveRegisterCsv newRegister =
          newRegistersFromCsv.Single(nr => nr.GmcNumber == cr.GmcNumber);
        cr.ExpiryDate = newRegister.DateOfS12Expiry;
        cr.FirstName = newRegister.FirstName;
        cr.GmcNumber = newRegister.GmcNumber;
        cr.IsActive = true;
        cr.LastName = newRegister.LastName;
        cr.Title = newRegister.Title;
        UpdateModified(cr);
      });

      List<Entities.Section12LiveRegister> newRegisters =
        newRegistersFromCsv.Where(nr => !currentMatchingRegisters.Select(cr => cr.GmcNumber)
                                                                 .Contains(nr.GmcNumber))
                           .Select(nr => new Entities.Section12LiveRegister
                           {
                             ExpiryDate = nr.DateOfS12Expiry,
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
