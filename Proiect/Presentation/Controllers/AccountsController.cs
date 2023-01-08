using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Data;
using Presentation.Models;

namespace Presentation.Controllers;

[Authorize]
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
    public async Task<Account> Get(Guid id)
    {
        return await _dbContext.Accounts.FindAsync(id);
    }

    [HttpGet]
    public async Task<IEnumerable<Account>> List()
    {
        return await _dbContext.Accounts.ToListAsync();
    }

    [HttpPost]
    public async Task Create([FromBody]Account account)
    {
        account.Id = Guid.NewGuid();
        await _dbContext.AddAsync(account);
        await _dbContext.SaveChangesAsync();
    }
    
    [HttpPut]
    public async Task Edit([FromBody]Account account)
    {
        await _dbContext.Accounts
            .Where(x => x.Id == account.Id)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(p => p.Name, account.Name)
            );
    }

    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await _dbContext.Accounts.Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }
    

}