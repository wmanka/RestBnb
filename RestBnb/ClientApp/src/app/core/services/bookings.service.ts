import { HttpClient } from '@angular/common/http';
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
}
