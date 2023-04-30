using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserManagment
{
    public class UserDTO
    {
        public string Id { get; set; } = "";
        public string UserName { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";

        public UserDTO(string id, AppDbContext _context,UserManager<AppUser> _userManager)
        {
            AppUser user = _context.Users.Where(x => x.Id == id).First();
            string RoleId = _context.UserRoles.Where(x => x.UserId== user.Id).First().RoleId;
            if (user != null) 
            {
                Id = user.Id;
                UserName = user.UserName!;
                FirstName = user.FirstName!;
                LastName = user.LastName!;
                
            } 
        }
        public UserDTO()
        {
        }

        public static UserDTO GetUserDTO(string id, AppDbContext _context, UserManager<AppUser> _userManager)
        {
            return new UserDTO(id, _context, _userManager);
        }
        public UserDTO(AppUser user, AppDbContext _context)
        {
            if (user != null)
            {
                Id = user.Id;
                UserName = user.UserName!;
                FirstName = user.FirstName!;
                LastName = user.LastName!;
                
            }
        }

        public static List<UserDTO> GetAllUsersDTO(AppDbContext _context)
        {
            List<AppUser> users = _context.Users.ToList();
            return users.Select(x => new UserDTO(x, _context)).ToList();
        }

        public static void UpdateUser(UserDTO userDTO, AppDbContext _context, UserManager<AppUser> _userManager)
        {
            AppUser user = _context.Users.Where(x => x.Id == userDTO.Id).First();
            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.UserName = userDTO.UserName;
            _userManager.UpdateAsync(user);
            _context.SaveChanges();
        }
        public static void CreateUser(UserDTO userDTO, AppDbContext _context, UserManager<AppUser> _userManager)
        {
            AppUser user = new AppUser();
            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.UserName = userDTO.UserName;
            _userManager.CreateAsync(user,userDTO.Password);
            _userManager.AddToRoleAsync(user, Roles.Client.ToString());
            _context.SaveChanges();
            return;
        }

    }

    

    
}
