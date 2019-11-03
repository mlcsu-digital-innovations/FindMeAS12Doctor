using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
using AutoMapper;
using Fmas12d.Business.Models.Profiles;

namespace Fmas12d.Business.Test
{
  [TestClass]
  public class LogProfileTest 
  {
    [TestMethod]
    public void LogProfileIsValid()
    {      
      MapperConfiguration config = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile<LogProfile>();
      });

      IMapper mapper = config.CreateMapper();
      mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
  }
}