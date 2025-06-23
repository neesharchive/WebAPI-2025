import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GuesthouseComponent } from './guesthouse/guesthouse.component';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { MatStepperModule } from '@angular/material/stepper';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { MatButtonModule } from '@angular/material/button';
import { GuesthouseListComponent } from './guesthouse-list/guesthouse-list.component';
import { MatTableModule } from '@angular/material/table';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';

const routes: Routes = [
  { path: '', redirectTo: 'guesthouse', pathMatch: 'full' },
  { path: 'guesthouse', component: GuesthouseComponent },
  {path:'gh-list',component:GuesthouseListComponent}
  // { path: 'location', component: LocationComponent },
  // { path: 'rules', component: RulesComponent }
];
@NgModule({
  declarations: [GuesthouseComponent, GuesthouseListComponent],
  imports: [
    MatPaginatorModule,
    MatIconModule,
    MatTableModule,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MatStepperModule,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    MatRadioModule,
    MatButtonModule,
    RouterModule.forChild(routes)
  ]
})
export class MasterModule {}
