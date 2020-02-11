import { Component } from '@angular/core';
import {PageNameService} from '../../shared/services/page-name.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  constructor(private pageNameService: PageNameService) {
  }
}
