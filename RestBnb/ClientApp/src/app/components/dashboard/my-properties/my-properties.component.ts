import { GetAllPropertiesParams } from './../../../core/services/properties.service';
import { PropertiesService } from 'src/app/core/services/properties.service';
import { PropertyResponse } from 'src/app/shared/models/propertyResponse';
import { Component } from '@angular/core';
import { ThemePalette } from '@angular/material/core';
import { ProgressSpinnerMode } from '@angular/material/progress-spinner';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';

@Component({
  selector: 'app-my-properties',
  templateUrl: './my-properties.component.html',
  styleUrls: ['./my-properties.component.css'],
})
export class MyPropertiesComponent {
  public userId: number;
  public myProperties: PropertyResponse[] = new Array<PropertyResponse>();
  public isLoading = true;

  color: ThemePalette = 'primary';
  mode: ProgressSpinnerMode = 'indeterminate';

  myPropertiesDisplayedColumns: string[] = [
    'name',
    'address',
    'pricePerNight',
    'details',
    'edit',
    'delete',
  ];

  constructor(
    private tokenStorageService: TokenStorageService,
    private propertiesService: PropertiesService
  ) {
    this.userId = this.tokenStorageService.getCurrentUserId();

    let params = new GetAllPropertiesParams();
    params.userId = this.userId;

    this.propertiesService.getAll(params).subscribe((properties) => {
      this.myProperties = properties;
      this.isLoading = false;
    });
  }

  public deleteProperty(id: number) {
    this.propertiesService.delete(id).subscribe(() => {
      let i = this.myProperties.findIndex((a) => a.id === id);
      this.myProperties.splice(i, 1);
    });
  }
}
