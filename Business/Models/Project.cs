using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class Project
{
    public string? Id { get; set; }
    public string? Image { get; set; }
    public string ProjectName { get; set; } = null!;
    public string? Description { get; set; }

    [DataType(DataType.Date)]
    public DateTime? StartDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }
    public decimal? Budget { get; set; }

    [DataType(DataType.Date)]
    public DateTime Created { get; set; } = DateTime.Now;
    public int StatusId { get; set; }
    public string ClientId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    public Client Client { get; set; } = null!;
}

