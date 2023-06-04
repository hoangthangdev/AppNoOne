using DTO.OutDTO;
using Models;
using Services.IDbContext;
using Services.InterfaceService;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Services.Services
{
    public class UserService : BaseService<AppUsers>, IUser
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserDTO> GetUserByUserName(string userName)
        => await (from u in _context.Users.AsNoTracking()
                  where u.Email == userName || u.UserName == userName
                  let roles = from ur in _context.UserRoles.AsNoTracking()
                              join r in _context.Roles.AsNoTracking()
                              on ur.RoleId equals r.Id
                              where ur.UserId == u.Id
                              select r.Name
                  select new UserDTO
                  {
                      Id = u.Id,
                      UserName = u.UserName,
                      Email = u.Email,
                      Roles = string.Join(',', roles.ToList())
                  }).SingleOrDefaultAsync();
    }
}
