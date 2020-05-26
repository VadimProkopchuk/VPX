import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {EndpointMapService} from './endpoint-map.service';
import {CardTestTemplate, KnowledgeTest, KnowledgeTestResult, TestTemplate} from './interfaces';
import {Observable} from 'rxjs';

@Injectable({providedIn: 'root'})
export class TestService {
  constructor(private http: HttpClient,
              private endpointMapService: EndpointMapService) {
  }

  getAll(): Observable<Array<CardTestTemplate>> {
    return this.http.get<Array<CardTestTemplate>>(this.endpointMapService.TestTemplates);
  }

  execute(id: string): Observable<KnowledgeTest> {
    return this.http.post<KnowledgeTest>(this.endpointMapService.TestTemplates + '/execute', { id });
  }

  delete(id: string): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(this.endpointMapService.TestTemplates + '/delete', { id });
  }

  submit(test: KnowledgeTest): Observable<KnowledgeTestResult> {
    return this.http.post<KnowledgeTestResult>(this.endpointMapService.TestTemplates + '/submit', test);
  }

  create(template: TestTemplate): Observable<TestTemplate> {
    return this.http.post<TestTemplate>(this.endpointMapService.TestTemplates, template);
  }

  results(id: string, userId: string): Observable<Array<KnowledgeTestResult>> {
    const url = `${this.endpointMapService.TestTemplates}/results/${id}/for-user/${userId}`;
    return this.http.get<Array<KnowledgeTestResult>>(url);
  }
}
