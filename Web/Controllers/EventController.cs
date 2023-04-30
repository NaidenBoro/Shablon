using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Service.Event;
using System.Data;
using System.Security.Claims;

namespace Web.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        public EventController(AppDbContext context)
        {
            _context = context;
        }
        // GET: EventController
        public ActionResult Index()
        {
            return View(_context.Events.ToList());
        }

        // GET: EventController/Details/5
        public ActionResult Details(string id)
        {

            byte[] byteData = _context.Files.Find(_context.Events.Find(id).ImageID).Content;
            //Convert byte arry to base64string
            string imreBase64Data = Convert.ToBase64String(byteData);
            string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
            //Passing image data in viewbag to view
            ViewBag.ImageData = imgDataURL;
            return View(new CreateEventViewModel(_context.Events.Find(id)));
        }

        // GET: EventController/Create

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View(new CreateEventViewModel());
        }

        public ActionResult Subscribe(string id)
        {
            _context.Bilet.Add(new Bilet() { EventId = id,EventName = _context.Events.Find(id).Name, UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) });
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Bileti()
        {
            return View(_context.Bilet.Where(x=>x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AllBileti()
        {
            return View("Bileti",_context.Bilet);
        }
        // POST: EventController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateUploadAsync(CreateEventViewModel model)
        {
            try
            {
                var img = model.FormFile;
                if (img == null)
                {
                    return View("Create");

                }
                if (!AppEventUtils.CheckSize(img))
                {
                    return View("Create");
                }

                await AppEventUtils.AddEventAndFileAsync(model,_context);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EventController/Edit/5

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            return View(new CreateEventViewModel(_context.Events.Find(id)));
        }

        // POST: EventController/Edit/5

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditEv(CreateEventViewModel model)
        {
            try
            {

                await AppEventUtils.UpdateEventAndFileAsync(model, _context);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: EventController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            _context.Events.Remove(_context.Events.Find(id));
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> RemoveBileti(string id)
        {
            _context.Bilet.Remove(_context.Bilet.Find(id));
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
