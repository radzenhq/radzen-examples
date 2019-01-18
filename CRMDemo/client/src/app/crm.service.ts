import { Injectable } from '@angular/core';
import { Location } from '@angular/common';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { ConfigService } from './config.service';
import { ODataClient } from './odata-client';
import * as models from './crm.model';

@Injectable()
export class CrmService {
  odata: ODataClient;
  basePath: string;

  constructor(private http: HttpClient, private config: ConfigService) {
    this.basePath = config.get('crm');
    this.odata = new ODataClient(this.http, this.basePath, { legacy: false, withCredentials: true });
  }

  getContacts(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/Contacts`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createContact(contact: models.Contact | null) {
    return this.odata.post(`/Contacts`, contact);
  }

  deleteContact(id: number | null) {
    return this.odata.delete(`/Contacts(${id})`, item => !(item.Id == id));
  }

  getContactById(id: number | null) {
    return this.odata.get(`/Contacts(${id})`);
  }

  updateContact(id: number | null, contact: models.Contact | null) {
    return this.odata.patch(`/Contacts(${id})`, contact, item => item.Id == id);
  }

  getOpportunities(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/Opportunities`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createOpportunity(opportunity: models.Opportunity | null) {
    return this.odata.post(`/Opportunities`, opportunity);
  }

  deleteOpportunity(id: number | null) {
    return this.odata.delete(`/Opportunities(${id})`, item => !(item.Id == id));
  }

  getOpportunityById(id: number | null) {
    return this.odata.get(`/Opportunities(${id})`);
  }

  updateOpportunity(id: number | null, opportunity: models.Opportunity | null) {
    return this.odata.patch(`/Opportunities(${id})`, opportunity, item => item.Id == id);
  }

  getOpportunityStatuses(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/OpportunityStatuses`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createOpportunityStatus(opportunityStatus: models.OpportunityStatus | null) {
    return this.odata.post(`/OpportunityStatuses`, opportunityStatus);
  }

  deleteOpportunityStatus(id: number | null) {
    return this.odata.delete(`/OpportunityStatuses(${id})`, item => !(item.Id == id));
  }

  getOpportunityStatusById(id: number | null) {
    return this.odata.get(`/OpportunityStatuses(${id})`);
  }

  updateOpportunityStatus(id: number | null, opportunityStatus: models.OpportunityStatus | null) {
    return this.odata.patch(`/OpportunityStatuses(${id})`, opportunityStatus, item => item.Id == id);
  }

  getTasks(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/Tasks`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createTask(task: models.Task | null) {
    return this.odata.post(`/Tasks`, task);
  }

  deleteTask(id: number | null) {
    return this.odata.delete(`/Tasks(${id})`, item => !(item.Id == id));
  }

  getTaskById(id: number | null) {
    return this.odata.get(`/Tasks(${id})`);
  }

  updateTask(id: number | null, task: models.Task | null) {
    return this.odata.patch(`/Tasks(${id})`, task, item => item.Id == id);
  }

  getTaskStatuses(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/TaskStatuses`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createTaskStatus(taskStatus: models.TaskStatus | null) {
    return this.odata.post(`/TaskStatuses`, taskStatus);
  }

  deleteTaskStatus(id: number | null) {
    return this.odata.delete(`/TaskStatuses(${id})`, item => !(item.Id == id));
  }

  getTaskStatusById(id: number | null) {
    return this.odata.get(`/TaskStatuses(${id})`);
  }

  updateTaskStatus(id: number | null, taskStatus: models.TaskStatus | null) {
    return this.odata.patch(`/TaskStatuses(${id})`, taskStatus, item => item.Id == id);
  }

  getTaskTypes(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/TaskTypes`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createTaskType(taskType: models.TaskType | null) {
    return this.odata.post(`/TaskTypes`, taskType);
  }

  deleteTaskType(id: number | null) {
    return this.odata.delete(`/TaskTypes(${id})`, item => !(item.Id == id));
  }

  getTaskTypeById(id: number | null) {
    return this.odata.get(`/TaskTypes(${id})`);
  }

  updateTaskType(id: number | null, taskType: models.TaskType | null) {
    return this.odata.patch(`/TaskTypes(${id})`, taskType, item => item.Id == id);
  }
}
