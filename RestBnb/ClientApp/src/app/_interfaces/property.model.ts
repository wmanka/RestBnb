import { Time } from '@angular/common';

export interface Property {
    id: number,
    name: string;
    description: string;
    address: string;
    pricePerNight: number;
    bedroomNumber: number;
    bathroomNumber: number;
    accommodatesNumber: number;
    startDate: Date;
    endDate: Date;
    checkInTime: Time;
    checkOutTime: Time;
    latitude: string;
    longitude: string;
    cityId: number;
}
