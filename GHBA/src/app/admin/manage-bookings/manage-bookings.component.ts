import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { BookingService } from 'src/app/services/booking.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-manage-bookings',
  templateUrl: './manage-bookings.component.html',
  styleUrls: ['./manage-bookings.component.css']
})
export class ManageBookingsComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = [
    'srNo',
    'userID',
    'guestHouseName',
    'checkInDate',
    'checkoutDate',
    'roomNumber',
    'bedNumber',
    'location',
    'actions'
  ];

  dataSource = new MatTableDataSource<any>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private bookingService: BookingService,
    private notify: NotificationService
  ) {}

  ngOnInit(): void {
    this.loadBookings();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  loadBookings(): void {
    this.bookingService.getAllBookings().subscribe(res => {
      if (res.success) {
        const pendingOnly = res.data.filter((b: any) => b.status === 0);
        this.dataSource.data = pendingOnly;

        // Optional re-assign paginator/sort
        setTimeout(() => {
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        });
      } else {
        this.notify.showError('Failed to load bookings');
      }
    });
  }

  approve(id: number): void {
    this.bookingService.approveBooking(id).subscribe({
      next: () => {
        this.dataSource.data = this.dataSource.data.filter(b => b.bookingID !== id);
        this.notify.showSuccess('Booking approved');
      },
      error: () => this.notify.showError('Approval failed')
    });
  }

  reject(id: number): void {
    this.bookingService.rejectBooking(id).subscribe({
      next: () => {
        this.dataSource.data = this.dataSource.data.filter(b => b.bookingID !== id);
        this.notify.showSuccess('Booking rejected');
      },
      error: () => this.notify.showError('Rejection failed')
    });
  }
}
