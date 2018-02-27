using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecamp2018.ViewModels.Session
{
    public class SessionManagement
    {
        public Codecamp2018.Models.Session Session { get; set; }
        // public IQueryable<Codecamp2018.Models.Track> Tracks { get; set; }
        public IEnumerable<SelectListItem> TrackItems { get; set; }
        public IEnumerable<SelectListItem> TimeslotItems { get; set; }
    }
}
