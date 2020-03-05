import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import { Injectable } from '@angular/core';
import {Observable, throwError} from 'rxjs';
import { AuthService } from '../services/auth.service';
import {catchError} from 'rxjs/operators';
import {AlertService} from '../services/alert.service';
import {Router} from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService,
              private alertService: AlertService,
              private router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    req = req.clone({
      setHeaders: {
        'Content-Type': 'application/json; charset=utf-8',
        'Accept': 'application/json',
        'Authorization': `Bearer ${this.authService.token}`,
      },
    });

    return next.handle(req)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          if ([401, 403].indexOf(error.status) !== -1) {
            this.alertService.warning('Истек срок действия токена.');
            this.authService.logout();
            this.router.navigate(['/home'], {
              queryParams: {
                authFailed: true
              }
            }).then(() => {});
          } else {
            this.alertService.danger(error.error);
          }
          return throwError(error);
        })
      );
  }
}
