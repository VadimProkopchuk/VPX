import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {EndpointMapService} from './endpoint-map.service';
import {Observable} from 'rxjs';
import {Group, UpdateGroup} from './interfaces';

@Injectable({providedIn: 'root'})
export class GroupsService {
  constructor(private http: HttpClient,
              private endpointMapService: EndpointMapService) {
  }

  public get(id: string): Observable<Group> {
    return this.http.get<Group>(this.endpointMapService.Groups + '/' + id);
  }

  public getAll(): Observable<Group[]> {
    return this.http.get<Group[]>(this.endpointMapService.Groups);
  }

  public create(name: string): Observable<Group> {
    return this.http.post<Group>(this.endpointMapService.Groups, { name });
  }

  public update(updates: UpdateGroup): Observable<void> {
    return this.http.post<void>(this.endpointMapService.Groups + '/update', updates);
  }
}
