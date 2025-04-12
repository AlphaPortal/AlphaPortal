using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class StatusFactory
{
    public static Status Create(StatusEntity entity)
    {
        var status = new Status
        {
            Id = entity.Id,
            StatusName = entity.StatusName,
        };

        return status;
    }
}
