import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiRoutes } from 'src/app/shared/constants/apiRoutes';
import { PropertyResponse } from 'src/app/shared/models/propertyResponse';

@Injectable({
  providedIn: 'root',
})
export class PropertiesService {
  constructor(private http: HttpClient) {}

  public getAll(
    params?: GetAllPropertiesParams
  ): Observable<PropertyResponse[]> {
    const options = params !== null ? { params: new HttpParams() } : {};

    if (params != null) {
      Object.keys(params).forEach((key) => {
        options.params = options.params.append(key, params[key]);
      });
    }

    return this.http.get<PropertyResponse[]>(
      ApiRoutes.Properties.GetAll,
      options
    );
  }
}

export class GetAllPropertiesParams {
  startDate?: Date;
  endDate?: Date;
  maxPricePerNight?: number;
  minPricePerNight?: number;
  accommodatesNumber?: number;
  userId?: number;
  cityId?: number;
}
