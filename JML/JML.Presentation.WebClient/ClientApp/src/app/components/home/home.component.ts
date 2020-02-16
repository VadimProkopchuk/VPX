import {Component} from '@angular/core';
import {PageNameService} from '../../shared/services/page-name.service';
import { AuthService } from '../../shared/services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  registerSelected = false;

  constructor(private pageNameService: PageNameService,
              private authService: AuthService) {
    pageNameService.pageName  = 'Home';
  }

  changeRegisterSelected(isSelected: boolean) {
    this.registerSelected = isSelected;
  }
}
