using Microsoft.AspNetCore.Blazor;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NorthwindBlazor.App
{
    public class ODataService
    {
        HttpClient client = new HttpClient();

        public async Task<ODataServiceResult<T>> Get<T>(ODataServiceArgs args)
        {
            var queryParameters = new Dictionary<string, object>();

            if (args.Count != null)
            {
                queryParameters.Add("$count", args.Count.Value.ToString().ToLower());
            }

            if (args.Skip != null)
            {
                queryParameters.Add("$skip", args.Skip.Value);
            }

            if (args.Top != null)
            {
                queryParameters.Add("$top", args.Top.Value);
            }

            if (!string.IsNullOrEmpty(args.Orderby))
            {
                queryParameters.Add("$orderBy", args.Orderby);
            }

            if (!string.IsNullOrEmpty(args.Filter))
            {
                queryParameters.Add("$filter", args.Filter);
            }

            if (!string.IsNullOrEmpty(args.Expand))
            {
                queryParameters.Add("$expand", args.Expand);
            }

            if (!string.IsNullOrEmpty(args.Select))
            {
                queryParameters.Add("$select", args.Select);
            }

            if (!string.IsNullOrEmpty(args.Format))
            {
                queryParameters.Add("$format", args.Format);
            }

            var result = await client.GetJsonAsync<dynamic>(string.Format("{0}{1}", args.Url,
                queryParameters.Any() ? "?" + string.Join("&", queryParameters.Select(a => $"{a.Key}={a.Value}")) : ""));

            JObject obj = JObject.FromObject(result);

            return new ODataServiceResult<T>()
            {
                Count = obj["@odata.count"] != null ? obj["@odata.count"].ToObject<int>() : -1,
                Data = obj["value"].Select(i => JsonConvert.DeserializeObject<T>(i.ToString()))
            };
        }


        public async Task<T> GetBy<T>(ODataServiceItemArgs args)
        {
            var queryParameters = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(args.Expand))
            {
                queryParameters.Add("$expand", args.Expand);
            }

            var result = await client.GetJsonAsync<dynamic>(string.Format("{0}{1}", args.Url,
                queryParameters.Any() ? "?" + string.Join("&", queryParameters.Select(a => $"{a.Key}={a.Value}")) : ""));

            var obj = JObject.FromObject(result);

            return JsonConvert.DeserializeObject<T>(obj.ToString());
        }

        public async Task<HttpResponseMessage> Delete(string url)
        {
            return await client.DeleteAsync(url);
        }

        public async Task<HttpResponseMessage> Update<T>(string url, T item)
        {
            return await client.PutAsync(url, 
                new StringContent(JsonConvert.SerializeObject(GetPostData(item), new JsonSerializerSettings
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc
                }), System.Text.Encoding.UTF8, "application/json"));
        }

        public async Task<T> Add<T>(string url, T item)
        {
            return await client.PostJsonAsync<T>(url, GetPostData(item));
        }

        private ExpandoObject GetPostData<T>(T item)
        {
            JObject obj = JObject.FromObject(item);
            foreach (var property in typeof(T).GetProperties())
            {
                if (property.PropertyType.IsGenericType &&
                    typeof(ICollection<>).IsAssignableFrom(property.PropertyType.GetGenericTypeDefinition()))
                {
                    obj.Remove(property.Name);
                }
            }

            return JsonConvert.DeserializeObject<ExpandoObject>(obj.ToString());
        }
    }

    public class ODataServiceResult<T>
    {
        public int Count { get; set; }
        public IEnumerable<T> Data { get; set; }
    }

    public class ODataServiceArgs
    {
        public string Url { get; set; }
        public bool? Count { get; set; }
        public int? Skip { get; set; }
        public int? Top { get; set; }
        public string Orderby { get; set; }
        public string Filter { get; set; }
        public string Expand { get; set; }
        public string Select { get; set; }
        public string Format { get; set; }
    }

    public class ODataServiceItemArgs
    {
        public string Url { get; set; }
        public string Expand { get; set; }
    }
}
