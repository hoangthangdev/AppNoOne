using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.OutDTO
{
    public class UserDTO
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public long UpdateAt { get; set; }

        public long DeletedAt { get; set; }

        public string Roles { get; set; }

        public int Status { get; set; }
    }
}
