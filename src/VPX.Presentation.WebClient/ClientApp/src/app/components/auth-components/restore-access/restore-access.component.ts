import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {UsersService} from '../../../shared/services/users.service';
import {AlertService} from '../../../shared/services/alert.service';
import {RestoreUserAccess} from '../../../shared/services/interfaces';

@Component({
  selector: 'app-restore-access',
  templateUrl: './restore-access.component.html',
  styleUrls: ['./restore-access.component.css']
})
export class RestoreAccessComponent implements OnInit {
  submitted = false;
  message: string;
  formGroup: FormGroup;

  constructor(private usersService: UsersService,
              private alertService: AlertService) { }

  ngOnInit() {
    this.formGroup = new FormGroup({
      email: new FormControl('', [
        Validators.required,
        Validators.email
      ])
    });
  }

  hasError (controlName: string, errorName: string) {
    return this.formGroup.controls[controlName].hasError(errorName);
  }

  submit() {
    const data = this.formGroup.value;
    const restoreUserAccess: RestoreUserAccess = {
      email: data.email
    };

    this.submitted = true;
    this.usersService.restoreAccess(restoreUserAccess)
      .subscribe(() => {
        this.formGroup.reset();
        this.message = 'Вам на почту отправлен новый пароль.';
        this.alertService.success(this.message);
        this.submitted = false;
      }, () => {
        this.submitted = false;
      });
  }

  close() {
    this.message = null;
  }
}
