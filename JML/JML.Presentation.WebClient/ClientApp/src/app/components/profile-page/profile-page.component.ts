import { Component, OnInit } from '@angular/core';
import {PageService} from '../../shared/services/page.service';
import {ActivatedRoute, Params} from '@angular/router';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css']
})
export class ProfilePageComponent implements OnInit {

  id: string;

  constructor(pageService: PageService,
              private route: ActivatedRoute) {
    pageService.changeHeader('Профиль');
  }

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      this.id = params['id'];
    });
  }
}
