using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels.Profiles
{
    public class AssessmentPutProfile : Profile
    {
        public AssessmentPutProfile()
        {
          CreateMap<AssessmentPut, BusinessModels.Assessment>();
        }
    }
}