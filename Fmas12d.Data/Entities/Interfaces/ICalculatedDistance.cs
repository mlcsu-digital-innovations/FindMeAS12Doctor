namespace Fmas12d.Data.Entities
{
    public interface ICalculatedDistance : IBaseEntity
    {
      decimal Distance { get; set; }
      decimal EndLatitude { get; set; }
      decimal EndLongitude { get; set; }
      int EstimatedJourneyTime { get; set; }
      decimal StartLatitude { get; set; }
      decimal StartLongitude { get; set; }
    }
}