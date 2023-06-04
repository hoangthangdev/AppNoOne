
using DTO.OutDTO;

namespace AppNoOne.MiddelWere
{
    public interface IJwtAuthenticationManager
    {
        public string CreateToken(UserDTO userDTO);
    }
}
