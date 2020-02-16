import { Component, OnInit } from '@angular/core';
import {AuthService} from '../../../shared/services/auth.service';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {LoginModel} from '../../../shared/services/interfaces';
import {Router} from '@angular/router';
import {UserService} from '../../../shared/services/user.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {
  form: FormGroup;
  submitted = false;
  message: string;

  constructor(private authService: AuthService,
              private userService: UserService,
              private router: Router) {
  }

  ngOnInit() {
    this.form = new FormGroup({
      Email: new FormControl(null, [Validators.email, Validators.required]),
      Password: new FormControl(null, [Validators.required])
    });
  }

  onSubmit(formData) {
    console.log(formData);

    this.submitted = true;
    const loginModel: LoginModel = {
      Email: formData.Email,
      Password: formData.Password
    };

    this.authService.login(loginModel).subscribe(() => {
      this.form.reset();
      this.userService.loadCurrentUserInfo();
      // this.router.navigate(['/admin', 'dashboard']);
      this.submitted = false;
    }, () => {
      this.submitted = false;
    });
  }
}
