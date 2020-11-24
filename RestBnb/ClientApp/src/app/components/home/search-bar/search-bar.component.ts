import { CityResponse } from './../../../shared/models/cityResponse';
import { CitiesService } from './../../../core/services/cities.service';
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

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css'],
})
export class SearchBarComponent {
  public searchForm: FormGroup;
  public filteredOptions: Observable<CityResponse[]>;

  constructor(private fb: FormBuilder, private citiesService: CitiesService) {
    this.searchForm = this.fb.group({
      location: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      numberOfGuests: ['', Validators.required],
    });

    this.filteredOptions = this.searchForm.controls.location.valueChanges.pipe(
      debounceTime(400),
      distinctUntilChanged(),
      switchMap((value?: string) => {
        return this.filter(value);
      })
    );
  }

  public search(): void {
    console.log(this.searchForm);
  }

  private filter(value: string) {
    const filterValue = value.toLowerCase();

    return this.citiesService.getAll(value).pipe(
      filter((data) => !!data),
      map((data) => {
        return data.filter((option) =>
          option.name.toLowerCase().includes(filterValue)
        );
      })
    );
  }
}
