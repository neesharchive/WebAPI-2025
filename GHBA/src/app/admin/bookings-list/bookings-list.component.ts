import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { BookingService } from 'src/app/services/booking.service';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-bookings-list',
  templateUrl: './bookings-list.component.html',
  styleUrls: ['./bookings-list.component.css']
})
export class BookingsListComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = [
    'position', 'userID', 'guestHouseName', 'roomNumber', 'bedNumber',
    'location', 'checkInDate', 'checkoutDate', 'gender', 'status'
  ];
  dataSource = new MatTableDataSource<any>();
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

  constructor(private bookingService: BookingService) { }
  resetFilters(): void {
    this.searchControl.setValue('');
    this.locationControl.setValue('');
    this.guestHouseControl.setValue('');
    this.statusControl.setValue('');
    this.checkInControl.setValue('');
    this.checkOutControl.setValue('');

    this.applyFilters(); // reapply to show all
  }

  ngOnInit(): void {
    this.bookingService.getAllBookings().subscribe({
      next: (res) => {
        if (res.success && res.data) {
          const bookings = res.data.map((b, i) => ({ ...b, position: i + 1 }));
          this.fullData = bookings;
          this.dataSource.data = bookings;
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
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

  formatGender(gender: number): string {
    return gender === 0 ? 'Male' : gender === 1 ? 'Female' : 'Other';
  }

  formatStatus(status: number): string {
    switch (status) {
      case 0: return 'Pending';
      case 1: return 'Approved';
      case 2: return 'Rejected';
      default: return 'Unknown';
    }
  }

}
