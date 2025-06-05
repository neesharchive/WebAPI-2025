import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from '../core/main/main.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ManageBookingsComponent } from './manage-bookings/manage-bookings.component';
import { GuesthouseComponent } from './master/guesthouse/guesthouse.component';
import { GuesthouseListComponent } from './master/guesthouse-list/guesthouse-list.component';
import { BookingsListComponent } from './bookings-list/bookings-list.component';
const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    children: [
      { path: 'dashboard', component: DashboardComponent },
      { path: 'pending-requests', component: ManageBookingsComponent },
      { path: 'master', loadChildren: () => import('./master/master.module').then(m => m.MasterModule) },
      {path:'report', component:BookingsListComponent}
    ]
  }

];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
