import {Component, OnDestroy, OnInit} from '@angular/core';
import {Subscription} from 'rxjs';
import {PageService} from '../../../shared/services/page.service';
import {Lecture, Literature, SectionGroupModel} from '../../../shared/services/interfaces';
import {AlertService} from '../../../shared/services/alert.service';
import {LecturesService} from '../../../shared/services/lectures.service';
import {CurrentUserService} from '../../../shared/services/current-user.service';
import {LiteratureService} from '../../../shared/services/literature.service';
import {FormControl} from '@angular/forms';

@Component({
  selector: 'app-lectures',
  templateUrl: './lectures.component.html',
  styleUrls: ['./lectures.component.css']
})
export class LecturesComponent implements OnInit, OnDestroy {

  subscription: Subscription;
  literatureSubscription: Subscription;
  updateSubscription: Subscription;
  lectures: Array<SectionGroupModel>;
  literature: Literature;
  isEditLiterature = false;
  literatureContent: FormControl;

  constructor(private alertService: AlertService,
              private lecturesService: LecturesService,
              public currentUserService: CurrentUserService,
              private literatureService: LiteratureService,
              pageService: PageService) {
    pageService.changeHeader('Материалы');
  }

  ngOnInit() {
    this.alertService.info('Загрузка материалов.');
    this.subscription = this.lecturesService.getAllBySection()
      .subscribe((lectures: Array<SectionGroupModel>) => {
        this.lectures = lectures;
      });
    this.literatureSubscription = this.literatureService.get()
      .subscribe((literature: Literature) => {
        this.literature = literature;
        this.literatureContent = new FormControl(literature.content);
      });
  }

  saveLiterature() {
    this.updateSubscription = this.literatureService
      .update(this.literatureContent.value)
      .subscribe(() => {
        this.literature.content = this.literatureContent.value;
        this.isEditLiterature = false;
        this.alertService.success('Литература успешно обновлена');
      });
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
