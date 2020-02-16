export interface TokenPair {
  token: string;
  expiredAt: Date;
}

export interface LoginModel {
  Email: string;
  Password: string;
}

export type AlertType = 'success' | 'warning' | 'danger';
export interface Alert {
  type: AlertType;
  text: string;
}
