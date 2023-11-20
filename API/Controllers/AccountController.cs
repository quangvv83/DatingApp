using API.Data;

namespace API;

public class AccountController : BaseApiController
{
    private readonly DataContext _context;

    public AccountController(DataContext context)
    {
        _context = context;
    }

}
