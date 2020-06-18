
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using BlazorCrmWasm.Models.Crm;

namespace BlazorCrmWasm
{
    public partial class CrmService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUri;
        private readonly NavigationManager navigationManager;
        public CrmService(NavigationManager navigationManager, HttpClient httpClient)
        {
            this.httpClient = httpClient;
            
            this.navigationManager = navigationManager;
            this.baseUri = new Uri($"{navigationManager.BaseUri}odata/CRM/");
        }

        public async System.Threading.Tasks.Task ExportContactsToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/contacts/excel") : "export/crm/contacts/excel", true);
        }

        public async System.Threading.Tasks.Task ExportContactsToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/contacts/csv") : "export/crm/contacts/csv", true);
        }

        partial void OnGetContacts(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<Models.Crm.Contact>> GetContacts(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"Contacts");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContacts(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<Models.Crm.Contact>>();
        }
        partial void OnCreateContact(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Models.Crm.Contact> CreateContact(Models.Crm.Contact contact = default(Models.Crm.Contact))
        {
            var uri = new Uri(baseUri, $"Contacts");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(contact), Encoding.UTF8, "application/json");

            OnCreateContact(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Models.Crm.Contact>();
        }

        public async System.Threading.Tasks.Task ExportOpportunitiesToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/opportunities/excel") : "export/crm/opportunities/excel", true);
        }

        public async System.Threading.Tasks.Task ExportOpportunitiesToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/opportunities/csv") : "export/crm/opportunities/csv", true);
        }

        partial void OnGetOpportunities(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<Models.Crm.Opportunity>> GetOpportunities(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"Opportunities");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetOpportunities(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<Models.Crm.Opportunity>>();
        }
        partial void OnCreateOpportunity(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Models.Crm.Opportunity> CreateOpportunity(Models.Crm.Opportunity opportunity = default(Models.Crm.Opportunity))
        {
            var uri = new Uri(baseUri, $"Opportunities");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(opportunity), Encoding.UTF8, "application/json");

            OnCreateOpportunity(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Models.Crm.Opportunity>();
        }

        public async System.Threading.Tasks.Task ExportOpportunityStatusesToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/opportunitystatuses/excel") : "export/crm/opportunitystatuses/excel", true);
        }

        public async System.Threading.Tasks.Task ExportOpportunityStatusesToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/opportunitystatuses/csv") : "export/crm/opportunitystatuses/csv", true);
        }

        partial void OnGetOpportunityStatuses(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<Models.Crm.OpportunityStatus>> GetOpportunityStatuses(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"OpportunityStatuses");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetOpportunityStatuses(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<Models.Crm.OpportunityStatus>>();
        }
        partial void OnCreateOpportunityStatus(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Models.Crm.OpportunityStatus> CreateOpportunityStatus(Models.Crm.OpportunityStatus opportunityStatus = default(Models.Crm.OpportunityStatus))
        {
            var uri = new Uri(baseUri, $"OpportunityStatuses");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(opportunityStatus), Encoding.UTF8, "application/json");

            OnCreateOpportunityStatus(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Models.Crm.OpportunityStatus>();
        }

        public async System.Threading.Tasks.Task ExportTasksToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/tasks/excel") : "export/crm/tasks/excel", true);
        }

        public async System.Threading.Tasks.Task ExportTasksToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/tasks/csv") : "export/crm/tasks/csv", true);
        }

        partial void OnGetTasks(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<Models.Crm.Task>> GetTasks(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"Tasks");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTasks(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<Models.Crm.Task>>();
        }
        partial void OnCreateTask(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Models.Crm.Task> CreateTask(Models.Crm.Task task = default(Models.Crm.Task))
        {
            var uri = new Uri(baseUri, $"Tasks");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(task), Encoding.UTF8, "application/json");

            OnCreateTask(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Models.Crm.Task>();
        }

        public async System.Threading.Tasks.Task ExportTaskStatusesToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/taskstatuses/excel") : "export/crm/taskstatuses/excel", true);
        }

        public async System.Threading.Tasks.Task ExportTaskStatusesToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/taskstatuses/csv") : "export/crm/taskstatuses/csv", true);
        }

        partial void OnGetTaskStatuses(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<Models.Crm.TaskStatus>> GetTaskStatuses(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"TaskStatuses");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTaskStatuses(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<Models.Crm.TaskStatus>>();
        }
        partial void OnCreateTaskStatus(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Models.Crm.TaskStatus> CreateTaskStatus(Models.Crm.TaskStatus taskStatus = default(Models.Crm.TaskStatus))
        {
            var uri = new Uri(baseUri, $"TaskStatuses");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(taskStatus), Encoding.UTF8, "application/json");

            OnCreateTaskStatus(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Models.Crm.TaskStatus>();
        }

        public async System.Threading.Tasks.Task ExportTaskTypesToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/tasktypes/excel") : "export/crm/tasktypes/excel", true);
        }

        public async System.Threading.Tasks.Task ExportTaskTypesToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/tasktypes/csv") : "export/crm/tasktypes/csv", true);
        }

        partial void OnGetTaskTypes(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<Models.Crm.TaskType>> GetTaskTypes(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"TaskTypes");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTaskTypes(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<Models.Crm.TaskType>>();
        }
        partial void OnCreateTaskType(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Models.Crm.TaskType> CreateTaskType(Models.Crm.TaskType taskType = default(Models.Crm.TaskType))
        {
            var uri = new Uri(baseUri, $"TaskTypes");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(taskType), Encoding.UTF8, "application/json");

            OnCreateTaskType(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Models.Crm.TaskType>();
        }
        partial void OnDeleteContact(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteContact(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Contacts({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteContact(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetContactById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Models.Crm.Contact> GetContactById(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Contacts({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContactById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Models.Crm.Contact>();
        }
        partial void OnUpdateContact(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateContact(int? id = default(int?), Models.Crm.Contact contact = default(Models.Crm.Contact))
        {
            var uri = new Uri(baseUri, $"Contacts({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(contact), Encoding.UTF8, "application/json");

            OnUpdateContact(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteOpportunity(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteOpportunity(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Opportunities({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteOpportunity(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetOpportunityById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Models.Crm.Opportunity> GetOpportunityById(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Opportunities({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetOpportunityById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Models.Crm.Opportunity>();
        }
        partial void OnUpdateOpportunity(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateOpportunity(int? id = default(int?), Models.Crm.Opportunity opportunity = default(Models.Crm.Opportunity))
        {
            var uri = new Uri(baseUri, $"Opportunities({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(opportunity), Encoding.UTF8, "application/json");

            OnUpdateOpportunity(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteOpportunityStatus(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteOpportunityStatus(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"OpportunityStatuses({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteOpportunityStatus(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetOpportunityStatusById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Models.Crm.OpportunityStatus> GetOpportunityStatusById(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"OpportunityStatuses({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetOpportunityStatusById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Models.Crm.OpportunityStatus>();
        }
        partial void OnUpdateOpportunityStatus(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateOpportunityStatus(int? id = default(int?), Models.Crm.OpportunityStatus opportunityStatus = default(Models.Crm.OpportunityStatus))
        {
            var uri = new Uri(baseUri, $"OpportunityStatuses({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(opportunityStatus), Encoding.UTF8, "application/json");

            OnUpdateOpportunityStatus(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteTask(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteTask(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Tasks({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTask(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetTaskById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Models.Crm.Task> GetTaskById(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Tasks({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTaskById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Models.Crm.Task>();
        }
        partial void OnUpdateTask(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateTask(int? id = default(int?), Models.Crm.Task task = default(Models.Crm.Task))
        {
            var uri = new Uri(baseUri, $"Tasks({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(task), Encoding.UTF8, "application/json");

            OnUpdateTask(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteTaskStatus(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteTaskStatus(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"TaskStatuses({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTaskStatus(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetTaskStatusById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Models.Crm.TaskStatus> GetTaskStatusById(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"TaskStatuses({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTaskStatusById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Models.Crm.TaskStatus>();
        }
        partial void OnUpdateTaskStatus(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateTaskStatus(int? id = default(int?), Models.Crm.TaskStatus taskStatus = default(Models.Crm.TaskStatus))
        {
            var uri = new Uri(baseUri, $"TaskStatuses({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(taskStatus), Encoding.UTF8, "application/json");

            OnUpdateTaskStatus(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteTaskType(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteTaskType(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"TaskTypes({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTaskType(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetTaskTypeById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Models.Crm.TaskType> GetTaskTypeById(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"TaskTypes({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTaskTypeById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Models.Crm.TaskType>();
        }
        partial void OnUpdateTaskType(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateTaskType(int? id = default(int?), Models.Crm.TaskType taskType = default(Models.Crm.TaskType))
        {
            var uri = new Uri(baseUri, $"TaskTypes({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(taskType), Encoding.UTF8, "application/json");

            OnUpdateTaskType(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
    }
}
