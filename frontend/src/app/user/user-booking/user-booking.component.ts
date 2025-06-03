import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BookingService } from 'src/app/services/booking.service';
import { AuthService } from 'src/app/services/auth.service';
import { forkJoin, of } from 'rxjs';
import { switchMap, map, catchError } from 'rxjs/operators';

@Component({
  selector: 'app-user-booking',
  templateUrl: './user-booking.component.html',
  styleUrls: ['./user-booking.component.css']
})
export class UserBookingComponent implements OnInit {
  bookingForm: FormGroup;
  locations: string[] = [];
  guestHouses: any[] = [];
  rooms: any[] = [];
  beds: any[] = [];
  tomorrow: Date = new Date();


  constructor(
    private fb: FormBuilder,
    private bookingService: BookingService,
    private authService: AuthService
  ) {
    this.tomorrow.setDate(this.tomorrow.getDate() + 1);
    this.bookingForm = this.fb.group({
      dates: this.fb.group({
        checkIn: ['', [Validators.required, this.checkInDateValidator]],
        checkOut: ['', Validators.required]
      }),
      location: ['', Validators.required],
      gender: ['', Validators.required],
      guestHouse: [{ value: null, disabled: true }, Validators.required],
      room: [{ value: null, disabled: true }, Validators.required],
      bed: [{ value: null, disabled: true }, Validators.required],
      purpose: [''],
      terms: [false, Validators.requiredTrue]
    });
  }

  ngOnInit(): void {
    this.loadLocations();

    this.bookingForm.get('location')?.valueChanges.subscribe(() => this.tryLoadGuestHouses());
    this.bookingForm.get('dates.checkIn')?.valueChanges.subscribe(() => this.tryLoadGuestHouses());
    this.bookingForm.get('dates.checkOut')?.valueChanges.subscribe(() => this.tryLoadGuestHouses());

    this.bookingForm.get('guestHouse')?.valueChanges.subscribe(() => this.tryLoadRooms());
    this.bookingForm.get('dates.checkIn')?.valueChanges.subscribe(() => this.tryLoadRooms());
    this.bookingForm.get('dates.checkOut')?.valueChanges.subscribe(() => this.tryLoadRooms());

    this.bookingForm.get('room')?.valueChanges.subscribe(roomId => {
      const checkIn = this.formatDate(this.bookingForm.get('dates.checkIn')?.value);
      const checkOut = this.formatDate(this.bookingForm.get('dates.checkOut')?.value);

      if (roomId && checkIn && checkOut) {
        this.bookingService.getAvailableBeds(roomId, checkIn, checkOut).subscribe(res => {
          console.log('[DEBUG] Beds Response:', res);
          if (res.success && res.data?.length) {
            this.beds = res.data;
            this.bookingForm.patchValue({ bed: null });
            this.bookingForm.get('bed')?.enable();
          } else {
            this.beds = [];
            this.bookingForm.patchValue({ bed: null });
            this.bookingForm.get('bed')?.disable();
          }
        });
      } else {
        this.beds = [];
        this.bookingForm.get('bed')?.disable();
      }
    });

  }

  checkInDateValidator(control: AbstractControl): { [key: string]: boolean } | null {
    const today = new Date();
    today.setHours(0, 0, 0, 0);
    const selected = new Date(control.value);
    selected.setHours(0, 0, 0, 0);
    return selected <= today ? { invalidCheckIn: true } : null;
  }

  private tryLoadGuestHouses() {
    const location = this.bookingForm.get('location')?.value;
    const checkIn = this.bookingForm.get('dates.checkIn')?.value;
    const checkOut = this.bookingForm.get('dates.checkOut')?.value;

    if (location && checkIn && checkOut) {
      console.log('[DEBUG] Loading Guest Houses...');
      this.bookingService.getGuestHousesByLocation(location).subscribe(res => {
        if (res.success && res.data?.length) {
          this.guestHouses = res.data;
          this.rooms = [];
          this.beds = [];
          this.bookingForm.patchValue({ guestHouse: null, room: null, bed: null });
          this.bookingForm.get('guestHouse')?.enable();
          this.bookingForm.get('room')?.disable();
          this.bookingForm.get('bed')?.disable();
        } else {
          this.resetFormFields();
        }
      });
    }
  }
  noRoomsInGuestHouse: boolean = false;

