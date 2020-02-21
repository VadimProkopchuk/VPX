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
}
