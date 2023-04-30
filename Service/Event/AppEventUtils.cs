using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Event
{
    public static class AppEventUtils
    {
        public static async Task<bool> AddEventAndFileAsync(CreateEventViewModel model,AppDbContext _context)
        {
            try 
            {
                var image = model.FormFile;
                string imageId = "";
                using (var memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);

                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        var file = new AppFile()
                        {
                            Content = memoryStream.ToArray()
                        };
                        imageId = file.Id;
                        _context.Files.Add(file);

                        AppEvent ev = new AppEvent() 
                        {
                            Name = model.Name,
                            Description = model.Description,
                            Image = file,
                            ImageID= imageId,
                            date= model.Date
                        };
                        _context.Events.Add(ev);

                        await _context.SaveChangesAsync();
                    }/*
                    else
                    {
                        ModelState.AddModelError("File", "The file is too large.");
                    }*/
                }

                return true;
            }
            catch (Exception ex) 
            {
            return false;
            }
        }

        public static bool CheckSize(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);

                // Upload the file if less than 2 MB
                return memoryStream.Length < 2097152;
            }
        }
    }

}
