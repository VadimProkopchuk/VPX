import {NgModule} from '@angular/core';
import {
  MatAutocompleteModule,
  MatBadgeModule,
  MatButtonModule,
  MatCardModule, MatChipsModule, MatDialogModule,
  MatIconModule,
  MatInputModule, MatProgressSpinnerModule,
  MatStepperModule,
  MatTabsModule,
  MatListModule,
  MatTreeModule,
  MatExpansionModule, MatRadioModule, MatCheckboxModule,
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
    MatChipsModule,
    MatAutocompleteModule,
    MatProgressSpinnerModule,
    MatDialogModule,
    MatExpansionModule,
    MatRadioModule,
    MatCheckboxModule,
  ],
  exports: [
    MatTabsModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatStepperModule,
    MatBadgeModule,
    MatChipsModule,
    MatAutocompleteModule,
    MatProgressSpinnerModule,
    MatDialogModule,
    MatListModule,
    MatTreeModule,
    MatExpansionModule,
    MatRadioModule,
    MatCheckboxModule,
  ]
})
export class MaterialModule { }
