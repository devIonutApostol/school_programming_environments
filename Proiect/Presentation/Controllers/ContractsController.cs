using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Data;
using Presentation.Models;

namespace Presentation.Controllers;

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
        return await _dbContext.Contracts.Include(x => x.Account).ToListAsync();
    }

    [HttpPost]
    public async Task Create([FromBody]Contract entity)
    {
        entity.Id = Guid.NewGuid();
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }
    
    [HttpPut]
    public async Task Edit([FromBody]Contract entity)
    {
        await _dbContext.Contracts
            .Where(x => x.Id == entity.Id)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(p => p.Name, entity.Name)
                    .SetProperty(p => p.Type, entity.Type)
                    .SetProperty(p => p.ContractValue, entity.ContractValue)
                    .SetProperty(p => p.AccountId, entity.AccountId)
            );
    }

    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await _dbContext.Contracts.Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }
}