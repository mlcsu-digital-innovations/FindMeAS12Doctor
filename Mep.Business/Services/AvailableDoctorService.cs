using AutoMapper;
using System.Threading.Tasks;
using Mep.Business.Models;
using Mep.Business.Models.SearchModels;
using System.Collections.Generic;
using System.Linq;
using System;
using Entities = Mep.Data.Entities;

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

    public async Task<IEnumerable<AvailableDoctor>> SearchAsync(AvailableDoctorSearch searchParams)
    {
      // TODO - is there a better way of converting the object to AvailableDoctor ?
      // TODO - is this query going to work well enough ?

      // if the latitude and longitude haven't been supplied then obtain them from the postcode
      if (searchParams.Latitude == 0 && searchParams.Longitude == 0)
      {
        using (LocationDetailService locationService = new LocationDetailService(_mapper))
        {
          Postcode postcodeSearch = new Postcode()
          {
            Code = searchParams.PostCode
          };

          Postcode postcodeDetails = await locationService.GetPostcodeDetailsAsync(postcodeSearch);

          if (postcodeDetails.Latitude == null && postcodeDetails.Longitude == null)
          {
            throw new Exception($"Details for examination postcode [{searchParams.PostCode}] not found");
          }
          examinationLatitude = postcodeDetails.Latitude;
          examinationLongitude = postcodeDetails.Longitude;
          maxDistance = searchParams.Distance;
        }
      }

      searchParams.ExaminationWindowStart = searchParams.ExaminationWindowStart == default ? new DateTimeOffset(DateTime.Now) : searchParams.ExaminationWindowStart;
      searchParams.ExaminationWindowEnd = searchParams.ExaminationWindowEnd == default ? new DateTimeOffset(DateTime.Now).AddHours(3) : searchParams.ExaminationWindowEnd;

      examinationWindowStart = searchParams.ExaminationWindowStart;
      examinationWindowEnd = searchParams.ExaminationWindowEnd;

      // query should look for all active doctor statuses where there availability matches the supplied date / time
      // and their distance from the location is less than the supplied limit

      IEnumerable<Entities.DoctorStatus> entities =
        await _context.DoctorStatuses
        .Include(d => d.ModifiedByUser)
        .Where(d => d.IsActive == true && (
          (d.AvailabilityStart <= searchParams.ExaminationWindowEnd && searchParams.ExaminationWindowStart <= d.AvailabilityEnd) ||
          (d.ExtendedAvailabilityStart1 <= searchParams.ExaminationWindowEnd && searchParams.ExaminationWindowStart <= d.ExtendedAvailabilityEnd1) ||
          (d.ExtendedAvailabilityStart2 <= searchParams.ExaminationWindowEnd && searchParams.ExaminationWindowStart <= d.ExtendedAvailabilityEnd2) ||
          (d.ExtendedAvailabilityStart3 <= searchParams.ExaminationWindowEnd && searchParams.ExaminationWindowStart <= d.ExtendedAvailabilityEnd3)
        ))
        .ToListAsync();

      IEnumerable<DoctorStatus> doctorStatuses = _mapper.Map<IEnumerable<DoctorStatus>>(entities);

      // calculate the distance for each of the available doctors
      List<AvailableDoctor> matchingDoctors = CalculateDistanceFromExamination(doctorStatuses, searchParams);

      IEnumerable<Models.AvailableDoctor> models =
        _mapper.Map<IEnumerable<Models.AvailableDoctor>>(matchingDoctors);

      return models;
    }

    private double ConvertDegreesToRadians(Decimal? degrees) {
      return (double)degrees * Math.PI / 180;  
    }

    private List<AvailableDoctor> CalculateDistanceFromExamination(IEnumerable<DoctorStatus> availableDoctors, AvailableDoctorSearch searchParams)
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



      return ;
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