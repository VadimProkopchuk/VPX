import {Component, OnInit} from '@angular/core';
import {PageService} from '../../shared/services/page.service';
import {AuthService} from '../../shared/services/auth.service';
import {ActivatedRoute, Params} from '@angular/router';
import {AlertService} from '../../shared/services/alert.service';
import {CurrentUserService} from '../../shared/services/current-user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  message: string;

  constructor(private pageService: PageService,
              private authService: AuthService,
              private activatedRoute: ActivatedRoute,
              private alertService: AlertService,
              private currentUserService: CurrentUserService) {
    pageService.changeHeader('Главная');
  }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      if (params['authFailed']) {
        this.message = 'Сессия истекла. Пройдите авторизацию повторно.';
      } else {
        this.message = null;
      }
      if (this.message) {
        this.alertService.warning(this.message);
      }
    });
  }
}
