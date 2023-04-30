using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserManagment
{
    public class ManageUsersModel
    {
        public List<UserDTO> users { get; set; } = new List<UserDTO>();
        public string curUserId { get; set; } = string.Empty;
    }
}
