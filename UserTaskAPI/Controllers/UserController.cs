using Microsoft.AspNetCore.Mvc;
using UserTaskAPI.Models;

namespace UserTaskAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        [HttpGet("/users/")]
        public IEnumerable<User> Get()
        {
            using (var context = new UserContext())
            {
                return context.Users;
            }
        }
    }
}
