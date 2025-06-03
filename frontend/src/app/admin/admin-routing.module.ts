import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from '../core/main/main.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ManageBookingsComponent } from './manage-bookings/manage-bookings.component';
const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    children: [
      { path: 'dashboard', component: DashboardComponent },
      { path: 'pending-requests', component: ManageBookingsComponent },
      { path: 'master', loadChildren: () => import('./master/master.module').then(m => m.MasterModule) }

    ]
  }

];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
