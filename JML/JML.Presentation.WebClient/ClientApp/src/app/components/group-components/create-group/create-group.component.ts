import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {GroupsService} from '../../../shared/services/groups.service';
import {AlertService} from '../../../shared/services/alert.service';
import {Group, Lecture} from '../../../shared/services/interfaces';
import {Subscription} from 'rxjs';
import {Router} from '@angular/router';

@Component({
  selector: 'app-create-group',
  templateUrl: './create-group.component.html',
  styleUrls: ['./create-group.component.css']
})
export class CreateGroupComponent implements OnInit, OnDestroy {
  submitted: boolean;
  form: FormGroup;
  subsription: Subscription;

  constructor(private groupsService: GroupsService,
              private alertService: AlertService,
              private router: Router) { }

  ngOnInit() {
    this.form = new FormGroup({
      name: new FormControl(null, Validators.required),
    });
  }

  submit() {
    const className = this.form.value.name;

    this.submitted = true;
    this.subsription = this.groupsService.create(className)
      .subscribe((createdLecture: Group) => {
        this.form.reset();
        this.alertService.success(`Класс "${className}" успешно добавлен.`);
        this.router.navigate(['/groups', 'all']).then(() => {});
      }, () => {
        this.alertService.danger(`Ошибка добавления.`);
        this.submitted = false;
      });
  }

   ngOnDestroy(): void {
    if (this.subsription) {
      this.subsription.unsubscribe();
    }
   }
}
