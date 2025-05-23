using System.ComponentModel;
using System.Runtime.InteropServices;
using WebAPI_2025.Models.Entities;

namespace WebAPI_2025.Repositories
{
    public interface IBedRepository
    {
        public Task Create(Bed bed);
        public Task<List<Bed>> GetByRoomID(int roomID);
        public Task<List<Bed>> GetAvaiablebBeds(int roomID, DateTime CID, DateTime COD);

    }
}
