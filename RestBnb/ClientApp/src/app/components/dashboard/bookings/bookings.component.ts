import { BookingState } from './../../../shared/models/bookingResponse';
import { PropertyImagesService } from './../../../core/services/images.service';
import { CitiesService } from './../../../core/services/cities.service';
import {
  PropertiesService,
  GetAllPropertiesParams,
} from './../../../core/services/properties.service';
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
  public myPropertiesBookings: BookingsListElement[] = [];

  public columnsToDisplay = [
    'checkInDate',
    'checkOutDate',
    'bookingState',
    'city',
    'pricePerNight',
    'totalPrice',
  ];
  public expandedElement: BookingResponse | null;

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
    private citiesService: CitiesService,
    private tokenStorageService: TokenStorageService,
    private propertyImagesService: PropertyImagesService
  ) {
    this.userId = this.tokenStorageService.getCurrentUserId();

    this.loadMyPropertiesBookings();
    this.loadMyBookings();
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
      });
  }

  public loadMyBookings() {
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
