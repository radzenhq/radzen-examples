using System;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Linq;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.OData.Formatter;
using System.Collections.Concurrent;

namespace Crm.Data
{
    public static class EntityPatch
    {
        public static void Apply(object obj, JObject patch)
        {
            var typeInfo = obj.GetType().GetTypeInfo();

            foreach (var property in patch)
            {
                var propertyInfo = typeInfo.GetProperty(property.Key);

                if (propertyInfo != null && propertyInfo.CanWrite)
                {
                    var computedAttribute = propertyInfo.GetCustomAttributes(typeof(DatabaseGeneratedAttribute), false)
                             .Cast<DatabaseGeneratedAttribute>().FirstOrDefault();
                    if (computedAttribute == null)
                    {
                        var value = property.Value.ToObject(propertyInfo.PropertyType);
                        propertyInfo.SetValue(obj, value);
                    }
                }
            }
        }

        public static IDictionary<string, object> IfMatch(HttpRequest request, Type elementType)
        {
            StringValues ifMatchValues;
            if (request.Headers.TryGetValue("If-Match", out ifMatchValues))
            {
                var etagHeaderValue = EntityTagHeaderValue.Parse(ifMatchValues.SingleOrDefault());
                if (etagHeaderValue != null)
                {
                    var values = request
                        .GetRequestContainer()
                        .GetRequiredService<IETagHandler>()
                        .ParseETag(etagHeaderValue) ?? new Dictionary<string, object>();

                    return elementType
                        .GetProperties()
                        .Where(pi => pi.GetCustomAttributes(typeof(ConcurrencyCheckAttribute), false).Any())
                        .OrderBy(pi => pi.Name)
                        .Select((pi, i) => new { Index = i, Name = pi.Name })
                        .ToDictionary(p => p.Name, p => values[p.Index.ToString()]);
                }
            }

            return null;
        }

        public static IQueryable<T> ApplyTo<T>(HttpRequest request, IQueryable<T> query)
        {
            var ifMatch = EntityPatch.IfMatch(request, query.ElementType);

            if (ifMatch != null)
            {
                var type = query.ElementType;
                var param = Expression.Parameter(type);
                Expression where = null;
                foreach (var item in ifMatch)
                {
                    var property = query.ElementType.GetProperty(item.Key);
                    var conversionType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    var itemValue = (item.Value == null) ? null :
                        Convert.ChangeType(item.Value is DateTimeOffset ? 
                            ((DateTimeOffset)item.Value).UtcDateTime : item.Value, conversionType);

                    var name = Expression.Property(param, item.Key);

                    var value = itemValue != null
                        ? IsNullable(property.PropertyType) ? ToNullable(Expression.Constant(itemValue)) : Expression.Constant(itemValue)
                        : Expression.Constant(value: null);

                    var equal = Expression.Equal(name, value);

                    where = where == null ? equal : Expression.AndAlso(where, equal);
                }

                if (where == null)
                {
                    return query;
                }

                return query.Where(Expression.Lambda<Func<T, bool>>(where, param));
            }

            return query;
        }

        public static Expression ToNullable(Expression expression)
        {
            if (!IsNullable(expression.Type))
            {
                return Expression.Convert(expression, ToNullable(expression.Type));
            }

            return expression;
        }

        public static bool IsNullable(Type clrType)
        {
            if (clrType.IsValueType)
            {
                return clrType.IsGenericType && clrType.GetGenericTypeDefinition() == typeof(Nullable<>);
            }
            else
            {
                return true;
            }
        }

        public static Type ToNullable(Type clrType)
        {
            if (IsNullable(clrType))
            {
                return clrType;
            }
            else
            {
                return typeof(Nullable<>).MakeGenericType(clrType);
            }
        }
    }
}
