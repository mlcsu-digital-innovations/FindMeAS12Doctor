using AutoMapper;
using BusinessModels = Mep.Business.Models;
using System.Linq;

namespace Mep.Api.ViewModels.Profiles
{
    public class ReferralPatientSearchProfile : Profile
    {
        public ReferralPatientSearchProfile()
        {
            CreateMap<BusinessModels.Patient, ReferralPatientSearch>()
            .ForMember(dest => dest.CcgDetails, opt => opt.MapFrom(src => src.Ccg.Name))
            .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.Id))
            .ForMember(
              dest => dest.GpPracticeDetails, 
              opt => opt.MapFrom(src => src.GpPractice.Name + " " + src.GpPractice.Postcode)
            )
            .ForMember(
              dest => dest.CurrentReferralId, 
              opt => opt.MapFrom(
                src => src.Referrals.OrderByDescending(r => r.CreatedAt)
                .Where(r => r.ReferralStatusId != 10)
                .FirstOrDefault().Id
              )
            );
        }
    }
}