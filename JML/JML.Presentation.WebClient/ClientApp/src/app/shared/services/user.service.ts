import {LoginModel, User} from './interfaces';
import {EndpointMapService} from './endpoint-map.service';
import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {AuthService} from './auth.service';
import {tap} from 'rxjs/operators';

@Injectable({providedIn: 'root'})
export class UserService {
  public user: User = null;

  constructor(private endpointMapService: EndpointMapService,
              private httpClient: HttpClient,
              private authService: AuthService) {
  }

  public loadCurrentUserInfo(): void {
    this.httpClient.get<User>(this.endpointMapService.CurrentUserInfoEndpoint)
      .subscribe((user: User) => {
        console.log(user);
        this.user = user;
      }, () => { });
  }

  public login(email: string, password: string) {
    const loginModel: LoginModel = {
      Email: email,
      Password: password
    };

    return this.authService.login(loginModel)
        .pipe(
            tap(this.checkAndLoadInfo.bind(this))
        );
  }

  private checkAndLoadInfo() {
    if (this.authService.isAuthenticated()) {
      this.loadCurrentUserInfo();
    }
  }

  public logout(): void {
    this.user = null;
    this.authService.logout();
  }
}
