using DTO.OutDTO;
using System.Threading.Tasks;

namespace Services.InterfaceService
{
    public interface IUser
    {
        public Task<UserDTO> GetUserByUserName(string userName);
    }
}
