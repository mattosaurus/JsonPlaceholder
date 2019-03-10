using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JsonPlaceholder.Models;
using JsonPlaceholder.Services;

namespace JsonPlaceholder.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJsonPlaceholderService _jsonPlaceholderService;

        public HomeController(IJsonPlaceholderService jsonPlaceholderService)
        {
            _jsonPlaceholderService = jsonPlaceholderService;
        }

        public async Task<IActionResult> Album(int? id)
        {
            List<Album> albums = await _jsonPlaceholderService.GetAlbums();

            return View(albums);
        }

        public async Task<IActionResult> Photo(int? albumId)
        {
            List<Photo> photos = new List<Photo>();
            List<Album> albums = new List<Album>();

            if (albumId == null || albumId == 0)
            {
                // Get all photos
                photos = await _jsonPlaceholderService.GetPhotos();

                //Get all albums
                albums = await _jsonPlaceholderService.GetAlbums();
            }
            else
            {
                // Get photos for desired album
                photos = await _jsonPlaceholderService.GetPhotosByAlbumId((int)albumId);

                // Get desired album
                albums = new List<Album>() { await _jsonPlaceholderService.GetAlbumById((int)albumId) };
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
