using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNet.OData.Query;

namespace Crm.Data
{
    public class CsvDataContractSerializerOutputFormatter : TextOutputFormatter
    {
        public CsvDataContractSerializerOutputFormatter()
        {
            SupportedMediaTypes.Add("text/csv");
            SupportedEncodings.Add(Encoding.Unicode);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var query = (IQueryable)context.Object;

            var queryString = context.HttpContext.Request.QueryString;
            var columns = queryString.Value.Contains("$select") ?
                OutputFormatter.GetPropertiesFromSelect(queryString.Value, query.ElementType) :
                    OutputFormatter.GetProperties(query.ElementType);

            var sb = new StringBuilder();

            foreach (var item in query)
            {
                var row = new List<string>();

                foreach (var column in columns)
                {
                    var value = OutputFormatter.GetValue(item, column.Key);

                    row.Add(value != null ? value.ToString() : "");
                }

                sb.AppendLine(string.Join(",", row.ToArray()));
            }

            return context.HttpContext.Response.WriteAsync(
                $"{string.Join(",", columns.Select(c => c.Key))}{System.Environment.NewLine}{sb.ToString()}",
                selectedEncoding,
                context.HttpContext.RequestAborted);
        }
    }
}
