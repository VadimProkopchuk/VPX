import {Component, OnDestroy, OnInit} from '@angular/core';
import {Subscription} from 'rxjs';
import {Lecture, Tag} from '../../../shared/services/interfaces';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {LecturesService} from '../../../shared/services/lectures.service';
import {PageService} from '../../../shared/services/page.service';
import {switchMap} from 'rxjs/operators';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {TagsService} from '../../../shared/services/tags.service';
import {AlertService} from '../../../shared/services/alert.service';

@Component({
  selector: 'app-edit-lecture',
  templateUrl: './edit-lecture.component.html',
  styleUrls: ['./edit-lecture.component.css']
})
export class EditLectureComponent implements OnInit, OnDestroy {
  submitted = false;
  lecture: Lecture;
  tags: Array<Tag> = [];
  autoCompleteTags: Array<Tag> = [];
  form: FormGroup;
  lectureSubscription: Subscription;
  updateLectureSubscription: Subscription;
  tagsSubscription: Subscription;

  constructor(private route: ActivatedRoute,
              private lecturesService: LecturesService,
              private tagsService: TagsService,
              private alertService: AlertService,
              private router: Router,
              pageService: PageService) {
    pageService.changeHeader('Редактирование материалов');
  }

  ngOnInit() {
    this.lectureSubscription = this.route.params
      .pipe(switchMap((params: Params) => {
        return this.lecturesService.get(params['url']);
      })).subscribe(lecture => {
        this.lecture = lecture;
        this.form = new FormGroup({
          title: new FormControl(lecture.name, Validators.required),
          url: new FormControl(lecture.url, Validators.required),
          text: new FormControl(lecture.content, Validators.required),
          section: new FormControl(lecture.section, Validators.required),
          preview: new FormControl(lecture.preview),
        });
        this.tags = lecture.tags;
      });
    this.tagsSubscription = this.tagsService
      .getAll()
      .subscribe(tags => this.autoCompleteTags = tags);
  }

  hasError (controlName: string, errorName: string) {
    return this.form.controls[controlName].hasError(errorName);
  }

  submit() {
    if (this.form.invalid) {
      return;
    }

    for (const tag of this.tags) {
      if (tag.value === tag.display) {
        tag.value = null;
      }
    }

    const lecture: Lecture = {
      id: this.lecture.id,
      name: this.form.value.title,
      url: this.form.value.url,
      content: this.form.value.text,
      tags: this.tags,
      preview: this.form.value.preview,
      section: this.form.value.section,
    };

    this.submitted = true;
    this.alertService.info(`Обновление материала...`);
    this.updateLectureSubscription = this.lecturesService.update(lecture)
      .subscribe((updatedLecture: Lecture) => {
        this.form.reset();
        this.tags = [];
        this.submitted = false;
        this.alertService.success(`Материал "${updatedLecture.name}" успешно обновлен.`);
        this.router.navigate(['/lecture', updatedLecture.url]).then(() => {});
      }, (error) => {
        this.alertService.danger(`Ошибка обновления.`);
        this.submitted = false;
      });
  }

  ngOnDestroy(): void {
    if (this.lectureSubscription) {
      this.lectureSubscription.unsubscribe();
    }
    if (this.tagsSubscription) {
      this.tagsSubscription.unsubscribe();
    }
    if (this.updateLectureSubscription) {
      this.updateLectureSubscription.unsubscribe();
    }
  }
}
