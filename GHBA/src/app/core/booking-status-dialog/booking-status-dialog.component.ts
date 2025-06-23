import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-booking-status-dialog',
  templateUrl: './booking-status-dialog.component.html',
  styleUrls: ['./booking-status-dialog.component.css']
})
export class BookingStatusDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: { success: boolean; message: string }) {}
}
