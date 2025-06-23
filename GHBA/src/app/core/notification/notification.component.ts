import { Component, OnInit } from '@angular/core';
import { NotificationService } from 'src/app/services/notification.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit {
  notifications: any[] = [];
  unreadCount: number = 0;
  dropdownOpen: boolean = false;
  role: string = '';

  constructor(
    private notificationService: NotificationService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.role = localStorage.getItem('userRole') || '';
    this.fetchNotifications();

    this.notificationService.refreshNotifier.subscribe(() => {
      this.fetchNotifications();
    });
  }

  fetchNotifications() {
    const userId = Number(localStorage.getItem('userId'));
    if (!userId) return;

    this.notificationService.getUserNotifications(userId).subscribe({
      next: (res: any) => {
        this.notifications = res.data || [];
        this.unreadCount = this.notifications.filter(n => !n.isRead).length;
      },
      error: () => {
        this.notifications = [];
      }
    });
  }

  toggleDropdown() {
    this.dropdownOpen = !this.dropdownOpen;
  }

  handleNotificationClick(notification: any) {
    this.notificationService.markAsRead(notification.notificationID).subscribe(() => {
      this.removeNotification(notification.notificationID);
      this.redirectBasedOnRole(notification.message);
    });
  }

  dismissNotification(event: MouseEvent, notification: any) {
    event.stopPropagation();
    this.notificationService.markAsRead(notification.notificationID).subscribe(() => {
      this.removeNotification(notification.notificationID);
    });
  }

  removeNotification(id: number) {
    this.notifications = this.notifications.filter(n => n.notificationID !== id);
    this.unreadCount = this.notifications.filter(n => !n.isRead).length;
  }

  redirectBasedOnRole(message: string) {
    if (this.role === 'Admin') {
      // Redirect admin to pending approval page
      this.router.navigate(['/admin/pending-requests']);
    } else {
      // Regular user goes to their own bookings page
      this.router.navigate(['/user/my-bookings']);
    }
  }

}
