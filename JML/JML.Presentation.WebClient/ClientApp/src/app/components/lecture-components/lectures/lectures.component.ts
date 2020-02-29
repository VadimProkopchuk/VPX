import {Component, OnDestroy, OnInit} from '@angular/core';
import {Subscription} from 'rxjs';
import {PageService} from '../../../shared/services/page.service';
import {Lecture} from '../../../shared/services/interfaces';
import {AlertService} from '../../../shared/services/alert.service';
import {LecturesService} from '../../../shared/services/lectures.service';

@Component({
  selector: 'app-lectures',
  templateUrl: './lectures.component.html',
  styleUrls: ['./lectures.component.css']
})
export class LecturesComponent implements OnInit, OnDestroy {

  subscription: Subscription;
  lectures: Lecture[];

  constructor(private alertService: AlertService,
              private lecturesService: LecturesService,
              pageService: PageService) {
    pageService.changeHeader('Материалы');
  }

  ngOnInit() {
    this.alertService.info('Загрузка материалов.');
    this.subscription = this.lecturesService.getAll()
      .subscribe((lectures: Lecture[]) => {
        this.lectures = lectures;
      });
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
