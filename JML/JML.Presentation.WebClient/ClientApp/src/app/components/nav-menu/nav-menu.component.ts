import { Component } from '@angular/core';
import {PageNameService} from '../../shared/services/page-name.service';
import {AuthService} from '../../shared/services/auth.service';
import {UserService} from '../../shared/services/user.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  constructor(private pageNameService: PageNameService,
              private authService: AuthService,
              private userService: UserService,
              private router: Router) {
  }


  logout() {
    this.userService.logout();
    this.router.navigate(['/']);
  }
}
