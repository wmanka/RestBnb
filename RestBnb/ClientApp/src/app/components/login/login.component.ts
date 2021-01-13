import { TokenStorageService } from './../../core/services/token-storage.service';
import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/core/services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  public loginForm: FormGroup;

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private authenticationService: AuthenticationService,
    private tokenStorageService: TokenStorageService
  ) {
    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  public signIn(): void {
    this.authenticationService
      .login(this.loginForm.value.email, this.loginForm.value.password)
      .subscribe((data) => {
        this.tokenStorageService.signIn(data.token);
        this.router.navigate(['/properties']);
      });
  }
}
