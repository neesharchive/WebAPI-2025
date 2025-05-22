namespace WebAPI_2025.Models.Entities
{
    public class Bed
    {
        public int Id { get; set; }
        public int BedNumber {  get; set; }
        public int RoomID { get; set; }
        public DateTime createdAt { get; private set; }= DateTime.Now;
    }
}
