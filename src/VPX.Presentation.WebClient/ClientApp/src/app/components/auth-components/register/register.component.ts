import {Component, OnInit} from '@angular/core';
import {AbstractControl, AsyncValidatorFn, FormControl, FormGroup, ValidatorFn, Validators} from '@angular/forms';
import {UsersService} from '../../../shared/services/users.service';
import {map} from 'rxjs/operators';
import {CreateUser, VerificationUser} from '../../../shared/services/interfaces';
import {AlertService} from '../../../shared/services/alert.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  submitted = false;
  success = false;
  isEditable = true;
  personalDataForm: FormGroup;
  accessDataForm: FormGroup;
  verificationDataForm: FormGroup;

  constructor(private usersService: UsersService,
              private alertService: AlertService) {}

  ngOnInit() {
    this.personalDataForm = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      email: new FormControl('', [
          Validators.required,
          Validators.email
      ], this.getAsyncEmailValidator())
    });

    this.accessDataForm = new FormGroup({
      password: new FormControl('', [Validators.required, Validators.minLength(8)]),
      confirm: new FormControl('', [Validators.required, this.getMatchValuesValidator('password')])
    });

    this.verificationDataForm = new FormGroup({
      verificationCode: new FormControl('', [Validators.required])
    });
  }

  hasPersonalError (controlName: string, errorName: string) {
    return this.personalDataForm.controls[controlName].hasError(errorName);
  }

  hasAccessError (controlName: string, errorName: string) {
    return this.accessDataForm.controls[controlName].hasError(errorName);
  }

  getUserName() {
    const personalData = this.personalDataForm.value;
    return `${personalData.firstName} ${personalData.lastName}`;
  }

  verify() {
    const personalData = this.personalDataForm.value;
    const user: VerificationUser = {
      firstName: personalData.firstName,
      lastName: personalData.lastName,
      email: personalData.email,
    };

    this.usersService.verify(user)
      .subscribe(() => {
        this.alertService.success('Вам на почту выслан код для подтверждения регистрации.');
      });
  }

  register() {
    const personalData = this.personalDataForm.value;
    const accessData = this.accessDataForm.value;
    const verificationData = this.verificationDataForm.value;

    const user: CreateUser = {
      firstName: personalData.firstName,
      lastName: personalData.lastName,
      email: personalData.email,
      password: accessData.password,
      verificationCode: verificationData.verificationCode
    };

    this.submitted = true;
    this.isEditable = false;
    this.usersService.register(user)
      .subscribe(() => {
        this.alertService.success('Регистрация успешно завершена.');
        this.success = true;
        this.resetForms();
      });
  }

  reset() {
    this.isEditable = true;
    this.resetForms();
  }

  private getAsyncEmailValidator(): AsyncValidatorFn {
    return (control: AbstractControl) => {
      return this.usersService.hasUserByEmail(control.value)
        .pipe(map(existingEmail => {
          if (existingEmail === true) {
            return { existingEmail };
          }
          return null;
        }));
    };
  }

  private getMatchValuesValidator(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return !!control.parent && !!control.parent.value &&
      control.value === control.parent.controls[matchTo].value
        ? null
        : { match: false };
    };
  }

  private resetForms() {
    this.personalDataForm.reset();
    this.accessDataForm.reset();
    this.verificationDataForm.reset();
  }
}
