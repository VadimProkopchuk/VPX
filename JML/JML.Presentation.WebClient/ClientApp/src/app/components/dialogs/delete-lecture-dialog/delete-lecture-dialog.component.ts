import {Component, Inject} from '@angular/core';
import {Lecture} from '../../../shared/services/interfaces';
import {MAT_DIALOG_DATA} from '@angular/material';

@Component({
  selector: 'app-delete-lecture-dialog',
  templateUrl: './delete-lecture-dialog.component.html',
})
export class DeleteLectureDialogComponent {
  public lecture: Lecture;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {
    this.lecture = data.lecture;
  }
}
