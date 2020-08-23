import { environment } from '../../../environments/environment';
import {Injectable} from '@angular/core';

@Injectable({providedIn: 'root'})
export class EndpointMapService {
  public LoginEndpoint = environment.apiEndpoint + 'account/login';
  public Register = environment.apiEndpoint + 'account/register';
  public Verify = environment.apiEndpoint + 'account/verify';
  public RestoreAccess = environment.apiEndpoint + 'account/restore';

  public Users = environment.apiEndpoint + 'users';
  public HasUserByEmail = this.Users + '/hasUserByEmail/';
  public CurrentUserInfoEndpoint = environment.apiEndpoint + 'users/current';

  public Lectures = environment.apiEndpoint + 'lecture';

  public Tags = environment.apiEndpoint + 'tag';

  public Groups = environment.apiEndpoint + 'groups';

  public TestTemplates = environment.apiEndpoint + 'TestTemplates';

  public Literature = environment.apiEndpoint + 'Literature';
}
