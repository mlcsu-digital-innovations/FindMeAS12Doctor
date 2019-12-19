using System;
using System.Runtime.Serialization;
using Fmas12d.Business.Models;

namespace Fmas12d.Business.Exceptions
{
  [Serializable()]
  public class ContactDetailBaseMissingForUserException : SerilogException
  {
    public ContactDetailBaseMissingForUserException(
      int  userId
    )
    {
      PropertyValues = new object[3];
      PropertyValues[0] = userId;
      PropertyValues[1] = ContactDetailType.BASE;
    }

    protected ContactDetailBaseMissingForUserException(
      SerializationInfo info, StreamingContext context)
      : base(info, context) { }

    protected override string GetMessage()
    {
      return "User {0} does not have the required base {1} contact detail type.";
    }

    protected override string GetMessageTemplate()
    {
      return "User {userId} does not have the required base {ContactDetailTypeId} " +
             "contact detail type.";
    }
  }
}