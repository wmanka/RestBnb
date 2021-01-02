export class SearchModel {
  constructor(
    startDate: Date,
    endDate: Date,
    location: number,
    accommodatesNumber: number
  ) {
    this.startDate = startDate;
    this.endDate = endDate;
    this.location = location;
    this.accommodatesNumber = accommodatesNumber;
  }

  startDate: Date;
  endDate: Date;
  location: number;
  accommodatesNumber: number;
}