  private tryLoadRooms() {
    const guestHouse = this.bookingForm.get('guestHouse')?.value;
    const guestHouseId = guestHouse?.guestHouseID;
    const checkIn = this.formatDate(this.bookingForm.get('dates.checkIn')?.value);
    const checkOut = this.formatDate(this.bookingForm.get('dates.checkOut')?.value);

    if (guestHouseId && checkIn && checkOut) {
      this.bookingForm.get('room')?.enable();
      this.bookingForm.get('bed')?.disable();
      this.bookingForm.patchValue({ room: null, bed: null });
      this.noRoomsInGuestHouse = false;

      this.bookingService.getAvailableRooms(guestHouseId, checkIn, checkOut).pipe(
        switchMap(roomRes => {
          if (!roomRes.success || !roomRes.data?.length) return of([]);

          const roomRequests = roomRes.data.map(room =>
            this.bookingService.getAvailableBeds(room.roomID, checkIn, checkOut).pipe(
              map(bedRes => ({
                room,
                hasBeds: bedRes.success && bedRes.data?.length > 0
              })),
              catchError(() => of({ room, hasBeds: false })) // handle failure gracefully
            )
          );

          return forkJoin(roomRequests);
        }),
        map(results => results.filter(r => r.hasBeds).map(r => r.room))
      ).subscribe(filteredRooms => {
        this.rooms = filteredRooms;
        this.noRoomsInGuestHouse = filteredRooms.length === 0;

        if (filteredRooms.length === 0) {
          this.bookingForm.get('room')?.disable();
          this.bookingForm.get('bed')?.disable();
          this.bookingForm.patchValue({ room: null, bed: null });
        }
      });

    } else {
      this.rooms = [];
      this.beds = [];
      this.bookingForm.get('room')?.disable();
      this.bookingForm.get('bed')?.disable();
      this.bookingForm.patchValue({ room: null, bed: null });
      this.noRoomsInGuestHouse = false;
    }
  }




  private formatDate(date: any): string | null {
    return date ? new Date(date).toISOString().split('T')[0] : null;
  }

  private resetFormFields() {
    this.guestHouses = [];
    this.rooms = [];
    this.beds = [];
    this.bookingForm.patchValue({ guestHouse: null, room: null, bed: null });
    this.bookingForm.get('guestHouse')?.disable();
    this.bookingForm.get('room')?.disable();
    this.bookingForm.get('bed')?.disable();
  }

  loadLocations() {
    this.bookingService.getLocations().subscribe(res => {
      if (res.success) {
        this.locations = res.data;
      }
    });
  }

  onSubmit() {
    console.log('[DEBUG] Booking form submitted');
    if (this.bookingForm.invalid) {
      console.warn('[DEBUG] Booking form invalid:', this.bookingForm.value);
      return;
    }

    const userId = this.getUserIdFromToken();
    if (!userId) {
      console.warn('[DEBUG] User ID could not be extracted from localStorage');
      return;
    }

    const form = this.bookingForm.value;
    const payload = {
      userID: userId,
      bedID: form.bed,
      checkInDate: form.dates.checkIn,
      checkoutDate: form.dates.checkOut,
      purpose: form.purpose,
      gender: form.gender === 'Male' ? 0 : 1,
      location: form.location,
      guestHouseName: form.guestHouse?.gH_Name || ''
    };

    console.log('[DEBUG] Booking Payload:', payload);

    this.bookingService.createBooking(payload).subscribe({
      next: (res) => {
        console.log('[DEBUG] Create Booking Response:', res);
        if (res.success) {
          alert('Booking successful!');
          this.bookingForm.reset();
          this.resetFormFields();
        } else {
          alert('Booking failed: ' + res.message);
        }
      },
      error: (err) => {
        console.error('[ERROR] Booking failed:', err);
        if (err.error?.errors) {
          console.table(err.error.errors);
        } else {
          alert('Booking failed: Unexpected error');
        }
      }
    });
  }

  onCancel() {
    this.bookingForm.reset();
    this.resetFormFields();
  }

  getUserIdFromToken(): number | null {
    const userId = localStorage.getItem('userId');
    return userId ? parseInt(userId) : null;
  }

}
