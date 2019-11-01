using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels.Profiles
{
    public class ReferralPutProfile : Profile
    {
        public ReferralPutProfile()
        {
          CreateMap<User, BusinessModels.User>();
          CreateMap<Patient, BusinessModels.Patient>();
          CreateMap<ReferralStatus, BusinessModels.ReferralStatus>();
          CreateMap<ReferralPut, BusinessModels.Referral>();
        }
    }
}