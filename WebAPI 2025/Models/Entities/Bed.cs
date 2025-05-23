using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_2025.Models.Entities
{
    public class Bed
    {
        [Key]
        public int BedID { get; set; }
        public int BedNumber {  get; set; }
        public int RoomID { get; set; }
        public DateTime createdAt { get; private set; }= DateTime.Now;
    }
}
