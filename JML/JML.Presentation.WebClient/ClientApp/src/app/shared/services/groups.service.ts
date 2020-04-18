import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {EndpointMapService} from './endpoint-map.service';
import {Observable} from 'rxjs';
import {Group} from './interfaces';

@Injectable({providedIn: 'root'})
export class GroupsService {
  constructor(private http: HttpClient,
              private endpointMapService: EndpointMapService) {
  }

  public getAll(): Observable<Group[]> {
    return this.http.get<Group[]>(this.endpointMapService.Groups);
  }

  public create(name: string): Observable<Group> {
    return this.http.post<Group>(this.endpointMapService.Groups, { name });
  }
}
