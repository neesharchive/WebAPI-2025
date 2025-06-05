import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { APIResponse } from '../model/api-response.model';
export interface DashboardStats {
  availableRooms: number;
  reservations: number;
  pendingRequests: number;
  checkIns: number;
  checkOuts: number;
}

export interface BedStats {
  availableBeds: number;
  occupiedBeds: number;
  checkIns: number;
  totalNights: number;
}

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private baseUrl = 'https://localhost:7212/api/dashboard';

  constructor(private http: HttpClient) { }

  getDashboardStats(): Observable<APIResponse<DashboardStats>> {
    return this.http.get<APIResponse<DashboardStats>>(`${this.baseUrl}/stats`);
  }

  getBedStats(startDate: string, endDate: string): Observable<APIResponse<BedStats>> {
    return this.http.get<APIResponse<BedStats>>(
      `${this.baseUrl}/bed-stats?startDate=${startDate}&endDate=${endDate}`
    );
  }
}
