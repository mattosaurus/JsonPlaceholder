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
            List<Album> albums = await Common.GetAlbums();

            return View(albums);
        }

        public async Task<IActionResult> Photo(int? albumId)
        {
            List<Photo> photos = new List<Photo>();
            List<Album> albums = new List<Album>();

            if (albumId == null || albumId == 0)
            {
                // Get all photos
                photos = await Common.GetPhotos();

                //Get all albums
                albums = await Common.GetAlbums();
            }
            else
            {
                // Get photos for desired album
                photos = await Common.GetPhotosByAlbumId((int)albumId);

                // Get desired album
                albums = new List<Album>() { await Common.GetAlbumById((int)albumId) };
            }

            // Merge data
            albums.ForEach(x => x.Photos = photos.Where(y => y.AlbumId == x.Id).ToList());

            return View(albums);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
