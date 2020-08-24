import { Component, OnInit } from '@angular/core';
import {PageService} from '../../../shared/services/page.service';

@Component({
  selector: 'app-tests-page',
  templateUrl: './tests-page.component.html',
  styleUrls: ['./tests-page.component.css']
})
export class TestsPageComponent implements OnInit {

  constructor(pageService: PageService) {
    pageService.changeHeader('Тесты');
  }

  ngOnInit() {
  }

}
