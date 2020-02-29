import {Title} from '@angular/platform-browser';
import {Injectable} from '@angular/core';

@Injectable({providedIn: 'root'})
export class PageService {
  public header: string;

  constructor(private titleService: Title) {
  }

  changeHeader(header: string) {
    this.header = header;
    this.changeTitle(header);
  }

  changeTitle(title: string) {
    this.titleService.setTitle(title);
  }
}
