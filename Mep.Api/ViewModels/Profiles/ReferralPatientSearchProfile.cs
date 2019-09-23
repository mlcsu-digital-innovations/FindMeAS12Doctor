using AutoMapper;
using BusinessModels = Mep.Business.Models;
using System.Linq;
using Enum = Mep.Api.Enums;

namespace Mep.Api.ViewModels.Profiles
{
    public class ReferralPatientSearchProfile : Profile
    {
        public ReferralPatientSearchProfile()
        {
            CreateMap<BusinessModels.Patient, ReferralPatientSearch>()
            .ForMember(dest => dest.CcgName, opt => opt.MapFrom(src => src.Ccg.Name))
            .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.Id))
            .ForMember(
              dest => dest.GpPracticeNameAndPostcode, 
              opt => opt.MapFrom(src => src.GpPractice.Name + ", " + src.GpPractice.Postcode)
            )
            .ForMember(
              dest => dest.CurrentReferralId, 
              opt => opt.MapFrom(
                src => src.Referrals.OrderByDescending(r => r.CreatedAt)
                .FirstOrDefault(r => r.ReferralStatusId != (int)Enum.ReferralStatus.ReferralClosed).Id
              )
            );
        }
    }
}