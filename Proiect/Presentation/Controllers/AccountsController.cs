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
    private readonly ApplicationDbContext _dbContext;

    public AccountsController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet("{accountId}")]
    public async Task<Account> Get(Guid accountId)
    {
        return await _dbContext.Accounts.FindAsync(accountId);
    }

    [HttpGet]
    public async Task<IEnumerable<Account>> List1()
    {
        return await _dbContext.Accounts.ToListAsync();
    }
    
    [HttpGet]
    public  IEnumerable<Account> List()
    {
        return _dbContext.Accounts.ToList();
    }
}