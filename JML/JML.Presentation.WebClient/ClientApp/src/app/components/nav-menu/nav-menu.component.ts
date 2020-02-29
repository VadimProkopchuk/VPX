import { Component } from '@angular/core';
import {AuthService} from '../../shared/services/auth.service';
import { CurrentUserService} from '../../shared/services/current-user.service';
import {PageService} from '../../shared/services/page.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  private openUserInfo;

  constructor(public pageService: PageService,
              private authService: AuthService,
              private currentUserService: CurrentUserService) {
    this.openUserInfo = false;
  }

  toggleUserInfo() {
    this.openUserInfo = !this.openUserInfo;
  }

}
