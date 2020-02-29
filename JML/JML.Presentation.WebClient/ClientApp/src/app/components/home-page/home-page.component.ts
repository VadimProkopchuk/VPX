import {Component} from '@angular/core';
import {PageService} from '../../shared/services/page.service';
import {AuthService} from '../../shared/services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent {
  registerSelected = false;

  constructor(pageService: PageService,
    private authService: AuthService) {
    pageService.changeHeader('Главная');
  }

  changeRegisterSelected(isSelected: boolean) {
    this.registerSelected = isSelected;
  }
}
