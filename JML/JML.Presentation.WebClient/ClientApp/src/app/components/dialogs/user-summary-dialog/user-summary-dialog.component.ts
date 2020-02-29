import {Component, Inject} from '@angular/core';
import {User} from '../../../shared/services/interfaces';
import {MAT_DIALOG_DATA} from '@angular/material';

@Component({
  selector: 'app-user-summary-dialog',
  templateUrl: './user-summary-dialog.component.html',
})
export class UserSummaryDialogComponent {
  public user: User;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {
    this.user = data.user;
  }
}
