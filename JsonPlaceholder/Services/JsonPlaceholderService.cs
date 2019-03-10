using JsonPlaceholder.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JsonPlaceholder.Services
{
    public interface IJsonPlaceholderService
    {
        Task<List<Album>> GetAlbums();

        Task<Album> GetAlbumById(int id);

        Task<List<Photo>> GetPhotos();

        Task<Photo> GetPhotoById(int id);

        Task<List<Photo>> GetPhotosByAlbumId(int id);
    }

    public class JsonPlaceholderService : IJsonPlaceholderService
    {
        private readonly IJsonPlaceholderClient _jsonPlaceholderClient;

        public JsonPlaceholderService(IJsonPlaceholderClient jsonPlaceholderClient)
        {
            _jsonPlaceholderClient = jsonPlaceholderClient;
        }

        public async Task<List<Album>> GetAlbums()
        {
            return await _jsonPlaceholderClient.GetAlbums();
        }

        public async Task<Album> GetAlbumById(int id)
        {
            return await _jsonPlaceholderClient.GetAlbumById(id);
        }

        public async Task<List<Photo>> GetPhotos()
        {
            return await _jsonPlaceholderClient.GetPhotos();
        }

        public async Task<Photo> GetPhotoById(int id)
        {
            return await _jsonPlaceholderClient.GetPhotoById(id);
        }

        public async Task<List<Photo>> GetPhotosByAlbumId(int id)
        {
            return await _jsonPlaceholderClient.GetPhotosByAlbumId(id);
        }
    }
}
