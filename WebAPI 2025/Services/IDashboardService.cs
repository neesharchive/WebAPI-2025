using WebAPI_2025.Data.DashboardDTO;
using WebAPI_2025.Models.Wrappers;

namespace WebAPI_2025.Services
{
    public interface IDashboardService
    {
        Task<APIResponse<DashboardStatsDTO>> GetAdminStatsAsync();
        Task<APIResponse<BedStatsDTO>> GetBedStatsAsync(DateTime startDate, DateTime endDate);
    }
}
