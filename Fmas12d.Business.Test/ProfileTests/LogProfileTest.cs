using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
using AutoMapper;
using Mep.Business.Models.Profiles;

namespace Mep.Business.Test
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