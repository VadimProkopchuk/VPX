import { environment } from '../../../environments/environment';
import {Injectable} from '@angular/core';

@Injectable({providedIn: 'root'})
export class EndpointMapService {
  get LoginEndpoint() {
    return environment.apiEndpoint + 'account/login';
  }
}
