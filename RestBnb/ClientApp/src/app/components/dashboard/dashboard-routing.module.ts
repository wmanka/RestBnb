import { MyBookingsComponent } from './my-bookings/my-bookings.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { MyPropertiesBookingsComponent } from './my-properties-bookings/my-properties-bookings.component';

const routes: Routes = [
  {
    path: 'my-bookings',
    canActivate: [AuthGuard],
    component: MyBookingsComponent,
  },
  {
    path: 'my-properties-bookings',
    canActivate: [AuthGuard],
    component: MyPropertiesBookingsComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DashboardRoutingModule {
  static components = [MyBookingsComponent, MyPropertiesBookingsComponent];
}
