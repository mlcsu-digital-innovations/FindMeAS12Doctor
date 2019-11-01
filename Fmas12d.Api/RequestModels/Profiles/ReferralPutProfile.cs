using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
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