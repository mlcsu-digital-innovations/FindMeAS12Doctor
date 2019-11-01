using System.Threading.Tasks;
using AutoMapper;
using Mep.Business.Models;
using Mep.Data.Entities;
using Mep.Business.Models.SearchModels;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Mep.Business.Services
{
  public abstract class SearchServiceBase<TBusinessModel, TEntity, TSearchModel> : ServiceBase<TBusinessModel, TEntity>
    where TBusinessModel : BaseModel
    where TEntity : BaseEntity
    where TSearchModel : BaseSearchModel
  {

    public abstract Task<IEnumerable<TBusinessModel>> SearchAsync(TSearchModel searchModel);

    protected SearchServiceBase(string typeName, ApplicationContext context, IMapper mapper) : base(typeName, context, mapper)
    {
    }

    public Expression GetSearchExpression(TSearchModel model, ParameterExpression param)
    {
      Expression searchExpression = null;

      foreach (PropertyInfo property in model.GetType().GetProperties())
      {

        Expression propertyExpression = null;
        MemberExpression memberExpression = Expression.Property(param, property.Name);
        ConstantExpression constantExpression = null;

        switch (property.GetValue(model))
        {
          case string stringValue:
            constantExpression = Expression.Constant(stringValue);
            propertyExpression = Expression.Equal(memberExpression, constantExpression);
            break;
          case long intValue:
            ConstantExpression value = Expression.Constant(intValue);
            UnaryExpression convertedExpression = Expression.Convert(value, memberExpression.Type);
            propertyExpression = Expression.Equal(memberExpression, convertedExpression);
            break;
        }

        if (propertyExpression != null)
        {
          if (searchExpression != null)
          {
            searchExpression = Expression.Or(searchExpression, propertyExpression);
          }
          else
          {
            searchExpression = propertyExpression;
          }
        }
      }

      return searchExpression;
    }
  }
}