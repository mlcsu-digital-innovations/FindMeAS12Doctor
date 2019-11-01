using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels.Profiles
{
    public class ExaminationPutProfile : Profile
    {
        public ExaminationPutProfile()
        {
          CreateMap<ExaminationPut, BusinessModels.Examination>();
        }
    }
}