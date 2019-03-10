using JsonPlaceholder.Controllers;
using JsonPlaceholder.Models;
using JsonPlaceholder.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace JsonPlaceholder.Test
{
    public class ControllerTests
    {
        [Fact]
        public async Task Album_ReturnsAViewResult_WithAListOf2Albums()
        {
            // Arrange
            var mockService = new Mock<IJsonPlaceholderService>();
            mockService.Setup(serv => serv.GetAlbums()).Returns(Task.FromResult(GetTestAlbums()));
            var homeController = new HomeController(mockService.Object);

            // Act
            var result = await homeController.Album(null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Album>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Photo_ReturnsAViewResult_WithAListOf1Album()
        {
            // Arrange
            var mockService = new Mock<IJsonPlaceholderService>();
            mockService.Setup(serv => serv.GetAlbumById(1)).Returns(Task.FromResult(GetTestAlbums(1)));
            mockService.Setup(serv => serv.GetPhotosByAlbumId(1)).Returns(Task.FromResult(GetTestPhotos(1)));
            var homeController = new HomeController(mockService.Object);

            // Act
            var result = await homeController.Photo(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Album>>(
                viewResult.ViewData.Model);
            Assert.Equal(1, model.Count());
        }

        [Fact]
        public async Task Photo_ReturnsAViewResult_WithAListOf2Albums()
        {
            // Arrange
            var mockService = new Mock<IJsonPlaceholderService>();
            mockService.Setup(serv => serv.GetAlbums()).Returns(Task.FromResult(GetTestAlbums()));
            mockService.Setup(serv => serv.GetPhotos()).Returns(Task.FromResult(GetTestPhotos()));
            var homeController = new HomeController(mockService.Object);

            // Act
            var result = await homeController.Photo(null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Album>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Photo_ReturnsAViewResult_WithAListOf1Album_WithAListOf2Photos()
        {
            // Arrange
            var mockService = new Mock<IJsonPlaceholderService>();
            mockService.Setup(serv => serv.GetAlbumById(2)).Returns(Task.FromResult(GetTestAlbums(2)));
            mockService.Setup(serv => serv.GetPhotosByAlbumId(2)).Returns(Task.FromResult(GetTestPhotos(2)));
            var homeController = new HomeController(mockService.Object);

            // Act
            var result = await homeController.Photo(2);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Album>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.First().Photos.Count);
        }

        [Fact]
        public async Task Service_ReturnsAlbumWithCorrectDetails()
        {
            // Arrange
            var mockClient = new Mock<IJsonPlaceholderClient>();
            mockClient.Setup(client => client.GetAlbumById(1)).Returns(Task.FromResult(GetTestAlbums(1)));
            var sut = new JsonPlaceholderService(mockClient.Object);

            // Act
            var result = await sut.GetAlbumById(1);

            // Assert
            Assert.Equal(1, result.Id);
            Assert.Equal(1, result.UserId);
            Assert.Equal("Test Album 2019", result.Title);
        }

        [Fact]
        public async Task Service_ReturnsPhotoWithCorrectDetails()
        {
            // Arrange
            var mockClient = new Mock<IJsonPlaceholderClient>();
            mockClient.Setup(client => client.GetPhotoById(2)).Returns(Task.FromResult(GetTestPhotos(2).Where(x => x.Id == 2).First()));
            var sut = new JsonPlaceholderService(mockClient.Object);

            // Act
            var result = await sut.GetPhotoById(2);

            // Assert
            Assert.Equal(2, result.Id);
            Assert.Equal(2, result.AlbumId);
            Assert.Equal("Test Photo 2", result.Title);
            Assert.Equal(new Uri("https://photo-2.com"), result.Url);
            Assert.Equal(new Uri("https://thumbnail-2.com"), result.ThumbnailUrl);
        }

        private List<Album> GetTestAlbums()
        {
            var albums = new List<Album>();
            albums.Add(new Album()
            {
                UserId = 1,
                Id = 1,
                Title = "Test Album 2019",
                Photos = new List<Photo>()
                {
                    {
                        new Photo()
                        {
                            AlbumId = 1,
                            Id = 1,
                            Title = "Test Photo 1",
                            Url = new Uri("https://photo-1.com"),
                            ThumbnailUrl = new Uri("https://thumbnail-1.com")
                        }
                    },
                    {
                        new Photo()
                        {
                            AlbumId = 1,
                            Id = 2,
                            Title = "Test Photo 2",
                            Url = new Uri("https://photo-2.com"),
                            ThumbnailUrl = new Uri("https://thumbnail-2.com")
                        }
                    }
                }
            });

            albums.Add(new Album()
            {
                UserId = 1,
                Id = 2,
                Title = "Test Album 2020",
                Photos = new List<Photo>()
                {
                    {
                        new Photo()
                        {
                            AlbumId = 2,
                            Id = 3,
                            Title = "Test Photo 3",
                            Url = new Uri("https://photo-3.com"),
                            ThumbnailUrl = new Uri("https://thumbnail-3.com")
                        }
                    },
                    {
                        new Photo()
                        {
                            AlbumId = 2,
                            Id = 4,
                            Title = "Test Photo 4",
                            Url = new Uri("https://photo-4.com"),
                            ThumbnailUrl = new Uri("https://thumbnail-4.com")
                        }
                    }
                }
            });

            return albums;
        }

        private Album GetTestAlbums(int id)
        {
            var album = new Album()
            {
                UserId = 1,
                Id = id,
                Title = "Test Album 2019",
                Photos = new List<Photo>()
                {
                    {
                        new Photo()
                        {
                            AlbumId = id,
                            Id = 1,
                            Title = "Test Photo 1",
                            Url = new Uri("https://photo-1.com"),
                            ThumbnailUrl = new Uri("https://thumbnail-1.com")
                        }
                    },
                    {
                        new Photo()
                        {
                            AlbumId = id,
                            Id = 2,
                            Title = "Test Photo 2",
                            Url = new Uri("https://photo-2.com"),
                            ThumbnailUrl = new Uri("https://thumbnail-2.com")
                        }
                    }
                }
            };

            return album;
        }

        private List<Photo> GetTestPhotos()
        {
            var photos = new List<Photo>();
            photos = new List<Photo>()
            {
                {
                    new Photo()
                    {
                        AlbumId = 1,
                        Id = 1,
                        Title = "Test Photo 1",
                        Url = new Uri("https://photo-1.com"),
                        ThumbnailUrl = new Uri("https://thumbnail-1.com")
                    }
                },
                {
                    new Photo()
                    {
                        AlbumId = 1,
                        Id = 2,
                        Title = "Test Photo 2",
                        Url = new Uri("https://photo-2.com"),
                        ThumbnailUrl = new Uri("https://thumbnail-2.com")
                    }
                },
                {
                    new Photo()
                    {
                        AlbumId = 2,
                        Id = 3,
                        Title = "Test Photo 3",
                        Url = new Uri("https://photo-3.com"),
                        ThumbnailUrl = new Uri("https://thumbnail-3.com")
                    }
                },
                {
                    new Photo()
                    {
                        AlbumId = 2,
                        Id = 4,
                        Title = "Test Photo 4",
                        Url = new Uri("https://photo-4.com"),
                        ThumbnailUrl = new Uri("https://thumbnail-4.com")
                    }
                }
            };

            return photos;
        }

        private List<Photo> GetTestPhotos(int id)
        {
            var photos = new List<Photo>();
            photos = new List<Photo>()
            {
                {
                    new Photo()
                    {
                        AlbumId = id,
                        Id = 1,
                        Title = "Test Photo 1",
                        Url = new Uri("https://photo-1.com"),
                        ThumbnailUrl = new Uri("https://thumbnail-1.com")
                    }
                },
                {
                    new Photo()
                    {
                        AlbumId = id,
                        Id = 2,
                        Title = "Test Photo 2",
                        Url = new Uri("https://photo-2.com"),
                        ThumbnailUrl = new Uri("https://thumbnail-2.com")
                    }
                }
            };

            return photos;
        }
    }
}
