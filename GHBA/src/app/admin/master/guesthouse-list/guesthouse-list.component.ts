import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { BookingService } from 'src/app/services/booking.service';
import { GuestHouse } from 'src/app/model/guesthouse.model';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from 'src/app/core/confirm-dialog/confirm-dialog.component';
import { FormControl } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';

interface DisplayGuestHouse extends GuestHouse {
  position: number;
  statusText: string;
  isEditing?: boolean;
  editedName?: string;
  editedStatus?: number;
}

@Component({
  selector: 'app-guesthouse-list',
  templateUrl: './guesthouse-list.component.html',
  styleUrls: ['./guesthouse-list.component.css']
})
export class GuesthouseListComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = [
    'position',
    'name',
    'location',
    'numberOfRooms',
    'bedsPerRoom',
    'statusText',
    'actions'
  ];

  dataSource = new MatTableDataSource<DisplayGuestHouse>();
  fullData: DisplayGuestHouse[] = [];

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  searchControl = new FormControl('');
  locationControl = new FormControl('');
  statusControl = new FormControl('');
  locations: string[] = [];

  constructor(private bookingService: BookingService, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.fetchGuestHouses();

    this.searchControl.valueChanges.subscribe(() => this.applyFilters());
    this.locationControl.valueChanges.subscribe(() => this.applyFilters());
    this.statusControl.valueChanges.subscribe(() => this.applyFilters());
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }

  fetchGuestHouses() {
    this.bookingService.getGuestHouses().subscribe({
      next: (response) => {
        if (response.success && response.data) {
          const guestHouses: DisplayGuestHouse[] = response.data.map((gh, index) => ({
            ...gh,
            position: index + 1,
            statusText: gh.status === 1 ? 'Active' : 'Inactive',
            editedName: gh.name,
            editedStatus: gh.status
          }));
          this.fullData = guestHouses;
          this.dataSource.data = guestHouses;

          this.locations = [...new Set(guestHouses.map(gh => gh.location))];
          if (this.paginator) this.dataSource.paginator = this.paginator;
        }
      },
      error: (err) => console.error('Error fetching guest houses:', err)
    });
  }

  applyFilters(): void {
    const search = this.searchControl.value?.toLowerCase() || '';
    const location = this.locationControl.value;
    const status = this.statusControl.value;

    const filtered = this.fullData.filter(item => {
      const matchesSearch = Object.values(item).some(val =>
        val && val.toString().toLowerCase().includes(search)
      );
      const matchesLocation = !location || item.location === location;
      const matchesStatus = status === '' || item.status === Number(status);

      return matchesSearch && matchesLocation && matchesStatus;
    });

    this.dataSource.data = filtered;
    if (this.paginator) this.dataSource.paginator?.firstPage();

  }

  resetFilters(): void {
    this.searchControl.setValue('');
    this.locationControl.setValue('');
    this.statusControl.setValue('');
    this.applyFilters();
  }

  enableEdit(element: DisplayGuestHouse) {
    element.isEditing = true;
  }

  cancelEdit(element: DisplayGuestHouse) {
    element.isEditing = false;
    element.editedName = element.name;
    element.editedStatus = element.status;
  }

  saveEdit(element: DisplayGuestHouse) {
    const updatedName = element.editedName!;
    const updatedStatus = element.editedStatus!;

    this.bookingService.updateGuestHouse(element.guestHouseID, updatedName, updatedStatus).subscribe({
      next: () => {
        element.name = updatedName;
        element.status = updatedStatus;
        element.statusText = updatedStatus === 1 ? 'Active' : 'Inactive';
        element.isEditing = false;
      },
      error: (err) => {
        console.error('Update failed:', err);
        alert('Failed to update guest house');
      }
    });
  }

  deleteGuestHouse(element: DisplayGuestHouse) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '350px',
      data: {
        title: 'Confirm Deletion',
        message: `Are you sure you want to delete "${element.name}"?`
      }
    });

    dialogRef.afterClosed().subscribe(confirmed => {
      if (confirmed) {
        this.bookingService.deleteGuestHouse(element.guestHouseID).subscribe({
          next: () => {
this.fullData = this.fullData.filter((g: DisplayGuestHouse) => g.guestHouseID !== element.guestHouseID);
            this.applyFilters();
          },
          error: (err) => {
            console.error('Delete failed:', err);
            alert('Failed to delete guest house');
          }
        });
      }
    });
  }

  toggleStatus(element: DisplayGuestHouse) {
    element.editedStatus = element.editedStatus === 1 ? 0 : 1;
  }
}
