import { PropertyImageResponse } from './../../../shared/models/imageResponse';
import { PropertyImagesService } from 'src/app/core/services/images.service';
import { Component } from '@angular/core';
import {
  GetAllPropertiesParams,
  PropertiesService,
} from 'src/app/core/services/properties.service';
import { PropertyListItem } from './models/propertyListItem';
import { SearchModel } from './models/searchModel';
import * as moment from 'moment';

@Component({
  selector: 'app-properties-list',
  templateUrl: './properties-list.component.html',
  styleUrls: ['./properties-list.component.css'],
})
export class PropertiesListComponent {
  public searchModel: SearchModel;
  public properties: PropertyListItem[];
  public displayedColumns: string[] = [
    'imageUrl',
    'name',
    'description',
    'price',
    'buttons',
  ];

  constructor(
    private propertiesService: PropertiesService,
    private propertyImagesService: PropertyImagesService
  ) {}

  public updateComponent($event): void {
    this.searchModel = $event;

    const params = new GetAllPropertiesParams();
    params.cityId = this.searchModel.location;
    params.accommodatesNumber = this.searchModel.accommodatesNumber;

    this.propertiesService.getAll(params).subscribe((properties) => {
      this.properties = properties.map((x) => new PropertyListItem(x));

      this.properties.forEach((property) => {
        this.propertyImagesService
          .getAll(property.id)
          .subscribe((x: PropertyImageResponse[]) => {
            if (x.length > 0) {
              let imageUrl = 'data:image/jpeg;base64,' + x[0].image;
              property.imageUrl = imageUrl;
            }
          });
      });
    });
  }

  public openDetailsTab(propertyId: number) {
    window.open(
      '/properties/details/' +
        propertyId +
        '?startDate=' +
        moment(this.searchModel.startDate).unix() +
        '&endDate=' +
        moment(this.searchModel.endDate).unix()
    );
  }
}
