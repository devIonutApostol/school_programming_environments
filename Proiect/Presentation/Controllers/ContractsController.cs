using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Data;
using Presentation.Models;

namespace Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class ContractsController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public ContractsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet("{id}")]
    public async Task<Contract> Get(Guid id)
    {
        return await _dbContext.Contracts.FindAsync(id);
    }

    [HttpGet]
    public async Task<IEnumerable<Contract>> List()
    {
        return await _dbContext.Contracts
            .Include(x => x.Account)
            .Include(x => x.Publisher)
            .ToListAsync();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]Contract entity)
    {
        await ValidateRelations(entity);
        if (!ModelState.IsValid)
            return ValidationProblem();
        
        entity.Id = Guid.NewGuid();
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> Edit([FromBody]Contract entity)
    {
        await ValidateRelations(entity);
        if (!ModelState.IsValid)
            return ValidationProblem();
        
        await _dbContext.Contracts
            .Where(x => x.Id == entity.Id)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(p => p.Name, entity.Name)
                    .SetProperty(p => p.Type, entity.Type)
                    .SetProperty(p => p.ContractValue, entity.ContractValue)
                    .SetProperty(p => p.AccountId, entity.AccountId)
            );

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await _dbContext.Contracts.Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }
    
    private async Task ValidateRelations(Contract entity)
    {
        if (!await _dbContext.Accounts.AnyAsync(x => x.Id == entity.AccountId))
        {
            ModelState.AddModelError("AccountId", "Account does not exists");
        }
        
        if (!await _dbContext.Publishers.AnyAsync(x => x.Id == entity.PublisherId))
        {
            ModelState.AddModelError("PublisherId", "Publisher does not exists");
        }
    }
}