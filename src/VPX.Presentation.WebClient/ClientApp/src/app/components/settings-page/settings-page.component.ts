import {Component, OnDestroy, OnInit} from '@angular/core';
import {CurrentUserService} from '../../shared/services/current-user.service';
import {PageService} from '../../shared/services/page.service';
import {User, UserUpdates} from '../../shared/services/interfaces';
import {Subscription} from 'rxjs';
import {UsersService} from '../../shared/services/users.service';
import {AlertService} from '../../shared/services/alert.service';

@Component({
  selector: 'app-settings-page',
  templateUrl: './settings-page.component.html',
  styleUrls: ['./settings-page.component.css']
})
export class SettingsPageComponent implements OnInit, OnDestroy {

  image: string;
  firstName: string;
  lastName: string;
  newPassword: string;
  password: string;

  loading = false;
  subscription: Subscription;

  constructor(private currentUserService: CurrentUserService,
              private usersService: UsersService,
              private alertService: AlertService,
              pageService: PageService) {
    pageService.changeHeader('Настройки');
  }

  ngOnInit() {
    this.firstName = this.currentUserService.user.firstName;
    this.lastName = this.currentUserService.user.lastName;
  }

  onProfileUpdated(image: string) {
    this.image = image;
    console.log((this.image));
  }

  submit() {
    const userUpdates: UserUpdates = {
      firstName: this.firstName,
      lastName: this.lastName,
      password: this.password,
      newPassword: this.newPassword,
      image: this.image
    };

    this.loading = true;
    this.subscription = this.usersService
      .updateProfile(userUpdates)
      .subscribe((user: User) => {
        this.alertService.success('Информация обновлена.');
        this.currentUserService.user = user;
        this.newPassword = null;
        this.password = null;
        this.loading = false;
      });
  }

  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
