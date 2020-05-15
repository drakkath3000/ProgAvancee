using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcMovie.Models
{
    public class SuckleSideViewModel
    {
        public List<Suckle> Suckles { get; set; }
        public SelectList Side { get; set; }
        public string SuckleSide { get; set; }
        public string SearchString { get; set; }
    }
}
