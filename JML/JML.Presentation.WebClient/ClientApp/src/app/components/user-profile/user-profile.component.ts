import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {Subscription} from 'rxjs';
import {UserProfile} from '../../shared/services/interfaces';
import {UsersService} from '../../shared/services/users.service';
import {CurrentUserService} from '../../shared/services/current-user.service';

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

  constructor(private usersService: UsersService,
              public currentUserService: CurrentUserService) { }

  ngOnInit() {
    if (this.id) {
      this.profileSubscription = this.usersService
        .getProfile(this.id)
        .subscribe((profile: UserProfile) => {
          this.profile = profile;
        });
    }
  }

  ngOnDestroy(): void {
    if (this.profileSubscription) {
      this.profileSubscription.unsubscribe();
    }
  }
}
