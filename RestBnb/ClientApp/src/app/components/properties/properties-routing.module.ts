import { AuthGuard } from './../../core/guards/auth.guard';
import { PropertyFormComponent } from './property-form/property-form.component';
import { PropertyDetailsComponent } from './property-details/property-details.component';
import { PropertiesListComponent } from './properties-list/properties-list.component';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { SearchBarComponent } from './properties-list/search-bar/search-bar.component';
import { PropertyResolver } from 'src/app/core/resolvers/propertyForm.resolver';

const routes: Routes = [
  {
    path: '',
    component: PropertiesListComponent,
  },
  {
    path: 'details/:id',
    component: PropertyDetailsComponent,
  },
  {
    path: 'add',
    canActivate: [AuthGuard],
    component: PropertyFormComponent,
  },
  {
    path: 'edit/:id',
    canActivate: [AuthGuard],
    resolve: {
      property: PropertyResolver,
    },
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
