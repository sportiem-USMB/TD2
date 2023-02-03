using Microsoft.VisualStudio.TestTools.UnitTesting;
using TD2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TD2.Models.EntityFramework;

namespace TD2.Controllers.Tests
{
    [TestClass()]
    public class SeriesControllerTests
    {
        private SeriesDBContext _context;
        private SeriesController _controller;

        public SeriesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<SeriesDBContext>().UseNpgsql("Server=localhost;port=5432;Database=SeriesDB; uid=postgres; password=postgres;");
            _context = new SeriesDBContext(builder.Options);
            _controller = new SeriesController(_context);
        }

        [TestMethod()]
        public void GetSerieTest()
        {
            Serie serie = new Serie
            {
                Serieid = 2,
                Titre = "James May's 20th Century",
                Resume = "The world in 1999 would have been unrecognisable to anyone from 1900. James May takes a look at some of the greatest developments of the 20th century, and reveals how they shaped the times we live in now.",
                Nbsaisons = 1,
                Nbepisodes = 6,
                Anneecreation = 2007,
                Network = "BBC Two"
            };

            //Act
            var result = _controller.GetSerie(1);
            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Serie>), "Pas un ActionResult");
            var actionResult = result.Result as ActionResult<Serie>;

            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Serie), "Pas une serie");
            Assert.AreEqual(serie, (Serie)actionResult.Value, "Serie pas identiques");
        }
    }
}