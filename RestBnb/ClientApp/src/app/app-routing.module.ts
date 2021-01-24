import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const appRoutes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/properties' },
  {
    path: 'properties',
    loadChildren: () =>
      import('./components/properties/properties.module').then(
        (m) => m.PropertiesModule
      ),
  },
  {
    path: 'dashboard',
    loadChildren: () =>
      import('./components/dashboard/dashboard.module').then(
        (m) => m.DashboardModule
      ),
  },
  {
    path: 'login',
    loadChildren: () =>
      import('./components/login/login.module').then((m) => m.LoginModule),
  },
  {
    path: 'register',
    loadChildren: () =>
      import('./components/register/register.module').then(
        (m) => m.RegisterModule
      ),
  },
  { path: '**', pathMatch: 'full', redirectTo: '/properties' },
];

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes, { relativeLinkResolution: 'legacy' }),
  ],
  exports: [RouterModule],
  providers: [],
})
export class AppRoutingModule {}
