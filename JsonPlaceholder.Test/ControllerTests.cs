using JsonPlaceholder.Controllers;
using JsonPlaceholder.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
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
        public async Task Album_ReturnsAViewResult_WithAListOf100Albums()
        {
            var controller = new HomeController();

            // Act
            var result = await controller.Album(null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Album>>(
                viewResult.ViewData.Model);
            Assert.Equal(100, model.Count());
        }

        [Fact]
        public async Task Photo_ReturnsAViewResult_WithAListOf1Album()
        {
            var controller = new HomeController();

            // Act
            var result = await controller.Photo(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Album>>(
                viewResult.ViewData.Model);
            Assert.Equal(1, model.Count());
        }

        [Fact]
        public async Task Photo_ReturnsAViewResult_WithAListOf100Album()
        {
            var controller = new HomeController();

            // Act
            var result = await controller.Photo(null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Album>>(
                viewResult.ViewData.Model);
            Assert.Equal(100, model.Count());
        }

        [Fact]
        public async Task Photo_ReturnsAViewResult_WithAListOf1Album_WithAListOf50Photos()
        {
            var controller = new HomeController();

            // Act
            var result = await controller.Photo(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Album>>(
                viewResult.ViewData.Model);
            Assert.Equal(50, model.First().Photos.Count);
        }

        [Fact]
        public async Task Photo_ReturnsAViewResult_WithAListOf100Albums_WithAListOf5000Photos()
        {
            var controller = new HomeController();

            // Act
            var result = await controller.Photo(null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Album>>(
                viewResult.ViewData.Model);
            Assert.Equal(5000, model.SelectMany(x => x.Photos).ToList().Count());
        }
    }
}
