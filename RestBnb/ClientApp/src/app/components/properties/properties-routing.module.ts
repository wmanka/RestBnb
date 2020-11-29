import { PropertyFormComponent } from './property-form/property-form.component';
import { PropertyDetailsComponent } from './property-details/property-details.component';
import { PropertiesListComponent } from './properties-list/properties-list.component';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { SearchBarComponent } from './properties-list/search-bar/search-bar.component';

const routes: Routes = [
  {
    path: '',
    component: PropertiesListComponent,
  },
  {
    path: ':id',
    component: PropertyDetailsComponent,
  },
  {
    path: 'edit/:id',
    component: PropertyFormComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PropertiesRoutingModule {
  static components = [
    PropertiesListComponent,
    PropertyDetailsComponent,
    SearchBarComponent,
    PropertyFormComponent,
  ];
}
