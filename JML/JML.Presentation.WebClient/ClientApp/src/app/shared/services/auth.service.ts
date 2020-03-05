import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { LoginModel, TokenPair } from './interfaces';
import {catchError, tap} from 'rxjs/operators';
import {AlertService} from './alert.service';
import {EndpointMapService} from './endpoint-map.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(private http: HttpClient,
              private alertService: AlertService,
              private endpointMapService: EndpointMapService) {}

  get token(): string {
    const expDate = new Date(localStorage.getItem('token-exp'));
    if (new Date() > expDate) {
      this.logout();
      return null;
    }
    return localStorage.getItem('token');
  }

  private static setToken(tokenPair: TokenPair | null) {
    if (tokenPair) {
      localStorage.setItem('token', tokenPair.token);
      localStorage.setItem('token-exp', tokenPair.expiredAt.toString());
    } else {
      localStorage.clear();
    }
  }

  private handleError(error: HttpErrorResponse) {
    this.alertService.danger(error.error);
    return throwError(error);
  }

  login(loginModel: LoginModel): Observable<any> {
    return this.http.post<TokenPair>(this.endpointMapService.LoginEndpoint, loginModel)
      .pipe(
        tap(AuthService.setToken),
        catchError(this.handleError.bind(this))
      );
  }

  logout() {
    AuthService.setToken(null);
  }

  isAuthenticated(): boolean {
    return this.token != null;
  }
}
