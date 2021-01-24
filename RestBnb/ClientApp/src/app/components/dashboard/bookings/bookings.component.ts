import { PropertyImagesService } from './../../../core/services/images.service';
import { CitiesService } from './../../../core/services/cities.service';
import { PropertiesService } from './../../../core/services/properties.service';
import { TokenStorageService } from './../../../core/services/token-storage.service';
import {
  BookingsService,
  GetAllBookingsParams,
  UpdateBookingModel,
} from './../../../core/services/bookings.service';
import { Component } from '@angular/core';
import { BookingResponse } from 'src/app/shared/models/bookingResponse';
import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';

@Component({
  selector: 'app-bookings',
  templateUrl: './bookings.component.html',
  styleUrls: ['./bookings.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition(
        'expanded <=> collapsed',
        animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')
      ),
    ]),
  ],
})
export class BookingsComponent {
  public userId: number;
  public data: BookingsListElement[] = [];
  columnsToDisplay = [
    'checkInDate',
    'checkOutDate',
    'bookingState',
    'city',
    'pricePerNight',
    'totalPrice',
  ];

  expandedElement: BookingResponse | null;

  constructor(
    private bookingsService: BookingsService,
    private propertiesService: PropertiesService,
    private citiesService: CitiesService,
    private tokenStorageService: TokenStorageService,
    private propertyImagesService: PropertyImagesService
  ) {
    this.userId = this.tokenStorageService.getCurrentUserId();

    const params = new GetAllBookingsParams();
    params.userId = this.userId;

    this.bookingsService.getAll(params).subscribe((bookings) => {
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
  }

  public cancelBooking(bookingId: number) {
    let booking = this.data.find((a) => a.id === bookingId);
    let updateModel = new UpdateBookingModel();
    updateModel.cancellationDate = new Date();
    updateModel.BookingState = booking.bookingState;
    updateModel.checkInDate = booking.checkInDate;
    updateModel.checkOutDate = booking.checkOutDate;

    this.bookingsService.put(bookingId, updateModel).subscribe((x) => {
      console.log(x);
      let i = this.data.findIndex((x) => x.id == bookingId);
      this.data[i].cancellationDate = new Date();
    });
  }
}

export class BookingsListElement {
  id: number;
  propertyName: string;
  description: string;
  address: string;
  checkInDate: Date;
  checkOutDate: Date;
  city: string;
  pricePerNight: number;
  totalPrice: number;
  bookingState: string;
  accommodatesNumber: number;
  bathroomNumber: number;
  bedroomNumber: number;
  propertyId: number;
  cancellationDate: Date | null;
  imageUrl;
}
