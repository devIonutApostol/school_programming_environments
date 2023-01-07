using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Data;
using Presentation.Models;

namespace Presentation.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountsController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public AccountsController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Account> Get(Guid accountId)
    {
        return await _dbContext.Accounts.FindAsync(accountId);
    }

    public async Task<IEnumerable<Account>> List()
    {
        return await _dbContext.Accounts.ToListAsync();
    }
}