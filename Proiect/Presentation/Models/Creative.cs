using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class Creative
{
    public Guid Id { get; set; }
    
    public Guid AccountId { get; set; }
    
    public Account Account { get; set; }
    
    [Required, MaxLength(50), MinLength(5)]
    public string Name { get; set; }
    
    public CreativeType Type { get; set; }
    
    [Required, MaxLength(50), MinLength(5)]
    public Uri SourceUrl { get; set; }
}

public enum CreativeType
{
    RealTimeBidding,
    ThirdParty,
    Media
}