import { PropertyDetailsComponent } from './property-details/property-details.component';
import { PropertiesListComponent } from './properties-list/properties-list.component';
import { AuthGuard } from './../../core/guards/auth.guard';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

const routes: Routes = [
  {
    path: '',
    component: PropertiesListComponent,
  },
  {
    path: ':id',
    component: PropertyDetailsComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PropertiesRoutingModule {
  static components = [PropertiesListComponent, PropertyDetailsComponent];
}
