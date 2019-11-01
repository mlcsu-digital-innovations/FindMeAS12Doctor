using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
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