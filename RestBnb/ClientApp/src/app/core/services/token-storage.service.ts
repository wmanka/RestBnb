import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TokenStorageService {
  private authStateSource = new Subject<boolean>();
  public authState$ = this.authStateSource.asObservable();

  public redirectUrl: string;

  public getToken(): string {
    return localStorage.getItem('token');
  }

  public signIn(token: string): void {
    localStorage.setItem('token', token);
    this.authStateSource.next(this.isLoggedIn());
  }

  public signOut(): void {
    localStorage.removeItem('token');
    this.authStateSource.next(this.isLoggedIn());
  }

  public isLoggedIn(): boolean {
    return this.getToken() != null;
  }
}
