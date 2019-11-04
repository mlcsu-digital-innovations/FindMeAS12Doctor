// using AutoMapper;
// using System.Threading.Tasks;
// using Fmas12d.Business.Models;
// using Fmas12d.Business.Models.SearchModels;
// using System.Collections.Generic;
// using System.Linq;
// using System;
// using Entities = Fmas12d.Data.Entities;
// using Fmas12d.Business.Exceptions;

// using Microsoft.EntityFrameworkCore;

// namespace Fmas12d.Business.Services
// {
//   public class AvailableDoctorService : IModelSimpleSearchService<AvailableDoctor, AvailableDoctorSearch>
//   {
//     private readonly IMapper _mapper;
//     private readonly ApplicationContext _context;

//     private List<AvailableDoctor> matchingDoctors;
//     private DateTimeOffset assessmentWindowStart;
//     private DateTimeOffset assessmentWindowEnd;
//     private Decimal? assessmentLatitude;
//     private Decimal? assessmentLongitude;
//     private int maxDistance;

//     public AvailableDoctorService(ApplicationContext context, IMapper mapper)
//     {
//       _mapper = mapper;
//       _context = context;
//     }

//     public async Task<IEnumerable<AvailableDoctor>> SearchAsync(AvailableDoctorSearch searchModel)
//     {
//       // if the latitude and longitude haven't been supplied then obtain them from the postcode
//       if (searchModel.Latitude == 0 && searchModel.Longitude == 0)
//       {
//         using LocationDetailService locationService = new LocationDetailService(_mapper);
//         Postcode postcodeSearch = new Postcode()
//         {
//           Code = searchModel.PostCode
//         };

//         Postcode postcodeDetails = await locationService.GetPostcodeDetailsAsync(postcodeSearch);

//         if (postcodeDetails.Latitude == null && postcodeDetails.Longitude == null)
//         {
//           throw new PostcodeNotFoundException(searchModel.PostCode);
//         }
//         assessmentLatitude = postcodeDetails.Latitude;
//         assessmentLongitude = postcodeDetails.Longitude;
//         maxDistance = searchModel.Distance;
//       }

//       searchModel.AssessmentWindowStart = searchModel.AssessmentWindowStart == default(DateTime) ? new DateTimeOffset(DateTime.Now) : searchModel.AssessmentWindowStart;
//       searchModel.AssessmentWindowEnd = searchModel.AssessmentWindowEnd == default(DateTime) ? new DateTimeOffset(DateTime.Now).AddHours(3) : searchModel.AssessmentWindowEnd;

//       assessmentWindowStart = searchModel.AssessmentWindowStart;
//       assessmentWindowEnd = searchModel.AssessmentWindowEnd;

//       // query should look for all active doctor statuses where there availability matches the supplied date / time
//       // and their distance from the location is less than the supplied limit

//       IEnumerable<Entities.UserAvailability> entities =
//         await _context.UserAvailabilities
//         .Include(d => d.ModifiedByUser)
//         .Where(d => d.IsActive && (
//           (d.Start <= searchModel.AssessmentWindowEnd && searchModel.AssessmentWindowStart <= d.End)
//         ))
//         .ToListAsync();

//       IEnumerable<UserAvailability> doctorAvailabilities = _mapper.Map<IEnumerable<UserAvailability>>(entities);

//       // calculate the distance for each of the available doctors
//       matchingDoctors = CalculateDistanceFromAssessment(doctorAvailabilities);

//       IEnumerable<Models.AvailableDoctor> models =
//         _mapper.Map<IEnumerable<Models.AvailableDoctor>>(matchingDoctors);

//       return models;
//     }

//     private double ConvertDegreesToRadians(Decimal? degrees) {
//       return (double)degrees * Math.PI / 180;  
//     }

//     private List<AvailableDoctor> CalculateDistanceFromAssessment(IEnumerable<UserAvailability> availableDoctors)
//     {
//       matchingDoctors = new List<AvailableDoctor>();

//       foreach(UserAvailability doctorAvailability in availableDoctors) {
//         AddDoctorToList(
//           doctorAvailability, 
//           doctorAvailability.Start, 
//           doctorAvailability.End, 
//           doctorAvailability.Latitude.Value, 
//           doctorAvailability.Longitude.Value);
//       }

//       return matchingDoctors;
//     }

//     private void AddDoctorToList(
//       UserAvailability doctorAvailability, 
//       DateTimeOffset availabilityStart, 
//       DateTimeOffset availabilityEnd,
//       decimal latitude, 
//       decimal longitude)
//     {
//       // is there an overlap in the dates / times ?
//       if ( availabilityStart <= assessmentWindowEnd && assessmentWindowStart <= availabilityEnd ) {

//         double distance = CalculateDistanceAsCrowFlies(latitude, longitude);

//         AvailableDoctor availableDoctor = new AvailableDoctor() {
//           Id = doctorAvailability.Id,
//           AvailabilityStart = availabilityStart,
//           AvailabilityEnd = availabilityEnd,
//           ModifiedByUserId = doctorAvailability.ModifiedByUserId,
//           ModifiedByUser = doctorAvailability.ModifiedByUser,
//           DistanceInMiles = Math.Round(distance,1),
//           Latitude = latitude,
//           Longitude = longitude,
//           DoctorName = doctorAvailability.ModifiedByUser.DisplayName
//         };

//         if (distance <= maxDistance) {
//           matchingDoctors.Add(availableDoctor);
//         }
//       }
//     }

//     private double CalculateDistanceAsCrowFlies(Decimal latitude, Decimal longitude) {

//       // use Haversine formula to calculate straight line distance between 2 points on a sphere

//       int r = 6371000; // radius of the earth in metres 
//       double phi_1 = ConvertDegreesToRadians(latitude);
//       double phi_2 = ConvertDegreesToRadians(assessmentLatitude);
//       double delta_phi = ConvertDegreesToRadians(assessmentLatitude - latitude);
//       double delta_lambda = ConvertDegreesToRadians(assessmentLongitude - longitude);


//       double a = Math.Sin(delta_phi/2) * Math.Sin(delta_phi/2) + Math.Cos(phi_1) * Math.Cos(phi_2) * Math.Sin(delta_lambda/2) * Math.Sin(delta_lambda/2); 
//       double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));
//       double miles = (c*r) / 1000 / 1.609344;

//       return miles;
//     }

//   }
// }