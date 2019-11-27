using System.Threading.Tasks;
using Fmas12d.Business.Models;

public interface IContactDetailsService
{
  Task<ContactDetail> Get(int id, int userId, bool asNoTracking, bool activeOnly);
}