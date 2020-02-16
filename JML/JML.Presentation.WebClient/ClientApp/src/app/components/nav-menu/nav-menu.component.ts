import { Component } from '@angular/core';
import {PageNameService} from '../../shared/services/page-name.service';
import {AuthService} from '../../shared/services/auth.service';
import { CurrentUserService} from '../../shared/services/current-user.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  private openUserInfo = false;

  constructor(private pageNameService: PageNameService,
              private authService: AuthService,
              private currentUserService: CurrentUserService) {
  }

  toggleUserInfo() {
    this.openUserInfo = !this.openUserInfo;
  }

}
