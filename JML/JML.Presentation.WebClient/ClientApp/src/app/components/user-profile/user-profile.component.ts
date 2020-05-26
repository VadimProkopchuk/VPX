import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {Subscription} from 'rxjs';
import {UserProfile} from '../../shared/services/interfaces';
import {UsersService} from '../../shared/services/users.service';
import {CurrentUserService} from '../../shared/services/current-user.service';
import {AlertService} from '../../shared/services/alert.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit, OnDestroy {

  @Input()
  id: string;

  profileSubscription: Subscription;
  profile: UserProfile;
  httpLoad = false;

  constructor(private usersService: UsersService,
              public currentUserService: CurrentUserService,
              public alertService: AlertService) { }

  ngOnInit() {
    if (this.id) {
      this.profileSubscription = this.usersService
        .getProfile(this.id)
        .subscribe((profile: UserProfile) => {
          this.profile = profile;
        });
    }
  }

  unlock() {
    this.httpLoad = true;
    this.usersService.unlock(this.id)
      .subscribe((userProfile: UserProfile) => {
        this.profile = userProfile;
        this.alertService.success(`Пользователь разблокирован.`);
        this.httpLoad = false;
      });
  }

  lock() {
    this.httpLoad = true;
    this.usersService.lock(this.id)
      .subscribe((userProfile: UserProfile) => {
        this.profile = userProfile;
        this.alertService.success(`Пользователь заблокирован.`);
        this.httpLoad = false;
      });
  }

  addStudentRole() {
    this.httpLoad = true;
    this.usersService.addStudentRole(this.id)
      .subscribe((userProfile: UserProfile) => {
        this.profile = userProfile;
        this.alertService.success(`Роль "Студент" успешно добавлена.`);
        this.httpLoad = false;
      });
  }

  removeStudentRole() {
    this.httpLoad = true;
    this.usersService.removeStudentRole(this.id)
      .subscribe((userProfile: UserProfile) => {
        this.profile = userProfile;
        this.alertService.success(`Роль "Студент" успешно удалена.`);
        this.httpLoad = false;
      });
  }

  addTeacherRole() {
    this.httpLoad = true;
    this.usersService.addTeacherRole(this.id)
      .subscribe((userProfile: UserProfile) => {
        this.profile = userProfile;
        this.alertService.success(`Роль "Учитель" успешно добавлена.`);
        this.httpLoad = false;
      });
  }

  removeTeacherRole() {
    this.httpLoad = true;
    this.usersService.removeTeacherRole(this.id)
      .subscribe((userProfile: UserProfile) => {
        this.profile = userProfile;
        this.alertService.success(`Роль "Учитель" успешно удалена.`);
        this.httpLoad = false;
      });
  }

  ngOnDestroy(): void {
    if (this.profileSubscription) {
      this.profileSubscription.unsubscribe();
    }
  }
}
