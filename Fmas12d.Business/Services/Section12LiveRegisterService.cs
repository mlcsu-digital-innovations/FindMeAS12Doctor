using CsvHelper;
using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Models;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Fmas12d.Business.Services
{
  public class Section12LiveRegisterService
    : ServiceBase<Entities.Section12LiveRegister>,
    ISection12LiveRegisterService
  {
    public Section12LiveRegisterService(
      ApplicationContext context,
      IUserClaimsService userClaimsService)
      : base(context, userClaimsService)
    {
    }

    public async Task<Section12LiveRegisterEtl> PerformEtlAsync(string filePath)
    {
      using StreamReader streamReader = new StreamReader(filePath);
      using CsvReader csvReader = new CsvReader(streamReader);

      Section12LiveRegisterEtl section12LiveRegisterEtl = new Section12LiveRegisterEtl()
      {
        LoadedDate = DateTimeOffset.Now
      };

      List<Section12LiveRegisterCsv> newRegistersFromCsv = csvReader
        .GetRecords<Section12LiveRegisterCsv>()
        .Where(r => !string.IsNullOrWhiteSpace(r.DateOfS12ExpiryString))
        .Where(r => r.Prn.All(c => char.IsNumber(c)))
        .ToList();
      section12LiveRegisterEtl.NoOfRowsInReport = newRegistersFromCsv.Count;

      List<Entities.Section12LiveRegister> currentMatchingRegisters =
        _context.Section12LiveRegisters
                .Where(s => newRegistersFromCsv.Select(c => c.GmcNumber).Contains(s.GmcNumber))
                .ToList();
      section12LiveRegisterEtl.NoOfRowsUpdated = currentMatchingRegisters.Count;

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
      section12LiveRegisterEtl.NoOfRowsAdded = newRegisters.Count;

      newRegisters.ForEach(nr => { UpdateModified(nr); });

      _context.Section12LiveRegisters.AddRange(newRegisters);

      await _context.SaveChangesAsync();

      return section12LiveRegisterEtl;
    }

  }
}
