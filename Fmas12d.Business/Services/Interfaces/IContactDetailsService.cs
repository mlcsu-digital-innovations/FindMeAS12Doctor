using System.Threading.Tasks;
using Fmas12d.Business.Models;

public interface IContactDetailsService
{
  Task<ContactDetail> GetBaseContactDetailTypeForCcgUserAsync(
    int ccgId,
    int userId, 
    bool asNoTracking, 
    bool activeOnly
  );
  Task<ContactDetail> GetByIdAndUserIdAsync(
    int id, 
    int userId, 
    bool asNoTracking, 
    bool activeOnly
  );
}