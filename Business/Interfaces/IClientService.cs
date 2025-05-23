﻿using Business.Models;
using Domain.Responses;

namespace Business.Interfaces
{
    public interface IClientService
    {
        Task<ClientResult<Client>> GetClientByIdAsync(string id);
        Task<ClientResult<IEnumerable<Client>>> GetClientsAsync();
    }
}