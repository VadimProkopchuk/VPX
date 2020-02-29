import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {EndpointMapService} from './endpoint-map.service';
import {Observable} from 'rxjs';
import {Tag} from './interfaces';

@Injectable({ providedIn: 'root' })
export class TagsService {
  constructor(private http: HttpClient,
              private endpointMapService: EndpointMapService) {
  }

  getAll(): Observable<Tag[]> {
    return this.http.get<Tag[]>(this.endpointMapService.Tags);
  }
}
