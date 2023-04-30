using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly ShablonContext _context;
        public UserController(ShablonContext context)
        {
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
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult ChangeRole(string id)
        {
            return View(UserDTO.GetUserDTO(id,_context));
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeRoleConfirm(string id, string role)
        {
                       
            try
            {
                if (User.FindFirstValue(ClaimTypes.NameIdentifier) == id || !Enum.GetNames(typeof(Roles)).Contains(role))
                {
                    return View("Error");
                }
                UserDTO.ChangeRole(id,role,_context);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        /*public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
