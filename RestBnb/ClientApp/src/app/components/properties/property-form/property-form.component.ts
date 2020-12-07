import { PropertyResponse } from './../../../shared/models/propertyResponse';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { CitiesService } from 'src/app/core/services/cities.service';
import { CityResponse } from 'src/app/shared/models/cityResponse';
import { PropertiesService } from 'src/app/core/services/properties.service';
import { PropertyFormModel } from '../properties-list/models/propertyFormModel';
import { ActivatedRoute } from '@angular/router';
import {
  debounceTime,
  distinctUntilChanged,
  filter,
  map,
  switchMap,
} from 'rxjs/operators';
import * as moment from 'moment';

@Component({
  selector: 'app-property-form',
  templateUrl: './property-form.component.html',
  styleUrls: ['./property-form.component.css'],
})
export class PropertyFormComponent {
  public propertyForm: FormGroup;
  public filteredOptions: Observable<CityResponse[]>;
  public minDate: Date = new Date();
  public property: PropertyResponse;
  public isInEditMode: boolean;

  constructor(
    private fb: FormBuilder,
    private citiesService: CitiesService,
    private propertiesService: PropertiesService,
    private route: ActivatedRoute
  ) {
    this.property = this.route.snapshot.data.property;
    if (this.property != null) {
      this.isInEditMode = true;
    }

    this.propertyForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      address: ['', Validators.required],
      price: ['', Validators.required],
      bathroomNumber: ['', Validators.required],
      bedroomNumber: ['', Validators.required],
      accommodatesNumber: ['', Validators.required],
      checkInTime: ['', Validators.required],
      checkOutTime: ['', Validators.required],
      location: ['', Validators.required],
      images: [''],
    });

    if (this.isInEditMode) {
      this.citiesService.get(this.property.cityId).subscribe((city) => {
        this.propertyForm.patchValue({
          name: this.property.name,
          description: this.property.description,
          price: this.property.pricePerNight,
          address: this.property.address,
          accommodatesNumber: this.property.accommodatesNumber,
          bedroomNumber: this.property.bedroomNumber,
          bathroomNumber: this.property.bathroomNumber,
          checkInTime: moment(this.property.checkInTime).format('HH:mm'),
          checkOutTime: moment(this.property.checkOutTime).format('HH:mm'),
          location: { id: city.id, name: city.name },
        });
      });
    }

    this.filteredOptions = this.propertyForm.controls.location.valueChanges.pipe(
      debounceTime(400),
      distinctUntilChanged(),
      switchMap((value?: string) => {
        return this.filter(value);
      })
    );
  }

  public submit(): void {
    const checkInTimeFieldValue: string = this.propertyForm.value.checkInTime;
    const checkOutTimeFieldValue: string = this.propertyForm.value.checkOutTime;

    const checkInDateTime: Date = new Date();
    const checkInHoursAndMinutes: Array<string> = checkInTimeFieldValue.split(
      ':'
    );
    checkInDateTime.setHours(parseInt(checkInHoursAndMinutes[0]));
    checkInDateTime.setMinutes(parseInt(checkInHoursAndMinutes[1]));
    checkInDateTime.setSeconds(0);

    const checkOutDateTime = new Date();
    const checkOutHoursAndMinutes: Array<string> = checkOutTimeFieldValue.split(
      ':'
    );
    checkOutDateTime.setHours(parseInt(checkOutHoursAndMinutes[0]));
    checkOutDateTime.setMinutes(parseInt(checkOutHoursAndMinutes[1]));
    checkOutDateTime.setSeconds(0);

    const propertyFormModel = new PropertyFormModel();
    propertyFormModel.name = this.propertyForm.value.name;
    propertyFormModel.description = this.propertyForm.value.description;
    propertyFormModel.address = this.propertyForm.value.address;
    propertyFormModel.pricePerNight = this.propertyForm.value.price;
    propertyFormModel.bathroomNumber = this.propertyForm.value.bathroomNumber;
    propertyFormModel.bedroomNumber = this.propertyForm.value.bedroomNumber;
    propertyFormModel.accommodatesNumber = this.propertyForm.value.accommodatesNumber;
    propertyFormModel.checkInTime = checkInDateTime;
    propertyFormModel.checkOutTime = checkOutDateTime;
    propertyFormModel.cityId = this.propertyForm.value.location.id;

    console.log(propertyFormModel);

    if (this.isInEditMode) {
      this.propertiesService
        .update(this.property.id, propertyFormModel)
        .subscribe((x) => console.log(x));
    } else {
      this.propertiesService
        .create(propertyFormModel)
        .subscribe((x) => console.log(x));
    }
  }

  private filter(value: string | CityResponse) {
    let filterValue: string;

    if (typeof value === 'string') {
      filterValue = value.toLowerCase();
    } else {
      filterValue = value.name.toLowerCase();
    }

    return this.citiesService.getAll(filterValue).pipe(
      filter((data) => !!data),
      map((data) => {
        return data.filter((option) =>
          option.name.toLowerCase().includes(filterValue)
        );
      })
    );
  }

  public displayCity(city: CityResponse) {
    return city ? city.name : '';
  }
}
