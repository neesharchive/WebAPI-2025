namespace WebAPI_2025.DTOs.DashboardDTO
{
    public class DashboardStatsDTO
    {
        public int AvailableRooms { get; set; }
        public int Reservations { get; set; }
        public int PendingRequests { get; set; }
        public int CheckIns { get; set; }
        public int CheckOuts { get; set; }
    }
}
