import {NgModule} from '@angular/core';
import {
  MatBadgeModule,
  MatButtonModule,
  MatCardModule,
  MatIconModule,
  MatInputModule,
  MatStepperModule,
  MatTabsModule
} from '@angular/material';



@NgModule({
  imports: [
    MatTabsModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatStepperModule,
    MatBadgeModule,
  ],
  exports: [
    MatTabsModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatStepperModule,
    MatBadgeModule,
  ]
})
export class MaterialModule { }
