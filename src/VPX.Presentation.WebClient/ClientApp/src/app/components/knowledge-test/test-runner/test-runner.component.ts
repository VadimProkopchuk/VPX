import {Component, OnDestroy, OnInit} from '@angular/core';
import {switchMap} from 'rxjs/operators';
import {ActivatedRoute, Params} from '@angular/router';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {TestService} from '../../../shared/services/test.service';
import {Subscription} from 'rxjs';
import {KnowledgeTest, KnowledgeTestResult} from '../../../shared/services/interfaces';
import {PageService} from '../../../shared/services/page.service';
import {AlertService} from '../../../shared/services/alert.service';

@Component({
  selector: 'app-test-runner',
  templateUrl: './test-runner.component.html',
  styleUrls: ['./test-runner.component.css']
})
export class TestRunnerComponent implements OnInit, OnDestroy {

  testSubscription: Subscription;
  resultSubscription: Subscription;
  test: KnowledgeTest;
  result: KnowledgeTestResult;
  questionNumber = 0;
  loading = false;

  constructor(private route: ActivatedRoute,
              private testService: TestService,
              private pageService: PageService,
              private alertService: AlertService) {
    this.pageService.changeHeader('Тестирование');
  }

  ngOnInit() {
    this.testSubscription = this.route.params
      .pipe(switchMap((params: Params) => {
        this.loading = true;
        return this.testService.execute(params['id']);
      })).subscribe((test: KnowledgeTest) => {
        this.test = test;
        this.loading = false;
      });
  }

  nextQuestion() {
    this.questionNumber++;
  }

  previousQuestion() {
    this.questionNumber--;
  }

  submit() {
    this.loading = true;
    this.alertService.info('Проверка ответов ...');

    this.resultSubscription = this.testService
      .submit(this.test)
      .subscribe((result: KnowledgeTestResult) => {
        this.result = result;
        this.loading = false;
      });
  }

  ngOnDestroy(): void {
    if (this.testSubscription) {
      this.testSubscription.unsubscribe();
    }
    if (this.resultSubscription) {
      this.resultSubscription.unsubscribe();
    }
  }

}
