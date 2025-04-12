using Business.Models;

namespace Presentation.Models;

public class ClientsViewModel
{
    public IEnumerable<Client> Clients { get; set; } = [];
}
