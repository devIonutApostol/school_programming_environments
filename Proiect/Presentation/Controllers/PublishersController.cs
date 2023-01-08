using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Data;
using Presentation.Models;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class PublishersController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public PublishersController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet("{publisherId}")]
    public async Task<Publisher> Get(Guid publisherId)
    {
        return await _dbContext.Publishers.FindAsync(publisherId);
    }

    [HttpGet]
    public async Task<IEnumerable<Publisher>> List()
    {
        return await _dbContext.Publishers.ToListAsync();
    }

    [HttpPost]
    public async Task Create([FromBody]Publisher publisher)
    {
        publisher.Id = Guid.NewGuid();
        await _dbContext.AddAsync(publisher);
        await _dbContext.SaveChangesAsync();
    }
    
    [HttpPut]
    public async Task Edit([FromBody]Publisher publisher)
    {
        await _dbContext.Publishers
            .Where(x => x.Id == publisher.Id)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(p => p.Name, publisher.Name)
                    .SetProperty(p => p.Email, publisher.Email)
                    .SetProperty(p => p.Site, publisher.Site)
            );
    }

    [HttpDelete("{publisherId}")]
    public async Task Delete(Guid publisherId)
    {
        await _dbContext.Publishers.Where(x => x.Id == publisherId)
            .ExecuteDeleteAsync();
    }
    

}