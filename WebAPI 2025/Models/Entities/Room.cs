using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_2025.Models.Entities
{
    public class Room
    {
        [Key]
        public int RoomID { get; set; }
        public int RoomNumber {  get; set; }
        public int Capacity {  get; set; }
        public int guesthouseID { get; set; }
        public DateTime createdAt { get; private set; }= DateTime.Now;
    }
}


