import { SharedModule } from './../../shared/shared.module';
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
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { PropertyFormComponent } from './property-form/property-form.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MaterialFileInputModule } from 'ngx-material-file-input';
import { MatDividerModule } from '@angular/material/divider';
import { MatCarouselModule } from '@ngbmodule/material-carousel';

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
    MaterialFileInputModule,
    MatIconModule,
    MatDividerModule,
    SharedModule,
    MatCarouselModule.forRoot(),
  ],
})
export class PropertiesModule {}
