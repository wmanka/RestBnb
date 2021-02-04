import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BookingsListElement } from 'src/app/components/dashboard/my-bookings/my-bookings.component';
import { ApiRoutes } from 'src/app/shared/constants/apiRoutes';

@Injectable({
  providedIn: 'root',
})
export class BookingsListService {
  constructor(private http: HttpClient) {}

  public getMyBookings(): Observable<BookingsListElement[]> {
    return this.http.get<BookingsListElement[]>(
      ApiRoutes.BookingsList.GetMyBookings
    );
  }
}
