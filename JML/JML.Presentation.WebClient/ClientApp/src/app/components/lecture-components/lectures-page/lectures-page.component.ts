import { Component } from '@angular/core';
import {CurrentUserService} from '../../../shared/services/current-user.service';

@Component({
  selector: 'app-lectures-page',
  templateUrl: './lectures-page.component.html',
  styleUrls: ['./lectures-page.component.css']
})
export class LecturesPageComponent {
  constructor(public currentUserService: CurrentUserService) {
  }
}
