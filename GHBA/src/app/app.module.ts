import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { NotifierModule } from 'angular-notifier';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { CoreModule } from './core/core.module';
import { IndicatorsModule } from '@progress/kendo-angular-indicators';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    CoreModule,
    MatIconModule,
    MatTooltipModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
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
    }),
    IndicatorsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
