import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {Literature} from './interfaces';
import {HttpClient} from '@angular/common/http';
import {EndpointMapService} from './endpoint-map.service';

@Injectable({providedIn: 'root'})
export class LiteratureService {

  constructor(private http: HttpClient,
              private endpointMapService: EndpointMapService) {
  }

  get(): Observable<Literature> {
    return this.http.get<Literature>(this.endpointMapService.Literature);
  }

  update(content: string): Observable<void> {
    return this.http.post<void>(this.endpointMapService.Literature, { content });
  }
}
