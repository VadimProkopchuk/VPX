import {User} from './interfaces';
import {EndpointMapService} from './endpoint-map.service';
import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {AuthService} from './auth.service';

@Injectable({providedIn: 'root'})
export class UserService {
  private user: User = null;

  constructor(private endpointMapService: EndpointMapService,
              private httpClient: HttpClient,
              private authService: AuthService) {
  }

  public get currentUser() {
    return this.user;
  }

  public loadCurrentUserInfo(): void {
    this.httpClient.get<User>(this.endpointMapService.CurrentUserInfoEndpoint)
      .subscribe((user: User) => {
        console.log(user);
        this.user = user;
      }, () => { });
  }

  public logout(): void {
    this.user = null;
    this.authService.logout();
  }
}
