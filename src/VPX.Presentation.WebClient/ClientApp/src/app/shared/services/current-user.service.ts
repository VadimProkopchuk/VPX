import {LoginModel, User} from './interfaces';
import {EndpointMapService} from './endpoint-map.service';
import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {AuthService} from './auth.service';
import {tap} from 'rxjs/operators';
import {AlertService} from './alert.service';
import {Role} from '../models/role';

@Injectable({providedIn: 'root'})
export class CurrentUserService {
  constructor(private endpointMapService: EndpointMapService,
              private httpClient: HttpClient,
              private authService: AuthService,
              private alertService: AlertService) {
  }

  public get user(): User {
    const userJson = localStorage.getItem('user');
    if (userJson) {
      return JSON.parse(userJson);
    }
    return null;
  }

  public set user(value: User) {
    if (value) {
      localStorage.setItem('user', JSON.stringify(value));
    } else {
      localStorage.removeItem('user');
    }
  }

  public loadCurrentUserInfo(): void {
    this.alertService.info('Загружается информация о пользователе...');
    this.httpClient.get<User>(this.endpointMapService.CurrentUserInfoEndpoint)
      .subscribe((user: User) => {
        this.user = user;
        this.alertService.success(`Добро пожаловать, ${user.firstName} ${user.lastName}!`);
      }, () => {
        this.alertService.danger('Ошибка загрузки информации о пользователе.');
      });
  }

  public login(email: string, password: string) {
    const loginModel: LoginModel = {
      Email: email,
      Password: password
    };

    this.authService.logout();

    return this.authService.login(loginModel)
        .pipe(
            tap(this.checkAndLoadInfo.bind(this))
        );
  }

  private checkAndLoadInfo() {
    if (this.authService.isAuthenticated()) {
      this.alertService.info('Авторизация успешно завершена.');
      this.loadCurrentUserInfo();
    }
  }

  public logout(): void {
    this.user = null;
    this.alertService.warning('Завершается текущая сессия...');
    this.authService.logout();
    this.alertService.success('Сессия завершена.');
  }

  public isTeacher(): boolean {
    return this.user.roles.map(x => x.value).includes(Role.Teacher);
  }

  public isAdmin(): boolean {
    return this.user.roles.map(x => x.value).includes(Role.Admin);
  }
}
