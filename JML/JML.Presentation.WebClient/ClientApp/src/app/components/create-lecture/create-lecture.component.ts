import { Component, OnInit, OnDestroy} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {AlertService} from '../../shared/services/alert.service';
import {Lecture, Tag} from '../../shared/services/interfaces';
import {LecturesService} from '../../shared/services/lectures.service';
import {Subscription} from 'rxjs';
import {TagsService} from '../../shared/services/tags.service';

@Component({
  selector: 'app-create-lecture',
  templateUrl: './create-lecture.component.html',
  styleUrls: ['./create-lecture.component.css']
})
export class CreateLectureComponent implements OnInit, OnDestroy {

  form: FormGroup;
  lectureSubscription: Subscription;
  tagsSubscription: Subscription;
  tags: Array<Tag> = [];
  autoCompleteTags: Array<Tag> = [];

  constructor(private alertService: AlertService,
              private lecturesService: LecturesService,
              private tagService: TagsService) { }

  ngOnInit() {
    this.form = new FormGroup({
      title: new FormControl(null, Validators.required),
      url: new FormControl(null, Validators.required),
      text: new FormControl(null, Validators.required),
      preview: new FormControl(null),
    });

    this.tagsSubscription = this.tagService
      .getAll()
      .subscribe(tags => {
        this.autoCompleteTags.push(...tags);
        console.log(...tags);
      });
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
      name: this.form.value.title,
      url: this.form.value.url,
      content: this.form.value.text,
      tags: this.tags,
      preview: this.form.value.preview
    };

    this.lectureSubscription = this.lecturesService.create(lecture)
      .subscribe((createdLecture: Lecture) => {
        this.form.reset();
        this.tags = [];
        this.alertService.success(`Материал "${createdLecture.name}" успешно добавлен.`);
        this.alertService.info(createdLecture.id + ' ' + createdLecture.createdAt);
      }, (error) => {
        this.alertService.danger(`Ошибка добавления.`);
        this.alertService.danger(error);
      });
  }

  ngOnDestroy(): void {
    if (this.lectureSubscription) {
      this.lectureSubscription.unsubscribe();
    }
    if (this.tagsSubscription) {
      this.tagsSubscription.unsubscribe();
    }
  }
}
