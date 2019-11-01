using System.Collections.Generic;
using System.Linq;

namespace Mep.Business.Helpers
{
  public static class NhsNumber
  {
    public static bool IsValid(long nhsNumber)
    {
      return IsValid(nhsNumber.ToString());
    }

    public static bool IsValid(string nhsNumber)
    {
      return nhsNumber.ToCharArray()
                      .Count(i => i >= 48 && i <= 57) == 10 
                      ? new List<int>() { 
                          nhsNumber.ToCharArray()
                                   .Where((value, index) => index < 9)
                                   .Select((value, index) => (10 - index) * (value - 48))
                                   .Sum() }
                          .Select(i => i % 11)
                          .Select(i => (11 - i) == 11 ? 0 : (11 - i))
                          .First() == (nhsNumber[^1] - 48)
                      : false;
    }
  }
}