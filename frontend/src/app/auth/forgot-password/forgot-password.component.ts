import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private notify: NotificationService
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  onSubmit(): void {
  if (this.form.invalid) return;

  const email = this.form.value.email;

  this.authService.requestPasswordReset(email).subscribe({
    next: (res) => {
      if (res.success) {
        this.notify.showSuccess('Reset link sent to your email');
      } else {
        this.notify.showError(res.message || 'Failed to send reset link');
      }
    },
    error: () => {
      this.notify.showError('Something went wrong');
    }
  });
}

}
