import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserBookingComponent } from './user-booking/user-booking.component';
import { MainComponent } from '../core/main/main.component'; // same layout as admin
import { MyBookingsComponent } from './my-bookings/my-bookings.component';
const routes: Routes = [
  {
    path: '',
    component: MainComponent, // shared layout
    children: [
      { path: 'booking', component: UserBookingComponent },
      { path: 'my-bookings', component: MyBookingsComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
