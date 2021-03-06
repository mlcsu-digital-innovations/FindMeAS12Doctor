using Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels
{
    public class VsrNumberPut
    {
      public int UserId { get; set; }
      public int CcgId { get; set; }
      public decimal? VsrNumber { get; set; }

      internal void MapToBusinessModel(Business.Models.IVsrNumberUpdate model)
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