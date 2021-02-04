import { BookingsListService } from './../../../core/services/bookings-list.service';
import {
  BookingsService,
  UpdateBookingModel,
} from './../../../core/services/bookings.service';
import { Component } from '@angular/core';
import { BookingsListElement } from '../my-bookings/my-bookings.component';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';
import { BookingState } from 'src/app/shared/models/bookingResponse';
import { ThemePalette } from '@angular/material/core';
import { ProgressSpinnerMode } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-my-properties-bookings',
  templateUrl: './my-properties-bookings.component.html',
  styleUrls: ['./my-properties-bookings.component.css'],
})
export class MyPropertiesBookingsComponent {
  public userId: number;
  public myPropertiesBookings: BookingsListElement[] = [];
  public isLoading = true;

  color: ThemePalette = 'primary';
  mode: ProgressSpinnerMode = 'determinate';
  value = 50;

  myPropertiesBookingsDisplayedColumns: string[] = [
    'propertyName',
    'city',
    'address',
    'checkInDate',
    'checkOutDate',
    'totalPrice',
    'details',
    'actions',
  ];

  constructor(
    private bookingsService: BookingsService,
    private tokenStorageService: TokenStorageService,
    private bookingsListService: BookingsListService
  ) {
    this.userId = this.tokenStorageService.getCurrentUserId();

    this.bookingsListService.getMyPropertiesBookings().subscribe((bookings) => {
      this.myPropertiesBookings = bookings;
      console.log(this.myPropertiesBookings);
      this.isLoading = false;
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
