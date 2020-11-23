import { PropertyDetailsComponent } from './property-details/property-details.component';
import { PropertiesListComponent } from './properties-list/properties-list.component';
import { AuthGuard } from './../../core/guards/auth.guard';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

const routes: Routes = [
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    component: PropertiesListComponent,
  },
  {
    path: ':id',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
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
