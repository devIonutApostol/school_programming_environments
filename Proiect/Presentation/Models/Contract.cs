using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class Contract
{
    public Guid Id { get; set; }
    
    public Guid AccountId { get; set; }
    
    public Account Account { get; set; }
    
    public Guid PublisherId { get; set; }
    
    public Publisher Publisher { get; set; }
    
    [Required, MaxLength(50), MinLength(5)]
    public string Name { get; set; }
    
    [Required]
    public ContractType Type { get; set; }
    
    [Required]
    public decimal ContractValue { get; set; }
}

public enum ContractType
{
    RevShare,
    FixedCpm,
    ViewableCpm
}