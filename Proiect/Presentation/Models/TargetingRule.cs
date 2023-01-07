using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Presentation.Models;

public class TargetingRule
{
    public Guid Id { get; set; }
    
    public Guid AccountId { get; set; }
    
    public Account Account { get; set; }
    
    public TargetingRuleType Type { get; set; }
    
    [Required, MaxLength(50), MinLength(5)]
    public string Value { get; set; }
    
    public ICollection<LineItem> LineItems { get; set; }

}

public enum TargetingRuleType
{
    Country,
    Device,
    UserAgent,
    Domain
}