using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Domain.Responses;

namespace Business.Services;

public class StatusService(IStatusRepository statusRepository) : IStatusService
{
    private readonly IStatusRepository _statusRepository = statusRepository;

    public async Task<StatusResult<IEnumerable<Status>>> GetStatusesAsync()
    {
        var result = await _statusRepository.GetAllAsync();
        if (result.Result != null)
        {
            var statuses = new List<Status>();

            foreach (var status in result.Result)
            {
                statuses.Add(StatusFactory.Create(status));
            }

            return new StatusResult<IEnumerable<Status>> { Succeeded = true, StatusCode = 200, Result = statuses };
        }

        return new StatusResult<IEnumerable<Status>> { Succeeded = false, StatusCode = 404, Error = result.Error };
    }
}
