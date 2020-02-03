namespace Fmas12d.Business.Services
{
  public interface IUserClaimsService
  {
    int GetUserId();
    bool HasUserIdClaim();
    bool IsUserAdmin();
  }
}