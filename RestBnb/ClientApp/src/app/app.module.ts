import { TokenStorageService } from 'src/app/core/services/token-storage.service';
import { ApiRoutes } from './shared/constants/apiRoutes';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FlexLayoutModule } from '@angular/flex-layout';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { JwtModule } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FlexLayoutModule,
    AppRoutingModule,
    CoreModule,
    SharedModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => {
          return localStorage.getItem('token');
        },
        allowedDomains: [environment.host],
        disallowedRoutes: [
          ApiRoutes.Auth.Login,
          ApiRoutes.Auth.Register,
          ApiRoutes.Cities.GetAll,
        ],
      },
    }),
  ],
  bootstrap: [AppComponent],
  providers: [TokenStorageService],
})
export class AppModule {}
