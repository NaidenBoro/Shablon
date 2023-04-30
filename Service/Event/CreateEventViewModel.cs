using Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Event
{
    public class CreateEventViewModel
    {
        
        public string Name { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public IFormFile? FormFile { get; set; }

        public CreateEventViewModel(AppEvent appEvent)
        {
            Name = appEvent.Name;
            Id = appEvent.Id;
            Description = appEvent.Description;
            Date = appEvent.Date;
        }

        public CreateEventViewModel()
        {
        
        }

    }
}
