import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  public isLoggedIn: boolean;

  constructor(
    private router: Router,
    private tokenService: TokenStorageService
  ) {
    this.tokenService.authState$.subscribe((state) => {
      this.isLoggedIn = state;
    });
  }

  public ngOnInit(): void {
    this.isLoggedIn = this.tokenService.isLoggedIn();
  }

  public logout(): void {
    this.tokenService.signOut();
    this.router.navigate(['/properties']);
  }
}
