import { Component } from '@angular/core';
import {AuthService} from '../../shared/services/auth.service';
import { CurrentUserService} from '../../shared/services/current-user.service';
import {PageService} from '../../shared/services/page.service';
import {MatDialog} from '@angular/material';
import {Router} from '@angular/router';
import {UserSummaryDialogComponent} from '../dialogs/user-summary-dialog/user-summary-dialog.component';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  constructor(private pageService: PageService,
              private authService: AuthService,
              private currentUserService: CurrentUserService,
              private userService: CurrentUserService,
              private router: Router,
              public dialog: MatDialog) {
  }

  openUserInfoDialog() {
    const dialogRef = this.dialog.open(UserSummaryDialogComponent, {
      data: {
        user: this.currentUserService.user
      },
      width: '320px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === true) {
        this.logout();
      }
    });
  }

  logout() {
    this.userService.logout();
    this.router.navigate(['/']).then(() => {});
  }
}
