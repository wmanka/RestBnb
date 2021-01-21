import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiRoutes } from 'src/app/shared/constants/apiRoutes';

@Injectable({
  providedIn: 'root',
})
export class PropertyImagesService {
  constructor(private http: HttpClient) {}

  public create(propertyId: number, images: FormData) {
    return this.http.post(
      ApiRoutes.PropertyImages.Create.replace(
        'propertyId',
        propertyId.toString()
      ),
      images
    );
  }
}
