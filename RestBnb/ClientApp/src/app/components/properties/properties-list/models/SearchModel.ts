export class SearchModel {
  constructor(
    startDate: Date,
    endDate: Date,
    location: string,
    numberOfGuests: number
  ) {
    this.startDate = startDate;
    this.endDate = endDate;
    this.location = location;
    this.numberOfGuests = numberOfGuests;
  }

  startDate: Date;
  endDate: Date;
  location: string;
  numberOfGuests: number;
}
