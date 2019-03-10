using JsonPlaceholder.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JsonPlaceholder.AppCode
{
    public static class Common
    {
        public static async Task<List<Album>> GetAlbums()
        {
            List<Album> albums = new List<Album>();

            HttpClient client = new HttpClient();

            try
            {
                HttpResponseMessage response = await client.GetAsync("http://jsonplaceholder.typicode.com/albums");
                
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    string message = String.Format("GET failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                byte[] responseByteArray = await response.Content.ReadAsByteArrayAsync();
                string responseString = Encoding.UTF8.GetString(responseByteArray, 0, responseByteArray.Length);

                albums = JsonConvert.DeserializeObject<List<Album>>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return albums;
        }

        public static async Task<Album> GetAlbumById(int id)
        {
            Album album = new Album();

            HttpClient client = new HttpClient();

            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Format("http://jsonplaceholder.typicode.com/albums/{0}", id.ToString()));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    string message = String.Format("GET failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                byte[] responseByteArray = await response.Content.ReadAsByteArrayAsync();
                string responseString = Encoding.UTF8.GetString(responseByteArray, 0, responseByteArray.Length);

                album = JsonConvert.DeserializeObject<Album>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return album;
        }

        public static async Task<List<Photo>> GetPhotos()
        {
            List<Photo> photos = new List<Photo>();

            HttpClient client = new HttpClient();

            try
            {
                HttpResponseMessage response = await client.GetAsync("http://jsonplaceholder.typicode.com/photos");

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    string message = String.Format("GET failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                byte[] responseByteArray = await response.Content.ReadAsByteArrayAsync();
                string responseString = Encoding.UTF8.GetString(responseByteArray, 0, responseByteArray.Length);

                photos = JsonConvert.DeserializeObject<List<Photo>>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return photos;
        }

        public static async Task<Photo> GetPhotoById(int id)
        {
            Photo photo = new Photo();

            HttpClient client = new HttpClient();

            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Format("https://jsonplaceholder.typicode.com/photos/{0}", id.ToString()));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    string message = String.Format("GET failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                byte[] responseByteArray = await response.Content.ReadAsByteArrayAsync();
                string responseString = Encoding.UTF8.GetString(responseByteArray, 0, responseByteArray.Length);

                photo = JsonConvert.DeserializeObject<Photo>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return photo;
        }

        public static async Task<List<Photo>> GetPhotosByAlbumId(int id)
        {
            List<Photo> photos = new List<Photo>();

            HttpClient client = new HttpClient();

            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Format("https://jsonplaceholder.typicode.com/photos?albumId={0}", id.ToString()));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    string message = String.Format("GET failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                byte[] responseByteArray = await response.Content.ReadAsByteArrayAsync();
                string responseString = Encoding.UTF8.GetString(responseByteArray, 0, responseByteArray.Length);

                photos = JsonConvert.DeserializeObject<List<Photo>>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return photos;
        }
    }
}
