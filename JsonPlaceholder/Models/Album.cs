using JsonPlaceholder.AppCode;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonPlaceholder.Models
{
    public class Album
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("photos")]
        public List<Photo> Photos { get; set; }

        public List<Photo> GetPhotos()
        {
            return Common.GetPhotosByAlbumId(Id).Result;
        }
    }
}
