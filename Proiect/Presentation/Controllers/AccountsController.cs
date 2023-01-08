using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Data;
using Presentation.Models;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AccountsController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public AccountsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet("{accountId}")]
    public async Task<Account> Get(Guid accountId)
    {
        return await _dbContext.Accounts.FindAsync(accountId);
    }

    [HttpGet]
    public async Task<IEnumerable<Account>> List()
    {
        return await _dbContext.Accounts.ToListAsync();
    }

    [HttpPost]
    public async Task Create([FromBody]Account account)
    {
        await _dbContext.AddAsync(account);
        await _dbContext.SaveChangesAsync();
    }
    
    [HttpPut]
    public async Task Edit([FromBody]Account account)
    {
        
        var entity = await _dbContext.Accounts.FindAsync(account.Id);
        _dbContext.Entry(entity).CurrentValues.SetValues(account);
        await _dbContext.SaveChangesAsync();
    }

    [HttpDelete("{accountId}")]
    public async Task Delete(Guid accountId)
    {
        await _dbContext.Accounts.Where(x => x.Id == accountId)
            .ExecuteDeleteAsync();
    }
    

}