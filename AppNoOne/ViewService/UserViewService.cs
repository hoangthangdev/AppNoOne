using AppNoOne.ViewService.IviewService;
using DTO.OutDTO;
using Services.InterfaceService;
using System.Threading.Tasks;

namespace AppNoOne.ViewService
{
    public class UserViewService : IUserViewService
    {
        private readonly IUser _user;
        public UserViewService(IUser user)
        {
            _user = user;
        }
        public Task<UserDTO> GetUserByUserName(string userName)
        {
            return _user.GetUserByUserName(userName);
        }
    }
}
