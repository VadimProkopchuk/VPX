import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {EndpointMapService} from './endpoint-map.service';
import {Lecture, SectionGroupModel} from './interfaces';
import {Observable} from 'rxjs';

@Injectable({providedIn: 'root'})
export class LecturesService {
  constructor(private http: HttpClient,
              private endpointMapService: EndpointMapService) {
  }

  get(url: string): Observable<Lecture> {
    return this.http.get<Lecture>(this.endpointMapService.Lectures + '/' + url);
  }

  getAllBySection(): Observable<Array<SectionGroupModel>> {
    return this.http.get<Array<SectionGroupModel>>(this.endpointMapService.Lectures);
  }

  create(lectureModel: Lecture): Observable<Lecture> {
    return this.http.post<Lecture>(this.endpointMapService.Lectures, lectureModel);
  }

  update(lectureModel: Lecture): Observable<Lecture> {
    return this.http.put<Lecture>(this.endpointMapService.Lectures, lectureModel);
  }

  remove(id: string): Observable<Lecture> {
    return this.http.delete<Lecture>(this.endpointMapService.Lectures + '/' + id);
  }
}
