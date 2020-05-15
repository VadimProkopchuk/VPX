import {Component, OnDestroy, OnInit} from '@angular/core';
import {Subscription} from 'rxjs';
import {CardTestTemplate} from '../../../shared/services/interfaces';
import {AlertService} from '../../../shared/services/alert.service';
import {PageService} from '../../../shared/services/page.service';
import {TestService} from '../../../shared/services/test.service';
import {CurrentUserService} from '../../../shared/services/current-user.service';

@Component({
  selector: 'app-tests',
  templateUrl: './tests.component.html',
  styleUrls: ['./tests.component.css']
})
export class TestsComponent implements OnInit, OnDestroy {

  subscription: Subscription;
  deleteSubscription: Subscription;
  templates: Array<CardTestTemplate>;
  processing = false;

  constructor(private alertService: AlertService,
              private testService: TestService,
              public currentUserService: CurrentUserService,
              pageService: PageService) {
    pageService.changeHeader('Тесты');
  }

  ngOnInit() {
    this.alertService.info('Загрузка тестов.');
    this.subscription = this.testService.getAll()
      .subscribe((templates: CardTestTemplate[]) => {
        this.templates = templates;
      });
  }

  delete(id: string) {
    this.processing = true;
    this.deleteSubscription = this.testService.delete(id)
      .subscribe(() => {
        this.templates = this.templates.filter(x => x.id !== id);
        this.processing = false;
      });
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
