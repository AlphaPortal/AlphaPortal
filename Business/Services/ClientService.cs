using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Domain.Responses;

namespace Business.Services;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;

    public async Task<ClientResult<IEnumerable<Client>>> GetClientsAsync()
    {
        var result = await _clientRepository.GetAllAsync
            (
            orderByDescending: false,
            sortByColumn: c => c.ClientName
            );

        if (result != null)
        {
            var entities = result.Result;
            if (entities != null)
            {
                var clients = ClientFactory.Create(entities);
                return new ClientResult<IEnumerable<Client>> { Succeeded = true, StatusCode = 200, Result = clients };
            }
        }

        return new ClientResult<IEnumerable<Client>> { Succeeded = false, StatusCode = 404, Error = "No clients found." };
    }

    public async Task<ClientResult<Client>> GetClientByIdAsync(string id)
    {
        var result = await _clientRepository.GetAsync(c => c.Id == id);
        if (result != null)
        {
            var entity = result.Result;
            if (entity != null)
            {
                var client = ClientFactory.Create(entity);
                return new ClientResult<Client> { Succeeded = true, StatusCode = 200, Result = client };
            }
        }

        return new ClientResult<Client> { Succeeded = false, StatusCode = 404, Error = $"Client with id '{id}' was not found." };
    }
}
