export interface Role {
  Id: number;
  Name: string;
}

export interface User {
  Id: number;
  Email: string;
  PasswordHash: string;
}

export interface UserRole {
  UserId: number;
  RoleId: number;
}
