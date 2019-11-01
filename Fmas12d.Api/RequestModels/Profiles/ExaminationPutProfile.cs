using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
{
    public class ExaminationPutProfile : Profile
    {
        public ExaminationPutProfile()
        {
          CreateMap<ExaminationPut, BusinessModels.Examination>();
        }
    }
}