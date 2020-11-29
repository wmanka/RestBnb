import { PropertiesRoutingModule } from './properties-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { PropertyFormComponent } from './property-form/property-form.component';
import { FlexLayoutModule } from '@angular/flex-layout';

@NgModule({
  declarations: [PropertiesRoutingModule.components, PropertyFormComponent],
  imports: [
    PropertiesRoutingModule,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MatDatepickerModule,
    MatCardModule,
    MatButtonModule,
    MatInputModule,
    MatAutocompleteModule,
    MatSelectModule,
    MatNativeDateModule,
    MatTableModule,
    FlexLayoutModule,
  ],
})
export class PropertiesModule {}
