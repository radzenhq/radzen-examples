
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Radzen;

namespace CRMBlazorWasmRBS.Client
{
    public partial class RadzenCRMService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUri;
        private readonly NavigationManager navigationManager;

        public RadzenCRMService(NavigationManager navigationManager, HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            this.navigationManager = navigationManager;
            this.baseUri = new Uri($"{navigationManager.BaseUri}odata/RadzenCRM/");
        }


        public async System.Threading.Tasks.Task ExportContactsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radzencrm/contacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radzencrm/contacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportContactsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radzencrm/contacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radzencrm/contacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetContacts(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact>> GetContacts(Query query)
        {
            return await GetContacts(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact>> GetContacts(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Contacts");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContacts(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact>>(response);
        }

        partial void OnCreateContact(HttpRequestMessage requestMessage);

        public async Task<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact> CreateContact(CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact contact = default(CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact))
        {
            var uri = new Uri(baseUri, $"Contacts");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contact), Encoding.UTF8, "application/json");

            OnCreateContact(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact>(response);
        }

        partial void OnDeleteContact(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteContact(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Contacts({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteContact(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetContactById(HttpRequestMessage requestMessage);

        public async Task<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact> GetContactById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Contacts({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContactById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact>(response);
        }

        partial void OnUpdateContact(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateContact(int id = default(int), CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact contact = default(CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact))
        {
            var uri = new Uri(baseUri, $"Contacts({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contact), Encoding.UTF8, "application/json");

            OnUpdateContact(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportOpportunitiesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radzencrm/opportunities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radzencrm/opportunities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportOpportunitiesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radzencrm/opportunities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radzencrm/opportunities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetOpportunities(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Opportunity>> GetOpportunities(Query query)
        {
            return await GetOpportunities(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Opportunity>> GetOpportunities(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Opportunities");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetOpportunities(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Opportunity>>(response);
        }

        partial void OnCreateOpportunity(HttpRequestMessage requestMessage);

        public async Task<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Opportunity> CreateOpportunity(CRMBlazorWasmRBS.Server.Models.RadzenCRM.Opportunity opportunity = default(CRMBlazorWasmRBS.Server.Models.RadzenCRM.Opportunity))
        {
            var uri = new Uri(baseUri, $"Opportunities");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(opportunity), Encoding.UTF8, "application/json");

            OnCreateOpportunity(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Opportunity>(response);
        }

        partial void OnDeleteOpportunity(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteOpportunity(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Opportunities({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteOpportunity(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetOpportunityById(HttpRequestMessage requestMessage);

        public async Task<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Opportunity> GetOpportunityById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Opportunities({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetOpportunityById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Opportunity>(response);
        }

        partial void OnUpdateOpportunity(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateOpportunity(int id = default(int), CRMBlazorWasmRBS.Server.Models.RadzenCRM.Opportunity opportunity = default(CRMBlazorWasmRBS.Server.Models.RadzenCRM.Opportunity))
        {
            var uri = new Uri(baseUri, $"Opportunities({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(opportunity), Encoding.UTF8, "application/json");

            OnUpdateOpportunity(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportOpportunityStatusesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radzencrm/opportunitystatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radzencrm/opportunitystatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportOpportunityStatusesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radzencrm/opportunitystatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radzencrm/opportunitystatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetOpportunityStatuses(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus>> GetOpportunityStatuses(Query query)
        {
            return await GetOpportunityStatuses(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus>> GetOpportunityStatuses(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"OpportunityStatuses");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetOpportunityStatuses(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus>>(response);
        }

        partial void OnCreateOpportunityStatus(HttpRequestMessage requestMessage);

        public async Task<CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus> CreateOpportunityStatus(CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus opportunityStatus = default(CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus))
        {
            var uri = new Uri(baseUri, $"OpportunityStatuses");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(opportunityStatus), Encoding.UTF8, "application/json");

            OnCreateOpportunityStatus(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus>(response);
        }

        partial void OnDeleteOpportunityStatus(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteOpportunityStatus(int id = default(int))
        {
            var uri = new Uri(baseUri, $"OpportunityStatuses({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteOpportunityStatus(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetOpportunityStatusById(HttpRequestMessage requestMessage);

        public async Task<CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus> GetOpportunityStatusById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"OpportunityStatuses({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetOpportunityStatusById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus>(response);
        }

        partial void OnUpdateOpportunityStatus(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateOpportunityStatus(int id = default(int), CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus opportunityStatus = default(CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus))
        {
            var uri = new Uri(baseUri, $"OpportunityStatuses({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(opportunityStatus), Encoding.UTF8, "application/json");

            OnUpdateOpportunityStatus(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTasksToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radzencrm/tasks/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radzencrm/tasks/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTasksToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radzencrm/tasks/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radzencrm/tasks/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTasks(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task>> GetTasks(Query query)
        {
            return await GetTasks(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task>> GetTasks(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Tasks");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTasks(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task>>(response);
        }

        partial void OnCreateTask(HttpRequestMessage requestMessage);

        public async Task<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task> CreateTask(CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task task = default(CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task))
        {
            var uri = new Uri(baseUri, $"Tasks");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(task), Encoding.UTF8, "application/json");

            OnCreateTask(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task>(response);
        }

        partial void OnDeleteTask(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTask(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Tasks({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTask(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTaskById(HttpRequestMessage requestMessage);

        public async Task<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task> GetTaskById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Tasks({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTaskById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task>(response);
        }

        partial void OnUpdateTask(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTask(int id = default(int), CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task task = default(CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task))
        {
            var uri = new Uri(baseUri, $"Tasks({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(task), Encoding.UTF8, "application/json");

            OnUpdateTask(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTaskStatusesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radzencrm/taskstatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radzencrm/taskstatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTaskStatusesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radzencrm/taskstatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radzencrm/taskstatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTaskStatuses(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus>> GetTaskStatuses(Query query)
        {
            return await GetTaskStatuses(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus>> GetTaskStatuses(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"TaskStatuses");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTaskStatuses(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus>>(response);
        }

        partial void OnCreateTaskStatus(HttpRequestMessage requestMessage);

        public async Task<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus> CreateTaskStatus(CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus taskStatus = default(CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus))
        {
            var uri = new Uri(baseUri, $"TaskStatuses");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(taskStatus), Encoding.UTF8, "application/json");

            OnCreateTaskStatus(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus>(response);
        }

        partial void OnDeleteTaskStatus(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTaskStatus(int id = default(int))
        {
            var uri = new Uri(baseUri, $"TaskStatuses({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTaskStatus(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTaskStatusById(HttpRequestMessage requestMessage);

        public async Task<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus> GetTaskStatusById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"TaskStatuses({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTaskStatusById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus>(response);
        }

        partial void OnUpdateTaskStatus(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTaskStatus(int id = default(int), CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus taskStatus = default(CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus))
        {
            var uri = new Uri(baseUri, $"TaskStatuses({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(taskStatus), Encoding.UTF8, "application/json");

            OnUpdateTaskStatus(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTaskTypesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radzencrm/tasktypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radzencrm/tasktypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTaskTypesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radzencrm/tasktypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radzencrm/tasktypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTaskTypes(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType>> GetTaskTypes(Query query)
        {
            return await GetTaskTypes(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType>> GetTaskTypes(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"TaskTypes");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTaskTypes(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType>>(response);
        }

        partial void OnCreateTaskType(HttpRequestMessage requestMessage);

        public async Task<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType> CreateTaskType(CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType taskType = default(CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType))
        {
            var uri = new Uri(baseUri, $"TaskTypes");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(taskType), Encoding.UTF8, "application/json");

            OnCreateTaskType(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType>(response);
        }

        partial void OnDeleteTaskType(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTaskType(int id = default(int))
        {
            var uri = new Uri(baseUri, $"TaskTypes({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTaskType(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTaskTypeById(HttpRequestMessage requestMessage);

        public async Task<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType> GetTaskTypeById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"TaskTypes({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTaskTypeById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType>(response);
        }

        partial void OnUpdateTaskType(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTaskType(int id = default(int), CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType taskType = default(CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType))
        {
            var uri = new Uri(baseUri, $"TaskTypes({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(taskType), Encoding.UTF8, "application/json");

            OnUpdateTaskType(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
    }
}