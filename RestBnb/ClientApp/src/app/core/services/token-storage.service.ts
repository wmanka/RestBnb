import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TokenStorageService {
  public redirectUrl: string;

  public getToken(): string {
    return localStorage.getItem('token');
  }

  public signIn(token: string): void {
    localStorage.setItem('token', token);
  }

  public signOut(): void {
    localStorage.removeItem('token');
  }

  public isLoggedIn(): boolean {
    return this.getToken() != null;
  }
}
