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
        public static async Task<List<Album>> GetAlbum()
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

        public static async Task<Album> GetAlbum(int id)
        {
            Album album = new Album();

            HttpClient client = new HttpClient();

            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Format("http://jsonplaceholder.typicode.com/albums/", id.ToString()));

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
    }
}
