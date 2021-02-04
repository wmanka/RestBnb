import { PropertiesService } from 'src/app/core/services/properties.service';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from '@angular/router';
import { Observable } from 'rxjs';
import { BookingsListElement } from './my-bookings.component';
import {
  BookingsService,
  GetAllBookingsParams,
} from 'src/app/core/services/bookings.service';
import { CitiesService } from 'src/app/core/services/cities.service';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';
import { PropertyImagesService } from 'src/app/core/services/images.service';
import { mergeMap } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class MyBookingsResolver implements Resolve<BookingsListElement[]> {
  public data: BookingsListElement[] = [];
  public userId: number;

  constructor(
    private bookingsService: BookingsService,
    private propertiesService: PropertiesService,
    private citiesService: CitiesService,
    private tokenStorageService: TokenStorageService,
    private propertyImagesService: PropertyImagesService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<BookingsListElement[]>
    | Promise<BookingsListElement[]>
    | BookingsListElement[] {
    this.userId = this.tokenStorageService.getCurrentUserId();

    let getAllBookingsParams = new GetAllBookingsParams();
    getAllBookingsParams.userId = this.userId;

    this.bookingsService.getAll(getAllBookingsParams).subscribe((bookings) => {
      bookings.forEach((booking) => {
        this.propertiesService.get(booking.propertyId).subscribe((property) => {
          this.citiesService.get(property.cityId).subscribe((city) => {
            this.propertyImagesService
              .getAll(property.id)
              .subscribe((images) => {
                let listElement = new BookingsListElement();
                listElement.address = property.address;
                listElement.bookingState = booking.bookingState;
                listElement.checkInDate = booking.checkInDate;
                listElement.checkOutDate = booking.checkOutDate;
                listElement.city = city.name;
                listElement.description = property.description;
                listElement.id = booking.id;
                listElement.pricePerNight = property.pricePerNight;
                listElement.propertyId = property.id;
                listElement.propertyName = property.name;
                listElement.totalPrice = booking.totalPrice;
                listElement.accommodatesNumber = property.accommodatesNumber;
                listElement.bedroomNumber = property.bedroomNumber;
                listElement.bathroomNumber = property.bathroomNumber;
                listElement.cancellationDate = booking.cancellationDate;
                if (images.length > 0) {
                  listElement.imageUrl =
                    'data:image/jpeg;base64,' + images[0].image;
                }

                this.data.push(listElement);
              });
          });
        });
      });
    });
    return this.data;
  }
}
