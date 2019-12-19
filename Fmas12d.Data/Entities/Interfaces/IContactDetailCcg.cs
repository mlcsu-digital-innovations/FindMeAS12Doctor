public interface IContactDetailCcg
  {
    int CcgId { get; set; }
    int ContactDetailTypeId { get; set; }
    string EmailAddress { get; set; }
    string TelephoneNumber { get; set; }
    int UserId { get; set; }
  }