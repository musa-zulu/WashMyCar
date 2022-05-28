using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using WashMyCar.Core.DataContracts;
using WashMyCar.Core.Request;
using WashMyCar.Core.Response;
using WashMyCar.Core.Services;

namespace WashMyCar.Core.Tests.Services
{
    [TestFixture]
    public class ColorServiceTests
    {
        private ColorRequest _request;
        private List<ColorResponse> _availableColors;
        private Mock<IColorRepository> _colorRepositoryMock;
        private ColorService _service;

        [SetUp]
        public void Setup()
        {
            _request = new ColorRequest
            {
                Description = "new color"
            };

            _availableColors = new List<ColorResponse>();

            _colorRepositoryMock = new Mock<IColorRepository>();

            _colorRepositoryMock.Setup(x => x.GetAll())
              .Returns(_availableColors);

            _service = new ColorService(_colorRepositoryMock.Object);
        }

        [Test]
        public void Contruct()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new ColorService(_colorRepositoryMock.Object));
            //---------------Test Result -----------------------
        }

        [Test]
        public void Construct_GivenThatColorRepositoryIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => new ColorService(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("colorRepository", ex.ParamName);
        }

        [Test]
        public void GetAll_GivenNoColorExist_ShouldReturnEmptyList()
        {
            //---------------Set up test pack-------------------            
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            IEnumerable<ColorResponse> results = _service.GetAllColors();
            //---------------Test Result -----------------------
            Assert.IsEmpty(results);            
        }

        [Test]
        public void GetAll_GivenAColorsExist_ShouldReturnThatColor() 
        {
            //---------------Set up test pack-------------------
            _availableColors = GetAvailableColors(1);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            IEnumerable<ColorResponse> results = _service.GetAllColors();
            //---------------Test Result -----------------------
            Assert.NotNull(results);
            Assert.AreEqual(1, results.Count());
            Assert.AreEqual("color 1", results.FirstOrDefault().Description);
        }

        [Test]
        public void GetAll_GivenTwoColorsExist_ShouldReturnThoseColors()
        {
            //---------------Set up test pack-------------------
            _availableColors = GetAvailableColors(2);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            IEnumerable<ColorResponse> results = _service.GetAllColors();
            //---------------Test Result -----------------------
            Assert.NotNull(results);
            Assert.AreEqual(2, results.Count());
            Assert.AreEqual("color 1", results.FirstOrDefault().Description);
        }

        [Test]
        public void GetAll_GivenManyColorsExist_ShouldReturnThoseColors()
        {
            //---------------Set up test pack-------------------
            _availableColors = GetAvailableColors(4);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            IEnumerable<ColorResponse> results = _service.GetAllColors();
            //---------------Test Result -----------------------
            Assert.NotNull(results);
            Assert.AreEqual(4, results.Count());
            Assert.AreEqual("color 4", results.LastOrDefault().Description);
        }

        private List<ColorResponse> GetAvailableColors(int numberOfColors)
        {
            for(int color = 1; color <= numberOfColors; color++)
                _availableColors.Add(new ColorResponse { Description = "color " + color });
            return _availableColors;
        }

    }
}
