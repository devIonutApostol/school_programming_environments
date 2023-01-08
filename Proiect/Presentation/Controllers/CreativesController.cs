using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Data;
using Presentation.Models;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CreativesController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public CreativesController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet("{id}")]
    public async Task<Creative> Get(Guid id)
    {
        return await _dbContext.Creatives.FindAsync(id);
    }

    [HttpGet]
    public async Task<IEnumerable<Creative>> List()
    {
        return await _dbContext.Creatives.Include(x => x.Account).ToListAsync();
    }

    [HttpPost]
    public async Task Create([FromBody]Creative creative)
    {
        creative.Id = Guid.NewGuid();
        await _dbContext.AddAsync(creative);
        await _dbContext.SaveChangesAsync();
    }
    
    [HttpPut]
    public async Task Edit([FromBody]Creative creative)
    {
        await _dbContext.Creatives
            .Where(x => x.Id == creative.Id)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(p => p.Name, creative.Name)
                    .SetProperty(p => p.Type, creative.Type)
                    .SetProperty(p => p.SourceUrl, creative.SourceUrl)
                    .SetProperty(p => p.AccountId, creative.AccountId)
            );
    }

    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await _dbContext.Creatives.Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }
    

}