import {Component, OnInit} from '@angular/core';
import {CurrentUserService} from './shared/services/current-user.service';
import {AuthService} from './shared/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  constructor(private authService: AuthService,
    private currentUserService: CurrentUserService) {
  }

  ngOnInit(): void {
    if (this.authService.token) {
      this.currentUserService.loadCurrentUserInfo();
    }
  }
}
