import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const appRoutes: Routes = [
    { path: '', pathMatch: 'full', redirectTo: '/home' },
    { path: 'properties', loadChildren: () => import('./components/properties/properties.module').then(m => m.PropertiesModule) },
    { path: 'login', loadChildren: () => import('./components/login/login.module').then(m => m.LoginModule) },
    { path: 'register', loadChildren: () => import('./components/register/register.module').then(m => m.RegisterModule) },
    { path: 'home', loadChildren: () => import('./components/home/home.module').then(m => m.HomeModule) },
    { path: '**', pathMatch: 'full', redirectTo: '/home' }
];

@NgModule({
    imports: [RouterModule.forRoot(appRoutes, { relativeLinkResolution: 'legacy' })],
    exports: [RouterModule],
    providers: []
})
export class AppRoutingModule { }
