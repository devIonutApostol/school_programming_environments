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

    [HttpPost]
    public async Task Create([FromBody]TargetingRule entity)
    {
        entity.Id = Guid.NewGuid();
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }
    
    [HttpPut]
    public async Task Edit([FromBody]TargetingRule entity)
    {
        await _dbContext.TargetingRules
            .Where(x => x.Id == entity.Id)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(p => p.Name, entity.Name)
                    .SetProperty(p => p.Type, entity.Type)
                    .SetProperty(p => p.AccountId, entity.AccountId)
            );
    }

    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await _dbContext.TargetingRules.Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }
}