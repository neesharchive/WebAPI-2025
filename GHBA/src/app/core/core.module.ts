import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

// Core layout components
import { HeaderComponent } from './header/header.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { FooterComponent } from './footer/footer.component';
import { MainComponent } from './main/main.component';

// Utility components
import { LoadingOverlayComponent } from './loading-overlay/loading-overlay.component';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';
import { NotificationComponent } from './notification/notification.component';

// Angular Material modules
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatBadgeModule } from '@angular/material/badge';

import { MatMenuModule } from '@angular/material/menu';

// Toaster (optional for later)
import { NotifierModule } from 'angular-notifier';
import { BookingStatusDialogComponent } from './booking-status-dialog/booking-status-dialog.component';

@NgModule({
  declarations: [
    HeaderComponent,
    SidebarComponent,
    FooterComponent,
    MainComponent,
    LoadingOverlayComponent,
    ConfirmDialogComponent,
    NotificationComponent,
    BookingStatusDialogComponent
  ],
  imports: [
    CommonModule,
    RouterModule,

    // Angular Material
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatDialogModule,
    MatProgressSpinnerModule,
    MatBadgeModule,
    MatMenuModule,
    // Toasts (disabled for now)
    NotifierModule.withConfig({
      position: {
        horizontal: { position: 'right' },
        vertical: { position: 'top' }
      },
      theme: 'material',
      behaviour: {
        autoHide: 3000,
        onMouseover: 'pauseAutoHide',
        showDismissButton: true,
        stacking: 4
      }
    })
  ],
  exports: [
    HeaderComponent,
    SidebarComponent,
    FooterComponent,
    MainComponent,
    LoadingOverlayComponent,
    NotificationComponent
  ]
})
export class CoreModule {}
