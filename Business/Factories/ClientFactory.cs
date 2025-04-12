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
            Image = e.Image,
            ClientName = e.ClientName,
        });

        return clients;
    }

    public static Client Create(ClientEntity entity)
    {
        var client = new Client
        {
            Id = entity.Id,
            Image = entity.Image,
            ClientName = entity.ClientName,
        };

        return client;
    }
}
