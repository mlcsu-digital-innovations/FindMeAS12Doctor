using System.Threading.Tasks;
using AutoMapper;
using Mep.Business.Models.SearchModels;
using System.Collections.Generic;

namespace Mep.Business.Services
{
  public abstract class GeneralSearchServiceBase
  {
    protected readonly ApplicationContext _context;
    protected readonly IMapper _mapper;
    protected readonly string _typeName;

    public abstract Task<IEnumerable<GeneralSearchResult>> SearchAsync(string searchString);

    protected GeneralSearchServiceBase(string typeName, ApplicationContext context, IMapper mapper)
    {
      _typeName = typeName;
      _context = context;
      _mapper = mapper;
    }
  }
}