export class BookingResponse {
  id: number;
  checkInDate: Date;
  checkOutDate: Date;
  pricePerNight: number;
  totalPrice: number;
  bookingState: BookingState;
  userId: number;
  propertyId: number;
  cancellationDate: Date | null;
}

export enum BookingState {
  Pending = 0,
  Accepted = 1,
}
