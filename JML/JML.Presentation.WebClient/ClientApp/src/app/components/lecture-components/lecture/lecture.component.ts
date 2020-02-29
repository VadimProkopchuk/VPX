import {Component, OnDestroy, OnInit} from '@angular/core';
import {Observable, Subscription} from 'rxjs';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {switchMap} from 'rxjs/operators';
import {Lecture} from '../../../shared/services/interfaces';
import {LecturesService} from '../../../shared/services/lectures.service';
import {PageService} from '../../../shared/services/page.service';
import {AlertService} from '../../../shared/services/alert.service';

@Component({
  selector: 'app-lecture',
  templateUrl: './lecture.component.html',
  styleUrls: ['./lecture.component.css']
})
export class LectureComponent implements OnInit, OnDestroy {
  lecture$: Observable<Lecture>;
  removeLectureSubscription: Subscription;

  constructor(private activatedRoute: ActivatedRoute,
              private lecturesService: LecturesService,
              private alertService: AlertService,
              private router: Router,
              pageService: PageService) {
    pageService.changeHeader('Материалы');
  }

  ngOnInit() {
    this.lecture$ = this.activatedRoute.params
      .pipe(switchMap((params: Params) => {
        return this.lecturesService.get(params['url']);
      }));
  }

  remove(id: string) {
    this.removeLectureSubscription = this.lecturesService
      .remove(id)
      .subscribe(lecture => {
        this.alertService.warning('Материал "' + lecture.name + '" удален.');
        this.router.navigate(['/lectures', 'all']);
      }, (() => {
        this.alertService.danger('Ошибка удаления.');
      }));
  }

  ngOnDestroy(): void {
    if (this.removeLectureSubscription) {
      this.removeLectureSubscription.unsubscribe();
    }
  }
}
