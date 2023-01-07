using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class Account
{
    public Guid Id { get; set; }
    
    [Required, MaxLength(50), MinLength(5)]
    public string Name { get; set; }
    
    public ICollection<Contract> Contracts { get; set; }
    
    public ICollection<Creative> Creatives { get; set; }
    
    public ICollection<LineItem> LineItems { get; set; }
    
    public ICollection<TargetingRule> TargetingRules { get; set; }
}