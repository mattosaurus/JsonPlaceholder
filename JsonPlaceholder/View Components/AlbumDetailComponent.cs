using JsonPlaceholder.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonPlaceholder.View_Components
{
    public class AlbumDetailComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Album album)
        {
            return View(album);
        }
    }
}
