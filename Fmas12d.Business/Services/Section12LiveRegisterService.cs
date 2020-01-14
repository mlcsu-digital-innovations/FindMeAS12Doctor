using CsvHelper;
using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Models;
using System.IO;
using System.Collections.Generic;
using System.Linq;

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

    public Section12LiveRegisterEtl PerformEtl(string filePath)
    {
      using (StreamReader streamReader = new StreamReader(filePath))
      using (CsvReader csvReader = new CsvReader(streamReader))
      {
        List<Section12LiveRegisterCsv> rows = csvReader
          .GetRecords<Section12LiveRegisterCsv>()
          .Where(r => !string.IsNullOrWhiteSpace(r.DateOfS12ExpiryString))
          .Where(r => r.Prn.All(c => char.IsNumber(c)))
          .ToList();
      }
      return null;
    }

  }
}
