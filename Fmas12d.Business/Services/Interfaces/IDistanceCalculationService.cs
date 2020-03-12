using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
    public interface IDistanceCalculationService
    {
        Task<decimal> CalculateRoadDistanceBetweenPoints(
          decimal startLatitude,
          decimal startLongitude,
          decimal endLatitude,
          decimal endLongitude
        );
    }
}