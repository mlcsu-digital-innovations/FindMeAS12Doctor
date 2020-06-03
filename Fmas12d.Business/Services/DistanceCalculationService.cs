using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Exceptions;
using Fmas12d.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Fmas12d.Business.Helpers;

namespace Fmas12d.Business.Services
{
  public class DistanceCalculationService : IDistanceCalculationService
  {
    protected readonly ApplicationContext _context;
    protected readonly IConfiguration _configuration;
    protected readonly IUserClaimsService _userClaimsService;

    private readonly string _googleDistanceMatrixEndpoint;
    private readonly string _googleDistanceMatrixKey;
    private readonly bool _googleDistanceMatrixKeyAvailable;

    public DistanceCalculationService(
      ApplicationContext context,
      IConfiguration config,
      IUserClaimsService userClaimsService)
    {
      _configuration = config;
      _context = context;
      _userClaimsService = userClaimsService;

      _googleDistanceMatrixEndpoint =
        config.GetValue(
          "GoogleDistanceMatrixEndpoint",
          "https://maps.googleapis.com/maps/api/distancematrix/json"
        );
      Serilog.Log.Debug
      (
        "GoogleDistanceMatrixEndpoint config {GoogleDistanceMatrixEndpoint}",
        _googleDistanceMatrixEndpoint
      );

      _googleDistanceMatrixKey = config.GetValue("GoogleDistanceMatrixKey", "");
      Serilog.Log.Debug(
        "GoogleDistanceMatrixKey config {GoogleDistanceMatrixKey}",
        _googleDistanceMatrixKey
      );

      _googleDistanceMatrixKeyAvailable = !string.IsNullOrWhiteSpace(_googleDistanceMatrixKey);
      if (!_googleDistanceMatrixKeyAvailable)
      {
        Serilog.Log.Warning("GoogleDistanceMatrixKey in configuration file is blank");
      }
    }

    public async Task<decimal> CalculateRoadDistanceBetweenPoints(
      decimal startLatitude,
      decimal startLongitude,
      decimal endLatitude,
      decimal endLongitude
      )
    {

      decimal distance = 0;

      // query db
      Entities.CalculatedDistance entity =
        await _context.CalculatedDistances
        .Where(a => a.StartLatitude == startLatitude)
        .Where(a => a.StartLongitude == startLongitude)
        .Where(a => a.EndLatitude == endLatitude)
        .Where(a => a.EndLongitude == endLongitude)
        .SingleOrDefaultAsync();

      if (entity != null)
      {
        Serilog.Log.Debug($"Distance retrieved from DB");
        distance = entity.Distance;
      }
      else
      {

        if (!_googleDistanceMatrixKeyAvailable)
        {
          return Distance.CalculateDistanceAsCrowFlies(
                startLatitude,
                startLongitude,
                endLatitude,
                endLongitude
          );
        }

        // if nothing in db and matrix key available then query google
        GoogleDistanceMatrixResult distanceMatrixResult = null;

        try
        {
          string url =
            $"{_googleDistanceMatrixEndpoint}?units=imperial&origins={startLatitude}," +
            $"{startLongitude}&destinations={endLatitude},{endLongitude}" +
            $"&key={_googleDistanceMatrixKey}";

          WebRequest request = WebRequest.Create(url);
          WebResponse response = request.GetResponse();
          Stream data = response.GetResponseStream();
          StreamReader reader = new StreamReader(data);

          // json-formatted string from maps api
          string responseFromServer = reader.ReadToEnd();
          response.Close();

          distanceMatrixResult =
            JsonConvert.DeserializeObject<GoogleDistanceMatrixResult>(responseFromServer);

          GoogleDistanceMatrixElement result = distanceMatrixResult.Rows[0].Elements[0];

          bool canParse =
            decimal.TryParse(result.Distance.Value, out distance);

          if (canParse)
          {
            bool validJourneyTime = int.TryParse(result.Duration.Value, out int journeyTime);

            distance /= (decimal)1609.34;

            Entities.CalculatedDistance calculatedDistance = await CreateAsync(
              startLatitude,
              startLongitude,
              endLatitude,
              endLongitude,
              distance,
              validJourneyTime ? journeyTime : 0
            );

            distance = calculatedDistance.Distance;
            Serilog.Log.Debug($"Distance saved to DB");

          }
          else
          {
            Serilog.Log.Error($"Unable to parse: {result.Status}. Use Distance Helper Method");
            distance = Distance.CalculateDistanceAsCrowFlies(
              startLatitude,
              startLongitude,
              endLatitude,
              endLongitude
            );
          }

        }
        catch
        {
          Serilog.Log.Error
            ($"{distanceMatrixResult.Status} {distanceMatrixResult.Error_Message}");
          //throw new GoogleDistanceMatrixException(distanceMatrixResult.Status);

          distance = Distance.CalculateDistanceAsCrowFlies(
            startLatitude,
            startLongitude,
            endLatitude,
            endLongitude
          );
        }
      }

      return distance;
    }

    private async Task<Entities.CalculatedDistance> CreateAsync(
      decimal startLatitude,
      decimal startLongitude,
      decimal endLatitude,
      decimal endLongitude,
      decimal distance,
      int journeyTime
      )
    {

      Entities.CalculatedDistance calculatedDistance = new Entities.CalculatedDistance
      {
        IsActive = true,
        StartLatitude = startLatitude,
        StartLongitude = startLongitude,
        EndLatitude = endLatitude,
        EndLongitude = endLongitude,
        Distance = distance,
        EstimatedJourneyTime = journeyTime,
        ModifiedAt = DateTimeOffset.Now,
        ModifiedByUserId = _userClaimsService.GetUserId()
      };

      _context.Add(calculatedDistance);
      await _context.SaveChangesAsync();

      return calculatedDistance;
    }
  }
}