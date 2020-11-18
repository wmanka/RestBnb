import { ApiRoutes } from './../../shared/constants/apiRoutes';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthResponse } from 'src/app/shared/models/authResponse';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  constructor(private http: HttpClient) {}

  public login(email: string, password: string): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(ApiRoutes.Auth.Login, {
      email,
      password,
    });
  }

  public register(email: string, password: string): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(ApiRoutes.Auth.Register, {
      email,
      password,
    });
  }
}
