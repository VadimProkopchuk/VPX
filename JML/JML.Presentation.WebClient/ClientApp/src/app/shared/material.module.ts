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
  MatExpansionModule,
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
  ]
})
export class MaterialModule { }
