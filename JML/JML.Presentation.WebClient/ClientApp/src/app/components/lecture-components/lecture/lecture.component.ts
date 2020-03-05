import {Component, OnDestroy, OnInit} from '@angular/core';
import {Observable, Subscription} from 'rxjs';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {switchMap} from 'rxjs/operators';
import {Lecture} from '../../../shared/services/interfaces';
import {LecturesService} from '../../../shared/services/lectures.service';
import {PageService} from '../../../shared/services/page.service';
import {AlertService} from '../../../shared/services/alert.service';
import {MatDialog} from '@angular/material';
import {DeleteLectureDialogComponent} from '../../dialogs/delete-lecture-dialog/delete-lecture-dialog.component';
import {CurrentUserService} from '../../../shared/services/current-user.service';

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
              public dialog: MatDialog,
              public currentUserService: CurrentUserService,
              pageService: PageService) {
    pageService.changeHeader('Материалы');
  }

  ngOnInit() {
    this.lecture$ = this.activatedRoute.params
      .pipe(switchMap((params: Params) => {
        return this.lecturesService.get(params['url']);
      }));
  }

  remove(lecture: Lecture) {
    const dialogRef = this.dialog.open(DeleteLectureDialogComponent, {
      data: {
        lecture: lecture
      },
      width: '320px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === true) {
        this.removeLecture(lecture.id);
      }
    });
  }

  private removeLecture(id: string) {
    this.removeLectureSubscription = this.lecturesService
      .remove(id)
      .subscribe(lecture => {
        this.alertService.warning('Материал "' + lecture.name + '" удален.');
        this.router.navigate(['/lectures', 'all']).then(() => {});
      });
  }

  ngOnDestroy(): void {
    if (this.removeLectureSubscription) {
      this.removeLectureSubscription.unsubscribe();
    }
  }
}
