// src/app/services/notification.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private baseUrl = 'https://localhost:7212/api/notification';
  private refreshSubject = new Subject<void>();

  constructor(private http: HttpClient) {}

  getUserNotifications(userId: number) {
    return this.http.get(`${this.baseUrl}/user/${userId}`);
  }

  markAsRead(id: number) {
  return this.http.put(`${this.baseUrl}/mark-as-read/${id}`, {});
}

  markAllAsRead(userId: number) {
    return this.http.post(`${this.baseUrl}/mark-all-read/${userId}`, {});
  }

  // 🔁 Used to trigger refresh from other components
  triggerRefresh() {
    this.refreshSubject.next();
  }

  get refreshNotifier() {
    return this.refreshSubject.asObservable();
  }

  // ✅ TEMPORARY: Placeholder methods for toast-style display
  showSuccess(message: string): void {
    console.log('✅', message);
  }

  showError(message: string): void {
    console.error('❌', message);
  }
}
