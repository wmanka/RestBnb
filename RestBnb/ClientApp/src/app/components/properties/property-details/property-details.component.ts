import { PropertyImagesService } from 'src/app/core/services/images.service';
import {
  BookingsService,
  GetAllBookingsParams,
} from './../../../core/services/bookings.service';
import { CitiesService } from './../../../core/services/cities.service';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PropertyResponse } from 'src/app/shared/models/propertyResponse';
import * as moment from 'moment';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BookingFormModel } from 'src/app/shared/models/bookingFormModel';
import { SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-property-details',
  templateUrl: './property-details.component.html',
  styleUrls: ['./property-details.component.css'],
})
export class PropertyDetailsComponent {
  public property: PropertyResponse;
  public cityName: string;
  public checkIn: string;
  public checkOut: string;
  public minDate: Date = new Date();
  public bookingForm: FormGroup;
  public bookedDates: Date[] = [];
  public startDate: Date;
  public endDate: Date;
  public dateFilter;
  public propertyImages;
  public thumbnails: Array<SafeUrl> = [];

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private citiesService: CitiesService,
    private bookingsService: BookingsService,
    private propertyImagesService: PropertyImagesService,
    private router: Router
  ) {
    this.property = this.route.snapshot.data.property;

    let params;
    this.route.queryParams.subscribe((x) => {
      params = x;
      this.startDate = new Date(params.startDate * 1000);
      this.endDate = new Date(params.endDate * 1000);
    });

    this.checkIn = moment(this.property.checkInTime).format('HH:mm');
    this.checkOut = moment(this.property.checkOutTime).format('HH:mm');
    this.citiesService
      .get(this.property.cityId)
      .subscribe((x) => (this.cityName = x.name));

    this.bookingForm = this.fb.group({
      startDate: [this.startDate, Validators.required],
      endDate: [this.endDate, Validators.required],
    });

    this.propertyImagesService.getAll(this.property.id).subscribe((x) => {
      this.propertyImages = x;

      this.propertyImages.forEach((element) => {
        let imageUrl = 'data:image/jpeg;base64,' + element.image;

        this.thumbnails.push(imageUrl);
      });
    });

    const bookingsParams = new GetAllBookingsParams();
    bookingsParams.propertyId = this.property.id;

    this.bookingsService.getAll(bookingsParams).subscribe((bookings) => {
      bookings.forEach((booking) => {
        const daysBetween = this.enumerateDaysBetweenDates(
          booking.checkInDate,
          booking.checkOutDate
        );

        daysBetween.forEach((date) => {
          this.bookedDates.push(moment(date).toDate());
        });
      });

      this.dateFilter = (d: Date): boolean => {
        return (
          this.bookedDates.findIndex(
            (testDate) => d.toDateString() === testDate.toDateString()
          ) < 0
        );
      };
    });
  }

  public enumerateDaysBetweenDates(from, to) {
    const fromDate = moment(new Date(from)).startOf('day');
    const toDate = moment(new Date(to)).endOf('day');

    const span = moment.duration(toDate.diff(fromDate)).asDays();
    const days = [];
    for (let i = 0; i <= span; i++) {
      days.push(moment(fromDate).add(i, 'day').startOf('day'));
    }
    return days;
  }

  public submit() {
    const model = new BookingFormModel();
    model.checkInDate = this.bookingForm.value.startDate;
    model.checkOutDate = this.bookingForm.value.endDate;
    model.propertyId = this.property.id;
    this.bookingsService.create(model).subscribe((x) => {
      console.log(x);
      this.router.navigate(['/bookings/my-bookings']);
    });
  }

  public calculateTotalPrice(): number {
    const numberOfDays = Math.floor(
      (Date.UTC(
        this.bookingForm.value.endDate.getFullYear(),
        this.bookingForm.value.endDate.getMonth(),
        this.bookingForm.value.endDate.getDate()
      ) -
        Date.UTC(
          this.bookingForm.value.startDate.getFullYear(),
          this.bookingForm.value.startDate.getMonth(),
          this.bookingForm.value.startDate.getDate()
        )) /
        (1000 * 60 * 60 * 24)
    );

    return this.property.pricePerNight * numberOfDays;
  }
}
