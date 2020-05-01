import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { PropertiesComponent } from './properties.component';


const routes: Routes = [
    { path: '', component: PropertiesComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PropertiesRoutingModule {
    static components = [PropertiesComponent];
}
