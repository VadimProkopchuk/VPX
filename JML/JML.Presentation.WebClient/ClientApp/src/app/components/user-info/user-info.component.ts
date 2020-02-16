import {Component, Input, OnInit} from '@angular/core';
import {User} from '../../shared/services/interfaces';
import {CurrentUserService} from '../../shared/services/current-user.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.css']
})
export class UserInfoComponent implements OnInit {
  @Input() user: User;
  @Input() open: boolean;

  constructor(private userService: CurrentUserService,
              private router: Router) { }

  ngOnInit() {
  }

  logout() {
    this.userService.logout();
    this.router.navigate(['/']);
  }
}
