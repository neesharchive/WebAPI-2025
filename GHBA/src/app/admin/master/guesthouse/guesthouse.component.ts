import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BookingService } from 'src/app/services/booking.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-guesthouse',
  templateUrl: './guesthouse.component.html',
  styleUrls: ['./guesthouse.component.css']
})
export class GuesthouseComponent implements OnInit {
  modeForm!: FormGroup;
  detailsForm!: FormGroup;
  configForm!: FormGroup;

  locations: string[] = [];

  constructor(private fb: FormBuilder, private bookingService: BookingService,private notify: NotificationService) {}

  ngOnInit(): void {
    this.modeForm = this.fb.group({
      action: ['new', Validators.required]
    });

    this.detailsForm = this.fb.group({
      name: ['', Validators.required],
      location: ['', Validators.required]
    });

    this.configForm = this.fb.group({
      numberOfRooms: [1, [Validators.required, Validators.min(1)]],
      bedsPerRoom: [1, [Validators.required, Validators.min(1)]]
    });

    this.fetchLocations();
  }

  fetchLocations(): void {
    this.bookingService.getLocations().subscribe({
      next: (res) => {
        if (res.success && res.data) {
          this.locations = res.data;
        }
      },
      error: (err) => {
        console.error('Failed to fetch locations:', err);
      }
    });
  }

  onSubmit(): void {
    const payload = {
      name: this.detailsForm.value.name,
      location: this.detailsForm.value.location,
      numberOfRooms: this.configForm.value.numberOfRooms,
      bedsPerRoom: this.configForm.value.bedsPerRoom,
      status: 1
    };

    this.bookingService.createGuestHouse(payload).subscribe({
  next: () => this.notify.showSuccess('Guest house created!'),
  error: () => this.notify.showError('Guest house creation failed')
});

  }
}
