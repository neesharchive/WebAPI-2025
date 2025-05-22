namespace WebAPI_2025.Models.Entities
{
    public class Room
    {
        public int ID { get; set; }
        public int RoomNumber {  get; set; }
        public int Capacity {  get; set; }
        public int guesthouseID { get; set; }
        public DateTime createdAt { get; private set; }= DateTime.Now;

    }
}
