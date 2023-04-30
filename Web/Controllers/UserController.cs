using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.UserManagment;
using System.Data;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager ;
        public UserController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        // GET: Users
        public ActionResult Index()
        {
            ManageUsersModel model = new ManageUsersModel()
            {
                users = UserDTO.GetAllUsersDTO(_context).ToList(),
                curUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!
            };
            return View("Users",model);
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            return View(UserDTO.GetUserDTO(id, _context, _userManager));
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View(new UserDTO());
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserDTO userDTO)
        {
            try
            {
                UserDTO.CreateUser(userDTO, _context, _userManager);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            return View(UserDTO.GetUserDTO(id,_context,_userManager));
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserDTO userDTO)
        {
                       
            try
            {
                UserDTO.UpdateUser(userDTO,_context,_userManager);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> DeleteAsync(string id)
        {
            await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id));
            return RedirectToAction(nameof(Index));
        }

        
    }
}
