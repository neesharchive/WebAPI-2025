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
  originalData: any[] = [];

  filter = {
    guestHouse: '',
    location: '',
    status: '',
    checkIn: '',
    checkOut: ''
  };

  guestHouseNames: string[] = [];
  locations: string[] = [];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private bookingService: BookingService, private dialog: MatDialog) {}

  ngOnInit(): void {
    const userId = parseInt(localStorage.getItem('userId') || '0');
    if (!userId) return;

    this.bookingService.getBookingsByUser(userId).subscribe(res => {
      if (res.success) {
        this.originalData = res.data;
        this.dataSource = new MatTableDataSource(this.originalData);
        this.guestHouseNames = [...new Set(res.data.map(b => b.guestHouseName))];
        this.locations = [...new Set(res.data.map(b => b.location))];
        setTimeout(() => {
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        });
      }
    });
  }

  applyFilters(): void {
    this.dataSource.data = this.originalData.filter(b => {
      const matchesGuestHouse = !this.filter.guestHouse || b.guestHouseName === this.filter.guestHouse;
      const matchesLocation = !this.filter.location || b.location === this.filter.location;
      const matchesStatus =
  this.filter.status === '' || this.filter.status === null || this.filter.status === undefined
    ? true
    : b.status === +this.filter.status;

      const matchesCheckIn = !this.filter.checkIn || new Date(b.checkInDate).toDateString() === new Date(this.filter.checkIn).toDateString();
      const matchesCheckOut = !this.filter.checkOut || new Date(b.checkoutDate).toDateString() === new Date(this.filter.checkOut).toDateString();

      return matchesGuestHouse && matchesLocation && matchesStatus && matchesCheckIn && matchesCheckOut;
    });
  }

  cancel(id: number) {
    this.bookingService.deleteBooking(id).subscribe(() => {
      this.originalData = this.originalData.filter(b => b.bookingID !== id);
      this.applyFilters();
    });
  }

  update(booking: any) {
    const dialogRef = this.dialog.open(BookingEditDialogComponent, {
      width: '70vw',
      data: booking
    });

    dialogRef.afterClosed().subscribe(updated => {
      if (updated) {
        this.bookingService.updateBooking(updated).subscribe({
          next: () => {
            this.bookingService.getBookingsByUser(booking.userID).subscribe(res => {
              if (res.success) {
                this.originalData = res.data;
                this.applyFilters();
              }
            });
          },
          error: (err) => {
            console.error('Booking update failed:', err);
            alert('Booking update failed. Please try again.');
          }
        });
      }
    });
  }
}
