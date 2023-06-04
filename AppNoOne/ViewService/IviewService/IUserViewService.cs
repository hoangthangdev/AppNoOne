using DTO.OutDTO;
using System.Threading.Tasks;

namespace AppNoOne.ViewService.IviewService
{
    public interface IUserViewService
    {
        public Task<UserDTO> GetUserByUserName(string userName);
    }
}
