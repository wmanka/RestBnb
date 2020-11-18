import { environment } from '../../../environments/environment';

export class ApiRoutes {
  private static ApiBase = environment.apiUrl;

  static Auth = class {
    public static Login = ApiRoutes.ApiBase + '/auth/login';
    public static Register = ApiRoutes.ApiBase + '/auth/register';
  };
}
