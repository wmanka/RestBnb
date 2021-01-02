import { PropertyResponse } from 'src/app/shared/models/propertyResponse';

export class PropertyListItem {
  constructor(property: PropertyResponse) {
    this.id = property.id;
    this.name = property.name;
    this.pricePerNight = property.pricePerNight;
    this.accommodatesNumber = property.accommodatesNumber;
  }

  id: number;
  name: string;
  imageUrl: string;
  pricePerNight: number;
  accommodatesNumber: number;
}
