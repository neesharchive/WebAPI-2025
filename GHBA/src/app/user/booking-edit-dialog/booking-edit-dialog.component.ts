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
  selectedGuestHouse: any;

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

    this.setupCascadingDropdowns();
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

  setupCascadingDropdowns() {
    this.editForm.get('location')?.valueChanges.subscribe(() => {
      const loc = this.editForm.get('location')?.value;
      this.loadGuestHouses(loc);
      this.editForm.patchValue({ guestHouse: null, room: null, bed: null });
      this.rooms = [];
      this.beds = [];
    });

    this.editForm.get('guestHouse')?.valueChanges.subscribe((gh: any) => {
      this.selectedGuestHouse = gh;
      this.editForm.patchValue({ room: null, bed: null });
      this.rooms = [];
      this.beds = [];
      this.tryLoadRooms();
    });

    this.editForm.get('room')?.valueChanges.subscribe(roomId => {
      const checkIn = this.formatDate(this.editForm.get('checkIn')?.value);
      const checkOut = this.formatDate(this.editForm.get('checkOut')?.value);
      if (roomId && checkIn && checkOut) {
        this.bookingService.getAvailableBeds(roomId, checkIn, checkOut).subscribe(res => {
          if (res.success) {
            this.beds = res.data;
          }
        });
      }
    });

    this.editForm.get('checkIn')?.valueChanges.subscribe(() => this.tryLoadRooms());
    this.editForm.get('checkOut')?.valueChanges.subscribe(() => this.tryLoadRooms());
  }

  loadLocations() {
    this.bookingService.getLocations().subscribe(res => {
      if (res.success) this.locations = res.data;
    });
  }

  loadGuestHouses(location: string, booking?: any) {
    this.bookingService.getGuestHousesByLocation(location).subscribe(res => {
      if (res.success) {
        this.guestHouses = res.data;
        if (booking) {
          const matchedGH = this.guestHouses.find(g => g.name === booking.guestHouseName);
          if (matchedGH) {
            this.editForm.get('guestHouse')?.setValue(matchedGH);
            this.tryLoadRooms(booking);
          }
        }
      }
    });
  }

  tryLoadRooms(booking?: any) {
    const gh = this.editForm.get('guestHouse')?.value;
    const guestHouseId = gh?.guestHouseID;
    const checkIn = this.formatDate(this.editForm.get('checkIn')?.value);
    const checkOut = this.formatDate(this.editForm.get('checkOut')?.value);

    if (guestHouseId && checkIn && checkOut) {
      this.bookingService.getAvailableRooms(guestHouseId, checkIn, checkOut).subscribe(roomRes => {
        if (roomRes.success && roomRes.data?.length) {
          this.rooms = roomRes.data;

          if (booking) {
            const matchedRoom = this.rooms.find(r => r.roomNumber === booking.roomNumber);
            if (matchedRoom) {
              this.editForm.get('room')?.setValue(matchedRoom.roomID);
              this.bookingService.getAvailableBeds(matchedRoom.roomID, checkIn, checkOut).subscribe(bedRes => {
                if (bedRes.success) {
                  this.beds = bedRes.data;
                  this.editForm.get('bed')?.setValue(booking.bedID);
                }
              });
            }
          }
        }
      });
    }
  }

  formatDate(date: any): string | null {
    return date ? new Date(date).toISOString().split('T')[0] : null;
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
compareById(a: any, b: any): boolean {
  return a && b && a.guestHouseID === b.guestHouseID;
}

  onCancel() {
    this.dialogRef.close();
  }
}
