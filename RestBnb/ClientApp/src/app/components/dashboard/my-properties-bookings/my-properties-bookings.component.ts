import {
  BookingsService,
  GetAllBookingsParams,
  UpdateBookingModel,
} from './../../../core/services/bookings.service';
import { Component } from '@angular/core';
import { BookingsListElement } from '../my-bookings/my-bookings.component';
import {
  GetAllPropertiesParams,
  PropertiesService,
} from 'src/app/core/services/properties.service';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';
import { BookingState } from 'src/app/shared/models/bookingResponse';

@Component({
  selector: 'app-my-properties-bookings',
  templateUrl: './my-properties-bookings.component.html',
  styleUrls: ['./my-properties-bookings.component.css'],
})
export class MyPropertiesBookingsComponent {
  public userId: number;
  public myPropertiesBookings: BookingsListElement[] = [];

  myPropertiesBookingsDisplayedColumns: string[] = [
    'propertyName',
    'city',
    'checkInDate',
    'checkOutDate',
    'totalPrice',
    'details',
    'actions',
  ];

  constructor(
    private bookingsService: BookingsService,
    private propertiesService: PropertiesService,
    private tokenStorageService: TokenStorageService
  ) {
    this.userId = this.tokenStorageService.getCurrentUserId();

    this.loadMyPropertiesBookings();
  }

  public loadMyPropertiesBookings() {
    let getAllPropertiesParams = new GetAllPropertiesParams();
    getAllPropertiesParams.userId = this.userId;

    this.propertiesService
      .getAll(getAllPropertiesParams)
      .subscribe((properties) => {
        properties.forEach((property) => {
          let getAllBookingsParams = new GetAllBookingsParams();
          getAllBookingsParams.propertyId = property.id;

          this.bookingsService
            .getAll(getAllBookingsParams)
            .subscribe((bookings) => {
              bookings.forEach((booking) => {
                let bookingOfMyPropertyListElement = new BookingsListElement();

                bookingOfMyPropertyListElement.bookingState =
                  booking.bookingState;
                bookingOfMyPropertyListElement.checkInDate =
                  booking.checkInDate;
                bookingOfMyPropertyListElement.checkOutDate =
                  booking.checkOutDate;
                bookingOfMyPropertyListElement.id = booking.id;
                bookingOfMyPropertyListElement.pricePerNight =
                  property.pricePerNight;
                bookingOfMyPropertyListElement.propertyId = property.id;
                bookingOfMyPropertyListElement.propertyName = property.name;
                bookingOfMyPropertyListElement.totalPrice = booking.totalPrice;
                bookingOfMyPropertyListElement.cancellationDate =
                  booking.cancellationDate;

                this.myPropertiesBookings.push(bookingOfMyPropertyListElement);
              });
            });
        });

        console.log(this.myPropertiesBookings);
      });
  }

  public cancelBookingAsHost(bookingId: number) {
    let booking = this.myPropertiesBookings.find((b) => b.id === bookingId);
    let updateModel = new UpdateBookingModel();
    updateModel.cancellationDate = new Date();
    updateModel.BookingState = booking.bookingState;
    updateModel.checkInDate = booking.checkInDate;
    updateModel.checkOutDate = booking.checkOutDate;

    this.bookingsService.put(bookingId, updateModel).subscribe(() => {
      let i = this.myPropertiesBookings.findIndex((b) => b.id === bookingId);
      this.myPropertiesBookings[i].cancellationDate = new Date();
    });
  }

  public acceptBooking(bookingId: number) {
    let booking = this.myPropertiesBookings.find((b) => b.id === bookingId);
    let updateModel = new UpdateBookingModel();
    updateModel.BookingState = BookingState.Accepted;
    updateModel.checkInDate = booking.checkInDate;
    updateModel.checkOutDate = booking.checkOutDate;

    this.bookingsService.put(bookingId, updateModel).subscribe((xx) => {
      let i = this.myPropertiesBookings.findIndex((b) => b.id === bookingId);
      this.myPropertiesBookings[i].bookingState = xx.bookingState;
    });
  }
}
