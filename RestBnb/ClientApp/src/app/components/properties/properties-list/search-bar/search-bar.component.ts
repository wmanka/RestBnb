import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { CitiesService } from 'src/app/core/services/cities.service';
import { CityResponse } from 'src/app/shared/models/cityResponse';
import { SearchModel } from '../models/searchModel';
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
  @Output() modelChanged = new EventEmitter<SearchModel>();

  public searchForm: FormGroup;
  public filteredOptions: Observable<CityResponse[]>;
  public minDate: Date = new Date();

  constructor(private fb: FormBuilder, private citiesService: CitiesService) {
    this.searchForm = this.fb.group({
      location: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      accommodatesNumber: ['', Validators.required],
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
    const searchModel = new SearchModel(
      this.searchForm.value.startDate,
      this.searchForm.value.endDate,
      this.searchForm.value.location.id,
      this.searchForm.value.accommodatesNumber
    );

    this.notifyModelChanged(searchModel);
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

  private notifyModelChanged(model: SearchModel) {
    this.modelChanged.emit(model);
  }

  displayState(state: CityResponse) {
    return state ? state.name : '';
  }
}
