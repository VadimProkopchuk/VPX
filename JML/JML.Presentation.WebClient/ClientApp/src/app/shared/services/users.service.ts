import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {EndpointMapService} from './endpoint-map.service';
import {CreateUser} from './interfaces';

@Injectable({providedIn: 'root'})
export class UsersService {
  constructor(private http: HttpClient,
              private endpointMapService: EndpointMapService) {
  }

  hasUserByEmail(email: string): Observable<boolean> {
    return this.http.get<boolean>(this.endpointMapService.HasUserByEmail + email);
  }

  register(user: CreateUser): Observable<void> {
    return this.http.post<void>(this.endpointMapService.Register, user);
  }
}
