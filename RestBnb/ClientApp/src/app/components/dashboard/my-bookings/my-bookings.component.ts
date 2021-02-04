import { BookingsListService } from './../../../core/services/bookings-list.service';
import { BookingState } from '../../../shared/models/bookingResponse';
import { CitiesService } from '../../../core/services/cities.service';
import { PropertiesService } from '../../../core/services/properties.service';
import { TokenStorageService } from '../../../core/services/token-storage.service';
import {
  BookingsService,
  UpdateBookingModel,
} from '../../../core/services/bookings.service';
import { Component, OnInit } from '@angular/core';
import { BookingResponse } from 'src/app/shared/models/bookingResponse';
import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';

@Component({
  selector: 'app-my-bookings',
  templateUrl: './my-bookings.component.html',
  styleUrls: ['./my-bookings.component.css'],
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
export class MyBookingsComponent {
  public userId: number;
  public data: BookingsListElement[];
  public expandedElement: BookingResponse | null;
  public isLoading = true;
  public columnsToDisplay = [
    'checkInDate',
    'checkOutDate',
    'bookingState',
    'city',
    'pricePerNight',
    'totalPrice',
  ];

  constructor(
    private bookingsService: BookingsService,
    private bookingsListService: BookingsListService
  ) {
    this.bookingsListService.getMyBookings().subscribe((bookings) => {
      console.log(bookings);
      bookings.map((booking) => {
        if (booking.imageUrl) {
          booking.imageUrl = 'data:image/jpeg;base64,' + booking.imageUrl;
        }
      });
      this.data = bookings;
      this.isLoading = false;
    });
  }

  public cancelBooking(bookingId: number) {
    let booking = this.data.find((a) => a.id === bookingId);
    let updateModel = new UpdateBookingModel();
    updateModel.cancellationDate = new Date();
    updateModel.BookingState = booking.bookingState;
    updateModel.checkInDate = booking.checkInDate;
    updateModel.checkOutDate = booking.checkOutDate;

    this.bookingsService.put(bookingId, updateModel).subscribe((xx) => {
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
  bookingState: BookingState;
  accommodatesNumber: number;
  bathroomNumber: number;
  bedroomNumber: number;
  propertyId: number;
  cancellationDate: Date | null;
  imageUrl;
}
