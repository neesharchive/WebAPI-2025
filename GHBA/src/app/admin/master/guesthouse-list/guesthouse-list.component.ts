import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { BookingService } from 'src/app/services/booking.service';
import { GuestHouse } from 'src/app/model/guesthouse.model';

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
export class GuesthouseListComponent implements OnInit {
  displayedColumns: string[] = ['position', 'name', 'location', 'numberOfRooms', 'bedsPerRoom', 'statusText', 'actions'];
  dataSource = new MatTableDataSource<DisplayGuestHouse>();

  constructor(private bookingService: BookingService) {}

  ngOnInit(): void {
    this.fetchGuestHouses();
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
          this.dataSource.data = guestHouses;
        }
      },
      error: (err) => {
        console.error('Error fetching guest houses:', err);
      }
    });
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
      next: (response) => {
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

  toggleStatus(element: DisplayGuestHouse) {
    element.editedStatus = element.editedStatus === 1 ? 0 : 1;
  }
}
