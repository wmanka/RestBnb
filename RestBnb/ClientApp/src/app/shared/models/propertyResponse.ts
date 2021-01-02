export interface PropertyResponse {
  id: number;
  name: string;
  description: string;
  address: string;
  pricePerNight: number;
  bedroomNumber: number;
  bathroomNumber: number;
  accommodatesNumber: number;
  checkInTime: Date;
  checkOutTime: Date;
  cityId: number;
}
