using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ClientFactory
{
    public static IEnumerable<Client> Create(IEnumerable<ClientEntity> entity)
    {
        var clients = entity.Select(e => new Client
        {
            Id = e.Id,
            ClientName = e.ClientName,
        });

        return clients;
    }

    public static Client Create(ClientEntity entity)
    {
        var client = new Client
        {
            Id = entity.Id,
            ClientName = entity.ClientName,
        };

        return client;
    }
}
