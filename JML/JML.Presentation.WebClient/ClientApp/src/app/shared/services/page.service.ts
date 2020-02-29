import {Title} from '@angular/platform-browser';
import {Injectable} from '@angular/core';

@Injectable({providedIn: 'root'})
export class PageService {
  private header: string;

  constructor(private titleService: Title) {
  }

  getHeader() {
    return this.header;
  }

  changeHeader(header: string) {
    this.changeTitle(header);
  }

  changeTitle(title: string) {
    this.titleService.setTitle(title);
  }
}
