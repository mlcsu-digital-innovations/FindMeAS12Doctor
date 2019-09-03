using AutoMapper;
using System.Threading.Tasks;
using Mep.Business.Models;
using Mep.Business.Models.SearchModels;
using System.Collections.Generic;
using System.Linq;
using System;
using Entities = Mep.Data.Entities;
using Mep.Business.Exceptions;

using Microsoft.EntityFrameworkCore;

namespace Mep.Business.Services
{
  public class AvailableDoctorService : IModelSimpleSearchService<AvailableDoctor, AvailableDoctorSearch>
  {
    private readonly IMapper _mapper;
    private readonly ApplicationContext _context;

    private List<AvailableDoctor> matchingDoctors;
    private DateTimeOffset examinationWindowStart;
    private DateTimeOffset examinationWindowEnd;
    private Decimal? examinationLatitude;
    private Decimal? examinationLongitude;
    private int maxDistance;

    public AvailableDoctorService(ApplicationContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<IEnumerable<AvailableDoctor>> SearchAsync(AvailableDoctorSearch searchModel)
    {
      // if the latitude and longitude haven't been supplied then obtain them from the postcode
      if (searchModel.Latitude == 0 && searchModel.Longitude == 0)
      {
        using (LocationDetailService locationService = new LocationDetailService(_mapper))
        {
          Postcode postcodeSearch = new Postcode()
          {
            Code = searchModel.PostCode
          };

          Postcode postcodeDetails = await locationService.GetPostcodeDetailsAsync(postcodeSearch);

          if (postcodeDetails.Latitude == null && postcodeDetails.Longitude == null)
          {
            throw new PostcodeNotFoundException(searchModel.PostCode);
          }
          examinationLatitude = postcodeDetails.Latitude;
          examinationLongitude = postcodeDetails.Longitude;
          maxDistance = searchModel.Distance;
        }
      }

      searchModel.ExaminationWindowStart = searchModel.ExaminationWindowStart == default(DateTime) ? new DateTimeOffset(DateTime.Now) : searchModel.ExaminationWindowStart;
      searchModel.ExaminationWindowEnd = searchModel.ExaminationWindowEnd == default(DateTime) ? new DateTimeOffset(DateTime.Now).AddHours(3) : searchModel.ExaminationWindowEnd;

      examinationWindowStart = searchModel.ExaminationWindowStart;
      examinationWindowEnd = searchModel.ExaminationWindowEnd;

      // query should look for all active doctor statuses where there availability matches the supplied date / time
      // and their distance from the location is less than the supplied limit

      IEnumerable<Entities.DoctorStatus> entities =
        await _context.DoctorStatuses
        .Include(d => d.ModifiedByUser)
        .Where(d => d.IsActive && (
          (d.AvailabilityStart <= searchModel.ExaminationWindowEnd && searchModel.ExaminationWindowStart <= d.AvailabilityEnd) ||
          (d.ExtendedAvailabilityStart1 <= searchModel.ExaminationWindowEnd && searchModel.ExaminationWindowStart <= d.ExtendedAvailabilityEnd1) ||
          (d.ExtendedAvailabilityStart2 <= searchModel.ExaminationWindowEnd && searchModel.ExaminationWindowStart <= d.ExtendedAvailabilityEnd2) ||
          (d.ExtendedAvailabilityStart3 <= searchModel.ExaminationWindowEnd && searchModel.ExaminationWindowStart <= d.ExtendedAvailabilityEnd3)
        ))
        .ToListAsync();

      IEnumerable<DoctorStatus> doctorStatuses = _mapper.Map<IEnumerable<DoctorStatus>>(entities);

      // calculate the distance for each of the available doctors
      matchingDoctors = CalculateDistanceFromExamination(doctorStatuses);

      IEnumerable<Models.AvailableDoctor> models =
        _mapper.Map<IEnumerable<Models.AvailableDoctor>>(matchingDoctors);

      return models;
    }

    private double ConvertDegreesToRadians(Decimal? degrees) {
      return (double)degrees * Math.PI / 180;  
    }

    private List<AvailableDoctor> CalculateDistanceFromExamination(IEnumerable<DoctorStatus> availableDoctors)
    {
      matchingDoctors = new List<AvailableDoctor>();

      foreach(DoctorStatus doctorStatus in availableDoctors) {
        AddDoctorToList(doctorStatus, doctorStatus.AvailabilityStart, doctorStatus.AvailabilityEnd, doctorStatus.Latitude, doctorStatus.Longitude);
      }

      return matchingDoctors;
    }

    private void AddDoctorToList(DoctorStatus doctorStatus, DateTimeOffset availabilityStart, DateTimeOffset availabilityEnd, Decimal latitude, Decimal longitude)
    {
      // is there an overlap in the dates / times ?
      if ( availabilityStart <= examinationWindowEnd && examinationWindowStart <= availabilityEnd ) {

        double distance = CalculateDistanceAsCrowFlies(latitude, longitude);

        AvailableDoctor availableDoctor = new AvailableDoctor() {
          Id = doctorStatus.Id,
          AvailabilityStart = availabilityStart,
          AvailabilityEnd = availabilityEnd,
          ModifiedByUserId = doctorStatus.ModifiedByUserId,
          ModifiedByUser = doctorStatus.ModifiedByUser,
          DistanceInMiles = Math.Round(distance,1),
          Latitude = latitude,
          Longitude = longitude,
          DoctorName = doctorStatus.ModifiedByUser.DisplayName
        };

        if (distance <= maxDistance) {
          matchingDoctors.Add(availableDoctor);
        }
      }
    }

    private double CalculateDistanceAsCrowFlies(Decimal latitude, Decimal longitude) {

      // use Haversine formula to calculate straight line distance between 2 points on a sphere

      int r = 6371000; // radius of the earth in metres 
      double phi_1 = ConvertDegreesToRadians(latitude);
      double phi_2 = ConvertDegreesToRadians(examinationLatitude);
      double delta_phi = ConvertDegreesToRadians(examinationLatitude - latitude);
      double delta_lambda = ConvertDegreesToRadians(examinationLongitude - longitude);


      double a = Math.Sin(delta_phi/2) * Math.Sin(delta_phi/2) + Math.Cos(phi_1) * Math.Cos(phi_2) * Math.Sin(delta_lambda/2) * Math.Sin(delta_lambda/2); 
      double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));
      double miles = (c*r) / 1000 / 1.609344;

      return miles;
    }

  }
}