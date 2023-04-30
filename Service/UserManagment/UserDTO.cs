using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserManagment
{
    public class UserDTO
    {
        public string Id { get; set; } = "NaN";
        public string UserName { get; set; } = "NaN";
        public string FirstName { get; set; } = "NaN";
        public string Role { get; set; } = "NaN";

        public UserDTO(string id, ShablonContext _context)
        {
            ShablonUser user = _context.Users.Where(x => x.Id == id).First();
            string RoleId = _context.UserRoles.Where(x => x.UserId== user.Id).First().RoleId;
            IdentityRole identityRole = _context.Roles.Where(x => x.Id == RoleId).First(); 
            if (user != null) 
            {
                Id = user.Id;
                UserName = user.UserName!;
                FirstName = user.UserName!;
                if (identityRole != null)
                {
                    Role = identityRole.Name!;
                }
            } 
        }

        public static UserDTO GetUserDTO(string id, ShablonContext _context)
        {
            return new UserDTO(id, _context);
        }
        public UserDTO(ShablonUser user, ShablonContext _context)
        {
            string RoleId = _context.UserRoles.Where(x => x.UserId == user.Id).First().RoleId;
            IdentityRole identityRole = _context.Roles.Where(x => x.Id == RoleId).First();
            if (user != null)
            {
                Id = user.Id;
                UserName = user.UserName!;
                FirstName = user.UserName!;
                if (identityRole != null)
                {
                    Role = identityRole.Name!;
                }
            }
        }

        public static List<UserDTO> GetAllUsersDTO(ShablonContext _context)
        {
            List<ShablonUser> users = _context.Users.ToList();
            return users.Select(x => new UserDTO(x, _context)).ToList();
        }

        public static void ChangeRole(string id, string role, ShablonContext _context)
        {
            ShablonUser user = _context.Users.Where(x => x.Id == id).First();
            string roleId = _context.Roles.Where(x => x.Name == role).First().Id;
            _context.UserRoles.Remove(_context.UserRoles.Where(x=>x.UserId == id).First());
            _context.UserRoles.Add(new IdentityUserRole<string>(){ RoleId=roleId,UserId=id});
            _context.SaveChanges();
        }

    }

    

    
}
