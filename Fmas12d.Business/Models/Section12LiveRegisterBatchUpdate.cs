using System;

namespace Fmas12d.Business.Models
{
  public class Section12LiveRegisterBatchUpdateResult
  {
    public DateTimeOffset LoadedDate { get; set; }
    public int NoOfRowsAdded { get; set; }
    public int NoOfRowsInReport { get; set; }
    public int NoOfRowsUpdated { get; set; }
  }
}