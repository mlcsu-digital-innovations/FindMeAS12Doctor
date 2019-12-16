public interface IContactDetailCcg
  {
    int CcgId { get; set; }
    int ContactDetailTypeId { get; set; }
    string EmailAddress { get; set; }
    long? TelephoneNumber { get; set; }
    int UserId { get; set; }
  }