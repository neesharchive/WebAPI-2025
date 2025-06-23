import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserResponseDTO } from '../model/user-response.model';
import { APIResponse } from '../model/api-response.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = 'https://localhost:7212/api';

  constructor(private http: HttpClient, private router: Router) {}

  login(credentials: { username: string; password: string }): Observable<APIResponse<UserResponseDTO>> {
    return this.http.post<APIResponse<UserResponseDTO>>(`${this.apiUrl}/auth/login`, credentials);
  }

  saveToken(token: string, role: string, userID?: number) {
  localStorage.setItem('jwtToken', token);
  localStorage.setItem('userRole', role);

  // // ✅ Save static username based on role
  // if (role.toLowerCase() === 'admin') {
  //   localStorage.setItem('username', 'admin');
  // } else {
  //   localStorage.setItem('username', 'user');
  // }

  // ✅ Save userId
  if (userID !== undefined) {
    localStorage.setItem('userId', userID.toString());
  } else {
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const idFromToken = payload.UserID || payload.userId || payload.sub;
      if (idFromToken) {
        localStorage.setItem('userId', idFromToken.toString());
      } else {
        console.warn('UserID not found in token payload.');
      }
    } catch (err) {
      console.error('Failed to decode token:', err);
    }
  }
}


  getToken(): string | null {
    return localStorage.getItem('jwtToken');
  }

  getUserRole(): string {
    return localStorage.getItem('userRole') || '';
  }

  getUserId(): number | null {
    const id = localStorage.getItem('userId');
    return id ? parseInt(id, 10) : null;
  }

  logout(): void {
    localStorage.removeItem('jwtToken');
    localStorage.removeItem('userRole');
    localStorage.removeItem('userId');
    this.router.navigate(['/login']);
  }

  // ✅ Updated method to match controller
  requestPasswordReset(email: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/auth/request-reset`, { email });
  }

  resetPassword(token: string, newPassword: string): Observable<APIResponse<any>> {
    return this.http.post<APIResponse<any>>(`${this.apiUrl}/auth/reset-password`, {
      token,
      newPassword
    });
  }

  changePassword(currentPassword: string, newPassword: string): Observable<APIResponse<any>> {
    return this.http.post<APIResponse<any>>(`${this.apiUrl}/auth/change-password`, {
      userId: this.getUserId(), // still optional
      currentPassword,
      newPassword
    });
  }
}
