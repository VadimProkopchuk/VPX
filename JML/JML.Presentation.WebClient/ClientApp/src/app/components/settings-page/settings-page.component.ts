import { Component, OnInit } from '@angular/core';
import {CurrentUserService} from '../../shared/services/current-user.service';
import {PageService} from '../../shared/services/page.service';

@Component({
  selector: 'app-settings-page',
  templateUrl: './settings-page.component.html',
  styleUrls: ['./settings-page.component.css']
})
export class SettingsPageComponent implements OnInit {

  constructor(private currentUserService: CurrentUserService,
              pageService: PageService) {
    pageService.changeHeader('Настройки');
  }

  ngOnInit() {
  }

}
