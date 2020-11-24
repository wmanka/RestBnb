import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiRoutes } from 'src/app/shared/constants/apiRoutes';
import { CityResponse } from 'src/app/shared/models/cityResponse';

@Injectable({
  providedIn: 'root',
})
export class CitiesService {
  constructor(private http: HttpClient) {}

  public getAll(name: string): Observable<CityResponse[]> {
    let term = name.trim();

    const options = term ? { params: new HttpParams().set('name', term) } : {};

    return this.http.get<CityResponse[]>(ApiRoutes.Cities.GetAll, options);
  }
}
