import { Component } from '@angular/core';
import { SearchModel } from './models/SearchModel';
@Component({
  selector: 'app-properties-list',
  templateUrl: './properties-list.component.html',
  styleUrls: ['./properties-list.component.css'],
})
export class PropertiesListComponent {
  public searchModel: SearchModel;

  public updateComponent($event): void {
    this.searchModel = $event;

    console.log(this.searchModel.location);
  }
}
