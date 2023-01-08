using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Data;
using Presentation.Models;

namespace Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class LineItemsController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public LineItemsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet("{id}")]
    public async Task<LineItem> Get(Guid id)
    {
        return await _dbContext.LineItems.FindAsync(id);
    }

    [HttpGet("{id}")]
    public async Task<IEnumerable<LineItem>> List(Guid id)
    {
        return await _dbContext.LineItems
            .Include(x => x.Account)
            .Include(x => x.Contract)
            .Include(x => x.Creative)
            .Include(x => x.TargetingRules)
            .Where(x => x.AccountId == id)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]LineItem entity)
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
    public async Task<IActionResult> Edit([FromBody]LineItem entity)
    {
        await ValidateRelations(entity);
        if (!ModelState.IsValid)
            return ValidationProblem();
        
        await _dbContext.LineItems
            .Where(x => x.Id == entity.Id)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(p => p.Name, entity.Name)
                    .SetProperty(p => p.Status, entity.Status)
                    .SetProperty(p => p.Cpm, entity.Cpm)
                    .SetProperty(p => p.AccountId, entity.AccountId)
                    .SetProperty(p => p.CreativeId, entity.CreativeId)
                    .SetProperty(p => p.ContractId, entity.ContractId)
            );

        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await _dbContext.LineItems.Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }
    
    private async Task ValidateRelations(LineItem entity)
    {
        if (!await _dbContext.Accounts.AnyAsync(x => x.Id == entity.AccountId))
        {
            ModelState.AddModelError("AccountId", "Account does not exists");
        }
        
        if (!await _dbContext.Creatives.AnyAsync(x => x.Id == entity.CreativeId && x.AccountId == entity.AccountId))
        {
            ModelState.AddModelError("CreativeId", "Creative does not exists or does not have the same account");
        }
        
        if (!await _dbContext.Contracts.AnyAsync(x => x.Id == entity.ContractId && x.AccountId == entity.AccountId))
        {
            ModelState.AddModelError("ContractId", "Contract does not exists or does not have the same account");
        }
    }
}