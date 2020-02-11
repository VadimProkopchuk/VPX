import {Component} from '@angular/core';
import {PageNameService} from '../../shared/services/page-name.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  constructor(private pageNameService: PageNameService) {
    pageNameService.pageName  = 'Home';
  }
}
