import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {CurrentUserService} from '../../../shared/services/current-user.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {
  submitted = false;
  formGroup: FormGroup;

  constructor(private currentUserService: CurrentUserService) {
  }

  ngOnInit() {
    this.formGroup = new FormGroup({
      email: new FormControl('', [
        Validators.required,
        Validators.email
      ]),
      password: new FormControl('', [
        Validators.required
      ])
    });
  }

  isValid() {
    return this.formGroup.status === 'VALID';
  }

  hasError (controlName: string, errorName: string) {
    return this.formGroup.controls[controlName].hasError(errorName);
  }

  submit() {
    const data = this.formGroup.value;

    this.submitted = true;
    this.currentUserService.login(data.email, data.password)
      .subscribe(() => {
        this.formGroup.reset();
        this.submitted = false;
      }, () => {
        this.submitted = false;
      });
  }
}
