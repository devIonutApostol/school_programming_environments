using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Data;
using Presentation.Models;

namespace Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class TargetingRulesController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public TargetingRulesController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet("{id}")]
    public async Task<TargetingRule> Get(Guid id)
    {
        return await _dbContext.TargetingRules.FindAsync(id);
    }

    [HttpGet]
    public async Task<IEnumerable<TargetingRule>> List()
    {
        return await _dbContext.TargetingRules.Include(x => x.Account).ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<IEnumerable<TargetingRule>> ListForAccount(Guid accountId)
    {
        return await _dbContext.TargetingRules
            .Include(x => x.Account)
            .Where(x => x.AccountId == accountId)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]TargetingRule entity)
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
    public async Task<IActionResult> Edit([FromBody]TargetingRule entity)
    {
        await ValidateRelations(entity);
        if (!ModelState.IsValid)
            return ValidationProblem();
        
        await _dbContext.TargetingRules
            .Where(x => x.Id == entity.Id)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(p => p.Name, entity.Name)
                    .SetProperty(p => p.Type, entity.Type)
                    .SetProperty(p => p.AccountId, entity.AccountId)
            );

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await _dbContext.TargetingRules.Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }
    
    private async Task ValidateRelations(TargetingRule entity)
    {
        if (!await _dbContext.Accounts.AnyAsync(x => x.Id == entity.AccountId))
        {
            ModelState.AddModelError("AccountId", "Account does not exists");
        }
    }
}