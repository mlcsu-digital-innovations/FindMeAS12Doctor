using System;
using System.Runtime.Serialization;
using Fmas12d.Business.Models;

namespace Fmas12d.Business.Exceptions
{
  [Serializable()]
  public class ContactDetailBaseMissingForCcgUserException : SerilogException
  {
    public ContactDetailBaseMissingForCcgUserException(
      int ccgId,
      int  userId
    )
    {
      PropertyValues = new object[3];
      PropertyValues[0] = userId;
      PropertyValues[1] = ContactDetailType.BASE;
      PropertyValues[2] = ccgId;
    }

    protected ContactDetailBaseMissingForCcgUserException(
      SerializationInfo info, StreamingContext context)
      : base(info, context) { }

    protected override string GetMessage()
    {
      return "User {0} does not have the required base {1} contact detail type for CCG {2}.";
    }

    protected override string GetMessageTemplate()
    {
      return "User {userId} does not have the required base {ContactDetailTypeId} " +
             "contact detail type for CCG {CcgId}.";
    }
  }
}