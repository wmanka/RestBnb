import { environment } from '../../../environments/environment';

export class ApiRoutes {
  private static ApiBase = environment.apiUrl;

  static Auth = class {
    public static Login = ApiRoutes.ApiBase + '/auth/login';
    public static Register = ApiRoutes.ApiBase + '/auth/register';
  };

  static Cities = class {
    public static GetAll = ApiRoutes.ApiBase + '/cities';
  };

  static Properties = class {
    public static GetAll = ApiRoutes.ApiBase + '/properties';
  };
}
