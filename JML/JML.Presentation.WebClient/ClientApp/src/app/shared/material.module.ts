import {NgModule} from '@angular/core';
import {MatButtonModule, MatCardModule, MatIconModule, MatInputModule, MatStepperModule, MatTabsModule} from '@angular/material';



@NgModule({
  imports: [
    MatTabsModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatStepperModule,
  ],
  exports: [
    MatTabsModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatStepperModule,
  ]
})
export class MaterialModule { }
