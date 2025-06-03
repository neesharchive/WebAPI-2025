import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BookingService } from 'src/app/services/booking.service';

@Component({
  selector: 'app-booking-edit-dialog',
  templateUrl: './booking-edit-dialog.component.html',
  styleUrls: ['./booking-edit-dialog.component.css']
})
export class BookingEditDialogComponent implements OnInit {
  editForm!: FormGroup;
  locations: string[] = [];
  guestHouses: any[] = [];
  rooms: any[] = [];
  beds: any[] = [];
  tomorrow: Date = new Date();

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<BookingEditDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private bookingService: BookingService
  ) {
    this.tomorrow.setDate(this.tomorrow.getDate() + 1);
  }

  ngOnInit(): void {
    this.initForm();
    this.loadLocations();

    this.bookingService.getBookingById(this.data.bookingID).subscribe(res => {
      if (res.success) {
        const booking = res.data;

        this.editForm.patchValue({
          checkIn: new Date(booking.checkInDate),
          checkOut: new Date(booking.checkoutDate),
          location: booking.location,
          gender: booking.gender === 0 ? 'Male' : 'Female',
          purpose: booking.purpose,
          terms: true
        });

        this.loadGuestHouses(booking.location, booking);
      }
    });
  }

  initForm() {
    this.editForm = this.fb.group({
      checkIn: [null, Validators.required],
      checkOut: [null, Validators.required],
      location: [null, Validators.required],
      gender: [null, Validators.required],
      guestHouse: [null, Validators.required],
      room: [null, Validators.required],
      bed: [null, Validators.required],
      purpose: [''],
      terms: [true, Validators.requiredTrue]
    });
  }

  loadLocations() {
    this.bookingService.getLocations().subscribe(res => {
      if (res.success) this.locations = res.data;
    });
  }

  loadGuestHouses(location: string, booking: any) {
    this.bookingService.getGuestHousesByLocation(location).subscribe(res => {
      if (res.success) {
        this.guestHouses = res.data;
        const matchedGH = this.guestHouses.find(g => g.gH_Name === booking.guestHouseName);
        if (matchedGH) {
          this.editForm.get('guestHouse')?.setValue(matchedGH);
          this.loadRooms(matchedGH.guestHouseID, booking);
        }
      }
    });
  }

  loadRooms(guestHouseId: number, booking: any) {
    this.bookingService.getRooms(guestHouseId).subscribe(res => {
      if (res.success) {
        this.rooms = res.data;
        const matchedRoom = this.rooms.find(r => r.roomNumber === booking.roomNumber);
        if (matchedRoom) {
          this.editForm.get('room')?.setValue(matchedRoom.roomID);
          this.loadBeds(matchedRoom.roomID, booking);
        }
      }
    });
  }

  loadBeds(roomId: number, booking: any) {
    this.bookingService.getBeds(roomId).subscribe(res => {
      if (res.success) {
        this.beds = res.data;
        this.editForm.get('bed')?.setValue(booking.bedID);
      }
    });
  }

  compareById(opt: any, val: any): boolean {
    return opt?.guestHouseID === val?.guestHouseID || opt === val;
  }

  onUpdate() {
    if (this.editForm.invalid) return;

    const form = this.editForm.value;
    const payload = {
      bookingID: this.data.bookingID,
      userID: this.data.userID,
      bedID: form.bed,
      checkInDate: form.checkIn,
      checkoutDate: form.checkOut,
      purpose: form.purpose,
      gender: form.gender === 'Male' ? 0 : 1
    };

    this.dialogRef.close(payload);
  }

  onCancel() {
    this.dialogRef.close();
  }
}
