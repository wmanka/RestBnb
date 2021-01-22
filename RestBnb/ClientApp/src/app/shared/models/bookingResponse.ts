export class BookingResponse {
  id: number;
  checkInDate: Date;
  checkOutDate: Date;
  pricePerNight: number;
  totalPrice: number;
  bookingState: string;
  userId: number;
  propertyId: number;
}
