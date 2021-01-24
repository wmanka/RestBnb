import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { BookingsComponent } from './bookings/bookings.component';

const routes: Routes = [
  {
    path: 'bookings',
    canActivate: [AuthGuard],
    component: BookingsComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DashboardRoutingModule {
  static components = [BookingsComponent];
}
