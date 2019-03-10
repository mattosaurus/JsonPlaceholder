using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JsonPlaceholder.Models;
using JsonPlaceholder.AppCode;

namespace JsonPlaceholder.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Album(int? id)
        {
            List<Album> albums = await Common.GetAlbum();

            return View(albums);
        }

        public IActionResult Photo(int? id)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
