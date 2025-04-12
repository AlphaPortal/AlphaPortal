using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers;

[Authorize]
public class ClientsController(IClientService clientService) : Controller
{
    private readonly IClientService _clientService = clientService;

    [Route("admin/[controller]")]
    public async Task<IActionResult> Index()
    {
        var clientsResult = await _clientService.GetClientsAsync();
        if (clientsResult.Succeeded)
        {
            var clients = clientsResult.Result;
            if (clients != null)
            {
                var vm = new ClientsViewModel
                {
                    Clients = clients
                };

                return View(vm);
            }

            return BadRequest(clientsResult.Error);
        }

        return View(clientsResult.Error);
    }
}
