using System;
using System.Net;
using System.IO;
using Fmas12d.Business.Models;
using Newtonsoft.Json;

namespace Fmas12d.Business.Helpers
{
  public static class Distance
  {
    public static string GoogleDistanceMatrixKey { get; set; }
    public static string GoogleDistanceMatrixEndpoint { get; set; }

    private static double ConvertDegreesToRadians(decimal? degrees)
    {
      return (double)degrees * Math.PI / 180.0;
    }

    /// <summary>
    /// Uses Haversine formula to calculate straight line distance between 2 points on a sphere
    /// </summary>
    public static decimal CalculateDistanceAsCrowFlies(
      decimal startLatitude,
      decimal startLongitude,
      decimal endLatitude,
      decimal endlongitude
      )
    {

      int r = 6371000; // radius of the earth in metres 
      double phi_1 = ConvertDegreesToRadians(endLatitude);
      double phi_2 = ConvertDegreesToRadians(startLatitude);
      double delta_phi = ConvertDegreesToRadians(startLatitude - endLatitude);
      double delta_lambda = ConvertDegreesToRadians(startLongitude - endlongitude);


      double a = Math.Sin(delta_phi / 2) * Math.Sin(delta_phi / 2) + Math.Cos(phi_1) *
                 Math.Cos(phi_2) * Math.Sin(delta_lambda / 2) * Math.Sin(delta_lambda / 2);
      double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
      double miles = (c * r) / 1000 / 1.609344;

      miles = Math.Truncate(miles * 1000) / 1000;
      // prevent test data from generating errors
      miles = miles > 999 ? 999 : miles;

      return (decimal)miles;
    }
  }
}

