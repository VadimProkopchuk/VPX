import {NgModule} from '@angular/core';
import {NotifierModule, NotifierOptions} from 'angular-notifier';

const notifierOptions: NotifierOptions = {
  position: {
    horizontal: {
      position: 'right',
      distance: 15
    },
    vertical: {
      position: 'bottom',
      distance: 15,
      gap: 9
    }
  },
  theme: 'material',
  behaviour: {
    autoHide: 10000,
    onClick: false,
    onMouseover: 'pauseAutoHide',
    showDismissButton: true,
    stacking: 8
  },
  animations: {
    enabled: true,
    show: {
      preset: 'slide',
      speed: 300,
      easing: 'ease'
    },
    hide: {
      preset: 'fade',
      speed: 300,
      easing: 'ease',
      offset: 50
    },
    shift: {
      speed: 300,
      easing: 'ease'
    },
    overlap: 150
  }
};

@NgModule({
  imports: [NotifierModule.withConfig(notifierOptions)],
  exports: [NotifierModule]
})
export class AppNotificationsModule {
}
