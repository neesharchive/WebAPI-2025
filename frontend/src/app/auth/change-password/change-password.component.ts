import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html'
})
export class ChangePasswordComponent {
  changeForm: FormGroup;

  constructor(private fb: FormBuilder, private auth: AuthService, private notify: NotificationService) {
    this.changeForm = this.fb.group({
      currentPassword: ['', Validators.required],
      newPassword: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', Validators.required]
    }, { validators: this.matchPasswords });
  }

  matchPasswords(group: FormGroup) {
    return group.get('newPassword')?.value === group.get('confirmPassword')?.value ? null : { mismatch: true };
  }

  submitChange(): void {
    const { currentPassword, newPassword } = this.changeForm.value;
    this.auth.changePassword(currentPassword, newPassword).subscribe({
      next: () => this.notify.showSuccess('Password changed successfully'),
      error: () => this.notify.showError('Failed to change password')
    });
  }
}
