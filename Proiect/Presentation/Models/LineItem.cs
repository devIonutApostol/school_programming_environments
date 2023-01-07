using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class LineItem
{
    public Guid Id { get; set; }
    
    [Required, MaxLength(50), MinLength(5)]
    public string Name { get; set; }
    
    public Guid AccountId { get; set; }
    
    public Account Account { get; set; }

    public Guid ContractId { get; set; }
    
    public Contract Contract { get; set; }
    
    public Guid CreativeId { get; set; }
    
    public Creative Creative { get; set; }
    
    public ICollection<TargetingRule> TargetingRules { get; set; }

    public LineItemStatus Status { get; set; }
    
    [Required, Range(0.1, 100)]
    public decimal Cpm { get; set; }
    
}

public enum LineItemStatus
{
    Active,
    Inactive
}