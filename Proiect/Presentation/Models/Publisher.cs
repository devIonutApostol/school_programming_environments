using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class Publisher
{
    public Guid Id { get; set; }
    
    [Required, MaxLength(50), MinLength(5)]
    public string Name { get; set; }

    [Required, MaxLength(50), MinLength(5), EmailAddress]
    public string Email { get; set; }
}