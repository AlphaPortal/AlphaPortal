using Business.Models;
using Domain.Responses;

namespace Business.Interfaces
{
    public interface IStatusService
    {
        Task<StatusResult<IEnumerable<Status>>> GetStatusesAsync();
    }
}