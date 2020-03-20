using Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels
{
    public class VsrUpdate
    {
      public int UserId { get; set; }
      public int CcgId { get; set; }
      public int VsrNumber { get; set; }

      internal void MapToBusinessModel(Business.Models.IVsrUpdate model)
      {      
        if (model != null)
        {
          model.UserId = UserId;
          model.CcgId = CcgId;
          model.VsrNumber = VsrNumber;
        }
      }
    }
}