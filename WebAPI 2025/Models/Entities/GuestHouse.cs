using System.ComponentModel.DataAnnotations;
using WebAPI_2025.Enums;

namespace WebAPI_2025.Models.Entities
{
    public class GuestHouse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }
        public GH_Status Status { get; set; }
        public DateTime CreatedAt { get; private set; } =DateTime.Now;
    }
}
