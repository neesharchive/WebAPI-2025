// src/app/services/booking.service.ts

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { APIResponse } from '../model/api-response.model';
import { GuestHouse } from '../model/guesthouse.model';
import { Room } from '../model/room.model';
import { Bed } from '../model/bed.model';
import { Booking } from '../model/booking.model';

@Injectable({ providedIn: 'root' })
export class BookingService {
  private apiUrl = 'https://localhost:7212/api';

  constructor(private http: HttpClient) { }

  getLocations() {
    return this.http.get<APIResponse<string[]>>(`${this.apiUrl}/guesthouse/locations`);
  }
  getGuestHouses() {
    return this.http.get<APIResponse<GuestHouse[]>>(`${this.apiUrl}/guesthouse`);
  }
  getGuestHousesByLocation(location: string) {
    return this.http.get<APIResponse<GuestHouse[]>>(`${this.apiUrl}/guesthouse/by-location/${location}`);
  }
  updateGuestHouse(id: number, name: string, status: number) {
  const payload = {
    guestHouseID: id,
    name: name,
    status: status
  };
  return this.http.put<APIResponse<GuestHouse>>(`${this.apiUrl}/guesthouse/${id}`, payload);
}


  getRooms(guestHouseId: number) {
    return this.http.get<APIResponse<Room[]>>(`${this.apiUrl}/room/guesthouse/${guestHouseId}`);
  }

  getAvailableRooms(guestHouseId: number, checkIn: string, checkOut: string) {
    return this.http.get<APIResponse<Room[]>>(
      `${this.apiUrl}/room/available?guestHouseId=${guestHouseId}&checkin=${checkIn}&checkout=${checkOut}`
    );
  }

  getBeds(roomId: number) {
    return this.http.get<APIResponse<Bed[]>>(`${this.apiUrl}/bed/room/${roomId}`);
  }

  getAvailableBeds(roomId: number, checkIn: string, checkOut: string) {
    return this.http.get<APIResponse<Bed[]>>(
      `${this.apiUrl}/bed/available?roomId=${roomId}&checkIn=${checkIn}&checkOut=${checkOut}`
    );
  }

  createBooking(payload: any) {
    return this.http.post<APIResponse<any>>(`${this.apiUrl}/booking`, payload);
  }

  getAllBookings() {
    return this.http.get<APIResponse<Booking[]>>(`${this.apiUrl}/booking`);
  }

  approveBooking(id: number) {
    return this.http.put<APIResponse<any>>(`${this.apiUrl}/booking/approve/${id}`, {});
  }

  rejectBooking(id: number) {
    return this.http.put<APIResponse<any>>(`${this.apiUrl}/booking/reject/${id}`, {});
  }
  getBookingsByUser(userId: number) {
    return this.http.get<APIResponse<Booking[]>>(`${this.apiUrl}/booking/user/${userId}`);
  }
  deleteBooking(id: number) {
    return this.http.delete(`${this.apiUrl}/booking/${id}`);
  }
  updateBooking(updatedBooking: any) {
    return this.http.put(`${this.apiUrl}/booking/${updatedBooking.bookingID}`, updatedBooking);
  }
  getBookingById(id: number) {
    return this.http.get<APIResponse<Booking>>(`${this.apiUrl}/booking/${id}`);
  }
  createGuestHouse(payload: any) {
    return this.http.post<APIResponse<any>>(`${this.apiUrl}/guesthouse`, payload);
  }
}
