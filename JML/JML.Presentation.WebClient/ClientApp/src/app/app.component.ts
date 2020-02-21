import {Component, OnInit} from '@angular/core';
import {CurrentUserService} from './shared/services/current-user.service';
import {AuthService} from './shared/services/auth.service';
import {AlertService} from './shared/services/alert.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  constructor(private authService: AuthService,
    private currentUserService: CurrentUserService,
    private alertService: AlertService) {
  }

  ngOnInit(): void {
    this.alertService.info('Модули приложения успешно загружены.');
    if (this.authService.token) {
      this.currentUserService.loadCurrentUserInfo();
    }
  }
}
