using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
{
  public class ExaminationDetailProfile : Profile
  {
    public ExaminationDetailProfile()
    {
      CreateMap<Entities.ExaminationDetail, Models.ExaminationDetail>();

      CreateMap<Models.ExaminationDetail , Entities.ExaminationDetail>();
    }
  }
}