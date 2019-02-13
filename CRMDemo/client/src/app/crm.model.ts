export interface Contact {
  Id: number;
  Email: string;
  Company: string;
  LastName: string;
  FirstName: string;
  Phone: string;
}

export interface Opportunity {
  Id: number;
  Amount: number;
  UserId: string;
  ContactId: number;
  StatusId: number;
  CloseDate: string;
  Name: string;
}

export interface OpportunityStatus {
  Id: number;
  Name: string;
}

export interface Task {
  Id: number;
  Title: string;
  OpportunityId: number;
  DueDate: string;
  TypeId: number;
  StatusId: number;
}

export interface TaskStatus {
  Id: number;
  Name: string;
}

export interface TaskType {
  Id: number;
  Name: string;
}
