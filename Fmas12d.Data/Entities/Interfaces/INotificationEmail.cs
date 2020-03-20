namespace Fmas12d.Data.Entities
{
    public interface INotificationEmail : INotificationText
    {
      string SubjectTemplate { get; set; }   
    }
}