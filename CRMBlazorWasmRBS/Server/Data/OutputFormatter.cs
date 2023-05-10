using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.OData.Query.Wrapper;


namespace CRMBlazorWasmRBS.Server.Data
{
    public static class OutputFormatter
    {
        public static object GetValue(object target, string name)
        {
            var selectExpandWrapper = target as ISelectExpandWrapper;

            return selectExpandWrapper != null ?
                selectExpandWrapper.ToDictionary()[name] :
                    target.GetType().GetProperty(name).GetValue(target);
        }

        public static IEnumerable<KeyValuePair<string, Type>> GetPropertiesFromSelect(string queryString, Type type)
        {
            var select = HttpUtility.ParseQueryString(queryString)["$select"];
            var selectedPropertyNames = select != null ? select.Split(",") : new string[0];

            var elementType = typeof(ISelectExpandWrapper).IsAssignableFrom(type) ? type.GenericTypeArguments.First() : type;

            return GetProperties(elementType).Where(p => selectedPropertyNames.Contains(p.Key));
        }

        public static IEnumerable<KeyValuePair<string, Type>> GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && OutputFormatter.IsSimpleType(p.PropertyType)).Select(p => new KeyValuePair<string, Type>(p.Name, p.PropertyType));
        }

        public static bool IsSimpleType(Type type)
        {
            var underlyingType = type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(Nullable<>) ?
                Nullable.GetUnderlyingType(type) : type;

            if(underlyingType == typeof(System.Guid) || underlyingType == typeof(System.DateTimeOffset))
                return true;

            var typeCode = Type.GetTypeCode(underlyingType);

            switch (typeCode)
            {
                case TypeCode.Boolean:
                case TypeCode.Byte:
                case TypeCode.Char:
                case TypeCode.DateTime:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.String:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                default:
                    return false;
            }
        }
    }
}
