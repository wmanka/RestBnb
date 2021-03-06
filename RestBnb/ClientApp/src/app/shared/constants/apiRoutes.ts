import { environment } from '../../../environments/environment';

export class ApiRoutes {
  private static ApiBase = environment.apiUrl;

  static Auth = class {
    public static Login = ApiRoutes.ApiBase + '/auth/login';
    public static Register = ApiRoutes.ApiBase + '/auth/register';
  };

  static Cities = class {
    public static GetAll = ApiRoutes.ApiBase + '/cities';
    public static Get = ApiRoutes.ApiBase + '/cities';
  };

  static Properties = class {
    public static GetAll = ApiRoutes.ApiBase + '/properties';
    public static Create = ApiRoutes.ApiBase + '/properties';
    public static Get = ApiRoutes.ApiBase + '/properties/';
    public static Delete = ApiRoutes.ApiBase + '/properties/';
  };

  static Bookings = class {
    public static GetAll = ApiRoutes.ApiBase + '/bookings';
    public static Create = ApiRoutes.ApiBase + '/bookings';
    public static Put = ApiRoutes.ApiBase + '/bookings/bookingId';
    public static Get = ApiRoutes.ApiBase + '/bookings/';
  };

  static BookingsList = class {
    public static GetMyBookings = ApiRoutes.ApiBase + '/myBookingsList';
    public static GetMyPropertiesBookings =
      ApiRoutes.ApiBase + '/myPropertiesBookingsList';
  };

  static PropertyImages = class {
    public static Create = ApiRoutes.ApiBase + '/properties/propertyId/images';
    public static GetAll = ApiRoutes.ApiBase + '/properties/propertyId/images';
  };
}
