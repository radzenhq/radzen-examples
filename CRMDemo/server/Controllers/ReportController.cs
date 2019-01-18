using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers
{
    public partial class ReportController : Controller
    {

        [HttpGet("/__ssrsreport")]
        public async Task Get(string url)
        {
            if (!String.IsNullOrEmpty(url))
            {
                using (var httpClient = CreateHttpClient())
                {
                    var responseMessage = await ForwardRequest(httpClient, Request, url);

                    CopyResponseHeaders(responseMessage, Response);

                    WriteResponse(Request, url, responseMessage, Response, false);
                }
            }
        }

        [Route("/ssrsproxy/{*url}")]
        public async Task Proxy()
        {
            var urlToReplace = String.Format("{0}://{1}{2}/{3}/", Request.Scheme, Request.Host.Value, Request.PathBase, "ssrsproxy");
            var requestedUrl = Request.GetDisplayUrl().Replace(urlToReplace, "", StringComparison.InvariantCultureIgnoreCase);
            var reportServerIndex = requestedUrl.IndexOf("/ReportServer");
            var reportUrlParts = requestedUrl.Substring(0, reportServerIndex).Split('/');

            var url = String.Format("{0}://{1}:{2}{3}", reportUrlParts[0], reportUrlParts[1], reportUrlParts[2],
                requestedUrl.Substring(reportServerIndex, requestedUrl.Length - reportServerIndex));

            using (var httpClient = CreateHttpClient())
            {
                var responseMessage = await ForwardRequest(httpClient, Request, url);

                CopyResponseHeaders(responseMessage, Response);

                if (Request.Method == "POST")
                {
                    WriteResponse(Request, url, responseMessage, Response, true);
                }
                else
                {
                    if (responseMessage.Content.Headers.ContentType.MediaType == "text/html")
                    {
                        WriteResponse(Request, url, responseMessage, Response, false);
                    }
                    else
                    {
                        using (var responseStream = await responseMessage.Content.ReadAsStreamAsync())
                        {
                            await responseStream.CopyToAsync(Response.Body, 81920, HttpContext.RequestAborted);
                        }
                    }
                }
            }
        }

        partial void OnHttpClientHandlerCreate(ref HttpClientHandler handler);

        private HttpClient CreateHttpClient()
        {
            var httpClientHandler = new HttpClientHandler();

            OnHttpClientHandlerCreate(ref httpClientHandler);

            return new HttpClient(httpClientHandler);
        }

        partial void OnReportRequest(ref HttpRequestMessage requestMessage);

        async Task<HttpResponseMessage> ForwardRequest(HttpClient httpClient, HttpRequest currentReqest, string url)
        {
            var proxyRequestMessage = new HttpRequestMessage(new HttpMethod(currentReqest.Method), url);

            foreach (var header in currentReqest.Headers)
            {
                if(header.Key != "Host")
                {
                    proxyRequestMessage.Headers.TryAddWithoutValidation(header.Key, new string[] { header.Value });
                }
            }

            this.OnReportRequest(ref proxyRequestMessage);

            if (currentReqest.Method == "POST")
            {
                using (var stream = new MemoryStream())
                {
                    currentReqest.Body.CopyTo(stream);
                    stream.Position = 0;

                    string body = new StreamReader(stream).ReadToEnd();
                    proxyRequestMessage.Content = new StringContent(body);

                    if (body.IndexOf("AjaxScriptManager") != -1)
                    {
                        proxyRequestMessage.Content.Headers.Remove("Content-Type");
                        proxyRequestMessage.Content.Headers.Add("Content-Type", new string[] { currentReqest.ContentType });
                    }
                }
            }

            return await httpClient.SendAsync(proxyRequestMessage);
        }

        static void CopyResponseHeaders(HttpResponseMessage responseMessage, HttpResponse response)
        {
            response.StatusCode = (int)responseMessage.StatusCode;
            foreach (var header in responseMessage.Headers)
            {
                response.Headers[header.Key] = header.Value.ToArray();
            }

            foreach (var header in responseMessage.Content.Headers)
            {
                response.Headers[header.Key] = header.Value.ToArray();
            }

            response.Headers.Remove("transfer-encoding");
        }

        static async void WriteResponse(HttpRequest currentReqest, string url, HttpResponseMessage responseMessage, HttpResponse response, bool isAjax)
        {
            var result = await responseMessage.Content.ReadAsStringAsync();

            var reportUri = new Uri(url);
            var proxyUrl = String.Format("{0}://{1}{2}/ssrsproxy/{3}/{4}/{5}", currentReqest.Scheme, currentReqest.Host.Value, currentReqest.PathBase,
                reportUri.Scheme, reportUri.Host, reportUri.Port);

            if (isAjax && result.IndexOf("|") != -1)
            {
                var builder = new StringBuilder();

                var delimiterIndex = 0;
                var length = 0; 
                var index = 0;

                var type = "";
                var id = "";
                var content = "";

                while (index < result.Length) {
                    delimiterIndex = result.IndexOf("|", index);
                    if (delimiterIndex == -1) {
                        break;
                    }
                    length = int.Parse(result.Substring(index, delimiterIndex - index));
                    if ((length % 1) != 0) {
                        break;
                    }
                    index = delimiterIndex + 1;
                    delimiterIndex = result.IndexOf("|", index);
                    if (delimiterIndex == -1) {
                        break;
                    }
                    type = result.Substring(index, delimiterIndex - index);
                    index = delimiterIndex + 1;
                    delimiterIndex = result.IndexOf("|", index);
                    if (delimiterIndex == -1) {
                        break;
                    }
                    id = result.Substring(index, delimiterIndex - index);
                    index = delimiterIndex + 1;
                    if ((index + length) >= result.Length) {
                        break;
                    }
                    content = result.Substring(index, length);
                    index += length;
                    if (result.Substring(index, 1) != "|") {
                        break;
                    }
                    index++;

                    content = content.Replace("/ReportServer/", String.Format("{0}/ReportServer/", proxyUrl), StringComparison.InvariantCultureIgnoreCase);
                    if(content.Contains("./ReportViewer.aspx")) 
                    {
                        content = content.Replace("./ReportViewer.aspx", String.Format("{0}/ReportServer/Pages/ReportViewer.aspx", proxyUrl), StringComparison.InvariantCultureIgnoreCase);
                    } else 
                    {
                        content = content.Replace("ReportViewer.aspx", String.Format("{0}/ReportServer/Pages/ReportViewer.aspx", proxyUrl), StringComparison.InvariantCultureIgnoreCase);
                    }

                    builder.Append(String.Format("{0}|{1}|{2}|{3}|", content.Length, type, id, content));
                }

                result = builder.ToString();
            } else {
                result = result.Replace("/ReportServer/", String.Format("{0}/ReportServer/", proxyUrl), StringComparison.InvariantCultureIgnoreCase);
                
                if(result.Contains("./ReportViewer.aspx"))
                {
                    result = result.Replace("./ReportViewer.aspx", String.Format("{0}/ReportServer/Pages/ReportViewer.aspx", proxyUrl), StringComparison.InvariantCultureIgnoreCase);
                } else 
                {
                    result = result.Replace("ReportViewer.aspx", String.Format("{0}/ReportServer/Pages/ReportViewer.aspx", proxyUrl), StringComparison.InvariantCultureIgnoreCase);
                }
            }

            response.Headers.Remove("Content-Length");
            response.Headers.Add("Content-Length", new string[] { System.Text.Encoding.UTF8.GetByteCount(result).ToString() });

            await response.WriteAsync(result);
        }
    }
}
