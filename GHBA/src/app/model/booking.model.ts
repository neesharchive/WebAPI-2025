export interface Booking {
  bookingID: number;
  userID: number;
  bedID: number;
  checkInDate: string;
  checkoutDate: string;
  purpose: string;
  gender: number; // 0 = Male, 1 = Female
  status: number; // 0 = Pending, 1 = Approved, 2 = Rejected
  roomNumber: number;
  bedNumber: number;
  guestHouseName: string;
  location: string;
}
