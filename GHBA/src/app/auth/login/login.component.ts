import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  forgotForm!: FormGroup;
  loading = false;
  errorMessage = '';
  forgotMode = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private notify: NotificationService
  ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });

    this.forgotForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  toggleForgotMode() {
    this.forgotMode = !this.forgotMode;
    this.errorMessage = '';
    this.forgotForm.reset();
  }

  onSubmit() {
    if (this.loginForm.invalid) return;

    this.loading = true;
    const { username, password } = this.loginForm.value;

    this.authService.login({ username, password }).subscribe({
      next: (res: any) => {
        this.loading = false;

        if (res.success && res.data) {
          const token = res.data.token;
          const role = res.data.role;
          const userID = res.data.userID;

          this.authService.saveToken(token, role, userID);

          if (role === 'Admin') {
            this.router.navigate(['/admin/dashboard']);
          } else {
            this.router.navigate(['/user/booking']);
          }
        } else {
          this.errorMessage = res.message || 'Login failed.';
        }
      },
      error: (err) => {
        this.loading = false;
        this.errorMessage = 'Login failed. Please check your credentials.';
        console.error(err);
      }
    });
  }

  sendResetLink(): void {
    if (this.forgotForm.invalid) {
      this.notify.showError('Please enter a valid email.');
      return;
    }

    const email = this.forgotForm.value.email;

    this.authService.requestPasswordReset(email).subscribe({
      next: (res: any) => {
        if (res.success) {
          this.notify.showSuccess(res.message || 'Reset link sent to your email.');
          this.toggleForgotMode();
        } else {
          this.notify.showError(res.message || 'User not found.');
        }
      },
      error: (err) => {
        this.notify.showError('Failed to send reset link.');
        console.error(err);
      }
    });
  }
}
