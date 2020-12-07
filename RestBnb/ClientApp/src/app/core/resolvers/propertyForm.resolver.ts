import { PropertiesService } from 'src/app/core/services/properties.service';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from '@angular/router';
import { PropertyResponse } from 'src/app/shared/models/propertyResponse';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class PropertyResolver implements Resolve<PropertyResponse> {
  constructor(private propertiesService: PropertiesService) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> | Promise<any> | any {
    const propertyId = parseInt(route.paramMap.get('id'));
    return this.propertiesService.get(propertyId);
  }
}
