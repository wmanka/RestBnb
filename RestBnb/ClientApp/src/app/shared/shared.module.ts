import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './components/navbar/navbar.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatMenuModule } from '@angular/material/menu';
import { RouterModule } from '@angular/router';
import { FooterComponent } from './components/footer/footer.component';
import { UploadComponent } from './components/upload/upload.component';

@NgModule({
  declarations: [NavbarComponent, FooterComponent, UploadComponent],
  imports: [
    CommonModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatSidenavModule,
    MatMenuModule,
    RouterModule,
  ],
  exports: [NavbarComponent, FooterComponent, UploadComponent],
})
export class SharedModule {}
