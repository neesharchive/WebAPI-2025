import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { BookingService } from 'src/app/services/booking.service';
import { NotificationService } from 'src/app/services/notification.service';
import { FormControl } from '@angular/forms';
@Component({
  selector: 'app-manage-bookings',
  templateUrl: './manage-bookings.component.html',
  styleUrls: ['./manage-bookings.component.css']
})
export class ManageBookingsComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = [
    'srNo', 'username', 'guestHouseName', 'checkInDate', 'checkoutDate',
    'roomNumber', 'bedNumber', 'location', 'actions'
  ];

  dataSource = new MatTableDataSource<any>([]);
  fullData: any[] = [];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  // Filters
  searchControl = new FormControl('');
  locationControl = new FormControl('');
  guestHouseControl = new FormControl('');
  statusControl = new FormControl('');
  checkInControl = new FormControl('');
  checkOutControl = new FormControl('');

  locations: string[] = [];
  guestHouses: string[] = [];

  constructor(
    private bookingService: BookingService,
    private notify: NotificationService
  ) {}
resetFilters(): void {
  this.searchControl.setValue('');
  this.locationControl.setValue('');
  this.guestHouseControl.setValue('');
  this.statusControl.setValue('');
  this.checkInControl.setValue('');
  this.checkOutControl.setValue('');
  this.applyFilters();
}

  ngOnInit(): void {
    this.bookingService.getAllBookings().subscribe(res => {
      if (res.success) {
        const pendingOnly = res.data.filter((b: any) => b.status === 0);
        this.fullData = pendingOnly;
        this.dataSource.data = pendingOnly;
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      } else {
        this.notify.showError('Failed to load bookings');
      }
    });

    this.bookingService.getLocations().subscribe(res => {
      if (res.success && res.data) this.locations = res.data;
    });

    this.bookingService.getGuestHouses().subscribe(res => {
      if (res.success && res.data) this.guestHouses = res.data.map(gh => gh.name);
    });

    this.searchControl.valueChanges.subscribe(() => this.applyFilters());
    this.locationControl.valueChanges.subscribe(() => this.applyFilters());
    this.guestHouseControl.valueChanges.subscribe(() => this.applyFilters());
    this.statusControl.valueChanges.subscribe(() => this.applyFilters());
    this.checkInControl.valueChanges.subscribe(() => this.applyFilters());
    this.checkOutControl.valueChanges.subscribe(() => this.applyFilters());
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilters(): void {
    const searchText = this.searchControl.value?.toLowerCase() || '';
    const location = this.locationControl.value;
    const guestHouse = this.guestHouseControl.value;
    const status = this.statusControl.value;
    const checkIn = this.checkInControl.value;
    const checkOut = this.checkOutControl.value;

    this.dataSource.data = this.fullData.filter(item => {
      const matchesSearch = Object.values(item).some(val =>
        val && val.toString().toLowerCase().includes(searchText)
      );
      const matchesLocation = !location || item.location === location;
      const matchesGuestHouse = !guestHouse || item.guestHouseName === guestHouse;
      const matchesStatus = status == null || status === '' || item.status === +status;

      const inDate = new Date(item.checkInDate);
      const outDate = new Date(item.checkoutDate);
      const fromDate = checkIn ? new Date(checkIn) : null;
      const toDate = checkOut ? new Date(checkOut) : null;
      const matchesDateRange = (!fromDate || inDate >= fromDate) &&
                               (!toDate || outDate <= toDate);

      return matchesSearch && matchesLocation && matchesGuestHouse && matchesStatus && matchesDateRange;
    });
  }

  approve(id: number): void {
    this.bookingService.approveBooking(id).subscribe({
      next: () => {
        this.fullData = this.fullData.filter(b => b.bookingID !== id);
        this.applyFilters();
        this.notify.showSuccess('Booking approved');
      },
      error: () => this.notify.showError('Approval failed')
    });
  }

  reject(id: number): void {
    this.bookingService.rejectBooking(id).subscribe({
      next: () => {
        this.fullData = this.fullData.filter(b => b.bookingID !== id);
        this.applyFilters();
        this.notify.showSuccess('Booking rejected');
      },
      error: () => this.notify.showError('Rejection failed')
    });
  }
}
