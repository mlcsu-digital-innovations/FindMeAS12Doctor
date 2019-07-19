using AutoMapper;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace Mep.Business.Test.ProfileTests
{
  public abstract class GenericProfileTest<BusinessModel, Entity>
    where BusinessModel : Mep.Business.Models.BaseModel
    where Entity : Mep.Data.Entities.BaseEntity
  {
    public void AssertBusiness2EntityMappingIsValid() {
      this.AssertBusiness2EntityMappingIsValid(new string[0]);
    }

    public void AssertBusiness2EntityMappingIsValid(IEnumerable<string> ignoredMappings)
    {
      // Mapping business model to entity
      MapperConfiguration config = CreateConfigurationForProfile();
      TypeMap typeMap = config.FindTypeMapFor<BusinessModel, Entity>();

      typeMap = IgnoreMappings(typeMap, ignoredMappings);
      var mapper = config.CreateMapper();

      try
      {
        mapper.ConfigurationProvider.AssertConfigurationIsValid(typeMap);
      }
      catch (Exception ex)
      {
        Assert.Fail(ex.Message);
      }
    }

    public void AssertEntity2BusinessMappingIsValid() {
      this.AssertEntity2BusinessMappingIsValid(new string[0]);
    }

    public void AssertEntity2BusinessMappingIsValid(IEnumerable<string> ignoredMappings)
    {
      // Mapping business model to entity
      MapperConfiguration config = CreateConfigurationForProfile();
      TypeMap typeMap = config.FindTypeMapFor<Entity, BusinessModel>();

      typeMap = IgnoreMappings(typeMap, ignoredMappings);
      var mapper = config.CreateMapper();

      try
      {
        mapper.ConfigurationProvider.AssertConfigurationIsValid(typeMap);
      }
      catch (Exception ex)
      {
        Assert.Fail(ex.Message);
      }
    }

    private TypeMap IgnoreMappings(TypeMap typeMap, IEnumerable<string> ignoredMappings)
    {
      // update the properties that are to be ignored in the mapping
      IEnumerable<PropertyMap> propertyMap = typeMap.PropertyMaps;

      foreach (string mapping in ignoredMappings)
      {
        if (propertyMap.Where(pm => String.Equals(pm.DestinationName, mapping)).Any())
        {
          propertyMap.First(pm => pm.DestinationName == mapping).Ignored = true;
        }
      }
      return typeMap;
    }

    private MapperConfiguration CreateConfigurationForProfile()
    {
      string profileName = ($"Mep.Business.Models.Profiles.{typeof(BusinessModel).Name}Profile, mep.business");
      Type profileType = Type.GetType(profileName);

      MapperConfiguration config = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile(profileType);
      });

      return config;
    }
  }
}