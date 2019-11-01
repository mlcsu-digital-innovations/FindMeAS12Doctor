// using AutoMapper;
// using BusinessModels = Mep.Business.Models;

// namespace Mep.Api.ViewModels.Profiles
// {
//     public class ReferralPatientSearchProfile : Profile
//     {
//         public ReferralPatientSearchProfile()
//         {
//             CreateMap<BusinessModels.Patient, ReferralPatientSearch>()
//             .ForMember(dest => dest.CcgName, opt => opt.MapFrom(src => src.Ccg.Name))
//             .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.Id))
//             .ForMember(dest => dest.CurrentReferralId, opt => opt.MapFrom(src => src.GetCurrentReferralId()));
//         }
//     }
// }