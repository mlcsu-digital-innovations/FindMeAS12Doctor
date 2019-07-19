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
    public void AssertBusiness2EntityMappingIsValid()
    {
      this.AssertBusiness2EntityMappingIsValid(new string[0]);
    }

    public void AssertBusiness2EntityMappingIsValid(IEnumerable<string> ignoredMappings)
    {
      try
      {
        // Mapping business model to entity
        IMapper mapper = CreateMapperForProfile();
        TypeMap typeMap = mapper.ConfigurationProvider.FindTypeMapFor<BusinessModel, Entity>();

        typeMap = IgnoreMappings(typeMap, ignoredMappings);
        AssertConfigurationIsValid(mapper, typeMap);
      }
      catch (Exception ex)
      {
        Assert.Fail(ex.Message);
      }
    }

    public void AssertEntity2BusinessMappingIsValid()
    {
      this.AssertEntity2BusinessMappingIsValid(new string[0]);
    }

    public void AssertEntity2BusinessMappingIsValid(IEnumerable<string> ignoredMappings)
    {
      try
      {
        // Mapping entity to business model
        IMapper mapper = CreateMapperForProfile();
        TypeMap typeMap = mapper.ConfigurationProvider.FindTypeMapFor<Entity, BusinessModel>();

        typeMap = IgnoreMappings(typeMap, ignoredMappings);
        AssertConfigurationIsValid(mapper, typeMap);
      }
      catch (Exception ex)
      {
        Assert.Fail(ex.Message);
      }
    }

    private void AssertConfigurationIsValid(IMapper mapper, TypeMap typeMap)
    {
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
      try
      {
        // update the properties that are to be ignored in the mapping
        IEnumerable<PropertyMap> propertyMap = typeMap.PropertyMaps;
        foreach (string mapping in ignoredMappings)
        {
          if (propertyMap.Any(pm => String.Equals(pm.DestinationName, mapping)))
          {
            propertyMap.First(pm => pm.DestinationName == mapping).Ignored = true;
          }
          else {
            Assert.Fail($"Type map profile '{typeMap.Profile.Name}' does not contain a property for the ignored mapping '{mapping}'");
          }
        }
      }
      catch (Exception ex)
      {
        Assert.Fail(ex.Message);
      }

      return typeMap;
    }

    private IMapper CreateMapperForProfile()
    {
      string profileName = ($"{typeof(BusinessModel).Namespace}.Profiles.{typeof(BusinessModel).Name}Profile, {typeof(BusinessModel).Assembly}");
      Type profileType = Type.GetType(profileName);

      MapperConfiguration config = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile(profileType);
      });

      IMapper mapper = config.CreateMapper();

      return mapper;
    }
  }
}