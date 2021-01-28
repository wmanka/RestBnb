import { BookingState } from './../../shared/models/bookingResponse';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiRoutes } from 'src/app/shared/constants/apiRoutes';
import { BookingFormModel } from 'src/app/shared/models/bookingFormModel';
import { BookingResponse } from 'src/app/shared/models/bookingResponse';

@Injectable({
  providedIn: 'root',
})
export class BookingsService {
  constructor(private http: HttpClient) {}

  public create(booking: BookingFormModel): Observable<BookingResponse> {
    return this.http.post<BookingResponse>(ApiRoutes.Bookings.Create, booking);
  }

  public getAll(params?: GetAllBookingsParams): Observable<BookingResponse[]> {
    const options = params !== null ? { params: new HttpParams() } : {};

    if (params != null) {
      Object.keys(params).forEach((key) => {
        options.params = options.params.append(key, params[key]);
      });
    }

    return this.http.get<BookingResponse[]>(ApiRoutes.Bookings.GetAll, options);
  }

  public put(
    bookingId: number,
    booking: UpdateBookingModel
  ): Observable<BookingResponse> {
    return this.http.put<BookingResponse>(
      ApiRoutes.Bookings.Put.replace('bookingId', bookingId.toString()),
      booking
    );
  }
}

export class GetAllBookingsParams {
  checkInDate?: Date;
  checkOutDate?: Date;
  bookingState?: BookingState;
  userId?: number;
  propertyId?: number;
}

export class UpdateBookingModel {
  checkInDate: Date;
  checkOutDate: Date;
  BookingState: BookingState;
  cancellationDate: Date;
}
