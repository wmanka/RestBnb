import { Component } from '@angular/core';
import {
  GetAllPropertiesParams,
  PropertiesService,
} from 'src/app/core/services/properties.service';
import { PropertyListItem } from './models/propertyListItem';
import { SearchModel } from './models/searchModel';

@Component({
  selector: 'app-properties-list',
  templateUrl: './properties-list.component.html',
  styleUrls: ['./properties-list.component.css'],
})
export class PropertiesListComponent {
  public searchModel: SearchModel;
  public properties: PropertyListItem[];
  public displayedColumns: string[] = ['imageUrl', 'description', 'price'];

  constructor(private propertiesService: PropertiesService) {}

  public updateComponent($event): void {
    this.searchModel = $event;

    const params = new GetAllPropertiesParams();
    params.cityId = this.searchModel.location;
    params.accommodatesNumber = this.searchModel.accommodatesNumber;

    this.propertiesService.getAll(params).subscribe((properties) => {
      this.properties = properties.map((x) => new PropertyListItem(x));
    });
  }
}
