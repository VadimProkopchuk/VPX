import { environment } from '../../../environments/environment';
import {Injectable} from '@angular/core';

@Injectable({providedIn: 'root'})
export class EndpointMapService {
  public LoginEndpoint = environment.apiEndpoint + 'account/login';
  public CurrentUserInfoEndpoint = environment.apiEndpoint + 'account/current-user';
  public Lectures = environment.apiEndpoint + 'lecture';
  public Tags = environment.apiEndpoint + 'tag';
  public Users = environment.apiEndpoint + 'users';
  public HasUserByEmail = this.Users + '/hasUserByEmail/';
  public Register = this.Users + '/register';
}
