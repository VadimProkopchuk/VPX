import { Component, OnInit } from '@angular/core';
import {PageService} from '../../../shared/services/page.service';
import {switchMap} from 'rxjs/operators';
import {ActivatedRoute, Params} from '@angular/router';
import {KnowledgeTestResult} from '../../../shared/services/interfaces';
import {Subscription} from 'rxjs';
import {TestService} from '../../../shared/services/test.service';

@Component({
  selector: 'app-test-results-page',
  templateUrl: './test-results-page.component.html',
  styleUrls: ['./test-results-page.component.css']
})
export class TestResultsPageComponent implements OnInit {

  subscription: Subscription;
  results: Array<KnowledgeTestResult>;

  constructor(pageService: PageService,
              private route: ActivatedRoute,
              private testService: TestService) {
    pageService.changeHeader('Результаты тестирования');
  }

  ngOnInit() {
    this.subscription = this.route.params
      .pipe(switchMap((params: Params) => {
        console.log(params);
        return this.testService.results(params['id'], params['userId']);
      })).subscribe((results: Array<KnowledgeTestResult>) => {
        this.results = results;
      });
  }

}
