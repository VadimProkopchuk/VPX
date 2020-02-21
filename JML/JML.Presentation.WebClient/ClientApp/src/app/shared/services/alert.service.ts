import {Injectable} from '@angular/core';
import {NotifierService} from 'angular-notifier';

@Injectable({providedIn: 'root'})
export class AlertService {
  private readonly notifierService: NotifierService;

  constructor(notifierService: NotifierService) {
    this.notifierService = notifierService;
  }

  success(text: string) {
    this.notifierService.notify('success', text);
  }

  warning(text: string) {
    this.notifierService.notify('warning', text);
  }

  danger(text: string) {
    this.notifierService.notify('error', text);
  }

  info(text: string) {
    this.notifierService.notify('info', text);
  }
}
