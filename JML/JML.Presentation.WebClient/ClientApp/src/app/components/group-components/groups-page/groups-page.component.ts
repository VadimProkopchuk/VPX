import {Component} from '@angular/core';
import {CurrentUserService} from '../../../shared/services/current-user.service';
@Component({
  selector: 'app-groups-page',
  templateUrl: './groups-page.component.html',
  styleUrls: ['./groups-page.component.css']
})
export class GroupsPageComponent {
  constructor(public currentUserService: CurrentUserService) {
  }
}
