import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import {
  debounceTime,
  distinctUntilChanged,
  filter,
  map,
  switchMap,
} from 'rxjs/operators';
import { CitiesService } from 'src/app/core/services/cities.service';
import { CityResponse } from 'src/app/shared/models/cityResponse';

@Component({
  selector: 'app-property-form',
  templateUrl: './property-form.component.html',
  styleUrls: ['./property-form.component.css'],
})
export class PropertyFormComponent {
  public propertyForm: FormGroup;
  public filteredOptions: Observable<CityResponse[]>;
  public minDate: Date = new Date();

  constructor(private fb: FormBuilder, private citiesService: CitiesService) {
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

    this.filteredOptions = this.propertyForm.controls.location.valueChanges.pipe(
      debounceTime(400),
      distinctUntilChanged(),
      switchMap((value?: string) => {
        return this.filter(value);
      })
    );
  }

  public submit() {
    console.log(this.propertyForm);
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
