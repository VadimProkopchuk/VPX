import { Component, OnInit } from '@angular/core';
import {Observable} from 'rxjs';
import {ActivatedRoute, Params} from '@angular/router';
import {switchMap} from 'rxjs/operators';
import {Lecture} from '../../../shared/services/interfaces';
import {LecturesService} from '../../../shared/services/lectures.service';
import {PageService} from '../../../shared/services/page.service';

@Component({
  selector: 'app-lecture',
  templateUrl: './lecture.component.html',
  styleUrls: ['./lecture.component.css']
})
export class LectureComponent implements OnInit {
  lecture$: Observable<Lecture>;

  constructor(private route: ActivatedRoute,
              private lecturesService: LecturesService,
              pageService: PageService) {
    pageService.changeHeader('Материалы');
  }

  ngOnInit() {
    this.lecture$ = this.route.params
      .pipe(switchMap((params: Params) => {
        return this.lecturesService.get(params['url']);
      }));
  }
}
