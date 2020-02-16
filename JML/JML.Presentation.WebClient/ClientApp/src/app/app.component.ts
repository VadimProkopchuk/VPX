import {Component, OnInit} from '@angular/core';
import {UserService} from './shared/services/user.service';
import {AuthService} from './shared/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  constructor(private authService: AuthService,
    private userService: UserService) {
  }

  ngOnInit(): void {
    if (this.authService.token) {
      this.userService.loadCurrentUserInfo();
    }
  }
}
