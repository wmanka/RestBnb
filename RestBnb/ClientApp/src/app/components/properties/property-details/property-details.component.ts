import { BookingsService } from './../../../core/services/bookings.service';
import { CitiesService } from './../../../core/services/cities.service';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PropertyResponse } from 'src/app/shared/models/propertyResponse';
import * as moment from 'moment';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BookingFormModel } from 'src/app/shared/models/bookingFormModel';

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
  public bookedDates;
  public startDate: Date;
  public endDate: Date;

  public slides = [
    {
      image:
        'https://www.homekoncept.com.pl/wp-content/uploads/2019/03/HomeKONCEPT_37_zdjecie_1.jpg',
    },
    {
      image:
        'https://lh3.googleusercontent.com/proxy/Ht5Pfx6ALDxM7r_qMYceZYNqDs2YgqpYgnrQslUCe2Dc3Jt-F921i70QpVRovIp80mOenS-M1m6c5Onocga8sKynVgDQGzYrZoDtzw3U723IReBI4wU1',
    },
  ];

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private citiesService: CitiesService,
    private bookingsService: BookingsService
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
  }

  public submit() {
    console.log(this.bookingForm);

    const model = new BookingFormModel();
    model.checkInDate = this.bookingForm.value.startDate;
    model.checkOutDate = this.bookingForm.value.endDate;
    model.propertyId = this.property.id;
    this.bookingsService.create(model).subscribe((x) => console.log(x));
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
