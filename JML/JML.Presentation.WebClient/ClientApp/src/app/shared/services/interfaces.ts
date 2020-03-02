import {Role} from '../models/role';

export interface TokenPair {
  token: string;
  expiredAt: Date;
}

export interface LoginModel {
  Email: string;
  Password: string;
}

export interface User {
  id: string;
  firstName: string;
  lastName: string;
  groupName: string;
  roles: Array<string>;
  enumRoles: Array<Role>;
}

export interface Lecture {
  id?: string;
  name: string;
  url: string;
  content: string;
  preview?: string;
  tags: Array<Tag>;
  createdAt?: Date;
  modifiedAt?: Date;
}

export interface Tag {
  display: String;
  value?: String;
}
