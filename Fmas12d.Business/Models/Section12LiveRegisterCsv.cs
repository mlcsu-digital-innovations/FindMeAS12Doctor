using System;
using System.Globalization;
using CsvHelper.Configuration.Attributes;

namespace Fmas12d.Business.Models
{
  public class Section12LiveRegisterCsv
  {
    [Name(" Address 1")]
    public string Address1 { get; set; }
    [Name(" Address 2")]
    public string Address2 { get; set; }
    [Name(" Approved Clinician")]
    public bool ApprovedClinician { get; set; }
    [Name(" County")]
    public string County { get; set; }
    [Name(" Date Of AC Expiry")]
    public string DateOfAcExpiryString { get; set; }
    [Name(" Date Of S12 Expiry")]
    public string DateOfS12ExpiryString { get; set; }
    [Name(" DOLS Assessor")]
    public bool DolsAssessor { get; set; }
    [Name(" Email")]
    public string Email { get; set; }
    [Name(" First Name")]
    public string FirstName { get; set; }
    [Name(" Grade")]
    public string Grade { get; set; }
    [Name(" Mobile")]
    public string Mobile { get; set; }
    [Name(" Organisation")]
    public string Organisation { get; set; }
    [Name(" Post Code")]
    public string Postcode { get; set; }
    [Name(" PRN")]
    public string Prn { get; set; }
    [Name(" Surname")]
    public string Surname { get; set; }
    [Name(" Telephone")]
    public string Telephone { get; set; }
    [Name("Title")]
    public string Title { get; set; }
    [Name(" Town")]
    public string Town { get; set; }

    public DateTimeOffset DateOfS12Expiry
    {
      get
      {
        return DateTime.ParseExact(
          DateOfS12ExpiryString,
          "d/M/YYYY",
          CultureInfo.InvariantCulture
        );
      }
    }

    public int GmcNumber
    {
      get
      {
        return int.Parse(Prn);
      }
    }
  }
}