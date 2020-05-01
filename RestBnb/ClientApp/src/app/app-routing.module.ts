import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const appRoutes: Routes = [
    { path: '', pathMatch: 'full', redirectTo: '/properties' },
    { path: 'properties', loadChildren: () => import('./components/properties/properties.module').then(m => m.PropertiesModule) },
    { path: 'login', loadChildren: () => import('./components/login/login.module').then(m => m.LoginModule) },
    { path: '**', pathMatch: 'full', redirectTo: '/properties' }
];

@NgModule({
    imports: [RouterModule.forRoot(appRoutes)],
    exports: [RouterModule],
    providers: []
})
export class AppRoutingModule { }
