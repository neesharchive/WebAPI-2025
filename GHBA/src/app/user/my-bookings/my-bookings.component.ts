import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { BookingService } from 'src/app/services/booking.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { BookingEditDialogComponent } from '../booking-edit-dialog/booking-edit-dialog.component';

@Component({
  selector: 'app-my-bookings',
  templateUrl: './my-bookings.component.html',
  styleUrls: ['./my-bookings.component.css']
})
export class MyBookingsComponent implements OnInit {
  displayedColumns: string[] = [
    'srNo',
    'guestHouseName',
    'checkInDate',
    'checkoutDate',
    'roomNumber',
    'bedNumber',
    'location',
    'status',
    'actions'
  ];
  dataSource = new MatTableDataSource<any>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private bookingService: BookingService, private dialog: MatDialog) { }

  ngOnInit(): void {
    const userId = parseInt(localStorage.getItem('userId') || '0');
    if (!userId) return;

    this.bookingService.getBookingsByUser(userId).subscribe(res => {
      if (res.success) {
        this.dataSource = new MatTableDataSource(res.data);
        setTimeout(() => {
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        });

      }
    });
  }

  cancel(id: number) {
    this.bookingService.deleteBooking(id).subscribe(() => {
      this.dataSource.data = this.dataSource.data.filter(b => b.bookingID !== id);
    });
  }

  update(booking: any) {
    const dialogRef = this.dialog.open(BookingEditDialogComponent, {
      width: '70vw',
      data: booking
    });

    dialogRef.afterClosed().subscribe(updated => {
      if (updated) {
        this.bookingService.updateBooking(updated).subscribe(() => {
          this.dataSource.data = this.dataSource.data.map(b =>
            b.bookingID === updated.bookingID ? { ...b, ...updated } : b
          );
        });
      }
    });
  }



}
