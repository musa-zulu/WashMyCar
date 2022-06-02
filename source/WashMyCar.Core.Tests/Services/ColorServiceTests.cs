using AutoMapper;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using WashMyCar.Core.DataContracts;
using WashMyCar.Core.Domain;
using WashMyCar.Core.Request;
using WashMyCar.Core.Response;
using WashMyCar.Core.Services;

namespace WashMyCar.Core.Tests.Services
{
    [TestFixture]
    public class ColorServiceTests
    {
        private ColorRequest _request;
        private ColorService _service;
        private List<Color> _availableColors;
        private IColorRepository _colorRepositoryMock;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _request = new ColorRequest
            {
                Description = "new color"
            };

            _availableColors = new List<Color>();

            _mapper = Substitute.For<IMapper>();
            
            _colorRepositoryMock = Substitute.For<IColorRepository>();

            _colorRepositoryMock.GetAll().Returns(_availableColors);

            _service = new ColorService(_colorRepositoryMock, _mapper);
        }

        [Test]
        public void Contruct()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new ColorService(_colorRepositoryMock, _mapper));
            //---------------Test Result -----------------------
        }

        [Test]
        public void Construct_GivenThatColorRepositoryIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => new ColorService(null, _mapper));
            //---------------Test Result -----------------------
            Assert.AreEqual("colorRepository", ex.ParamName);
        }

        [Test]
        public void GetAllColors_GivenNoColorExist_ShouldReturnEmptyList()
        {
            //---------------Set up test pack-------------------            
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            IEnumerable<ColorResponse> results = _service.GetAllColors();
            //---------------Test Result -----------------------
            Assert.IsEmpty(results);
        }

        [Test]
        public void GetAllColors_GivenAColorsExist_ShouldReturnThatColor()
        {
            //---------------Set up test pack-------------------
            _availableColors = GetAvailableColors(1);
            var list = GetList(1);
            ResolveMapper(list);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = _service.GetAllColors();
            //---------------Test Result -----------------------
            Assert.NotNull(results);
            Assert.AreEqual(1, results.Count());
            Assert.AreEqual("color 1", results.FirstOrDefault().Description);
        }
        
        [Test]
        public void GetAllColors_GivenTwoColorsExist_ShouldReturnThoseColors()
        {
            //---------------Set up test pack-------------------
            _availableColors = GetAvailableColors(2);
            List<ColorResponse> list = GetList(2);
            ResolveMapper(list);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = _service.GetAllColors();
            //---------------Test Result -----------------------
            Assert.NotNull(results);
            Assert.AreEqual(2, results.Count());
            Assert.AreEqual("color 1", results.FirstOrDefault().Description);
        }
        
        [Test]
        public void GetAllColors_GivenManyColorsExist_ShouldReturnThoseColors()
        {
            //---------------Set up test pack-------------------
            _availableColors = GetAvailableColors(4);
            var list = GetList(4);
            ResolveMapper(list);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = _service.GetAllColors();
            //---------------Test Result -----------------------
            Assert.NotNull(results);
            Assert.AreEqual(4, results.Count());
            Assert.AreEqual("color 4", results.LastOrDefault().Description);
        }

        [Test]
        public void GetAllColors_ShouldCallGetAll()
        {
            //---------------Set up test pack------------------- 
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _ = _service.GetAllColors();
            //---------------Test Result -----------------------            
            _colorRepositoryMock.Received(1).GetAll();
        }

        [Test]
        public void Save_GivenRequestIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------            
            var ex = Assert.Throws<ArgumentNullException>(() =>
            _service.Save(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("colorRequest", ex.ParamName);
        }

        [Test]
        public void Save_GivenValidRequest_ShouldCallSaveFromRepo()
        {
            //---------------Set up test pack-------------------
            var color = new Color
            {
                Description = "white",
            };
            _mapper.Map<Color>(Arg.Any<ColorRequest>())
                .Returns(color);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _service.Save(_request);
            //---------------Test Result -----------------------            
            _colorRepositoryMock.Received(1).Save(Arg.Any<Color>());
        }

        private void ResolveMapper(List<ColorResponse> list)
        {
            _mapper.Map<List<ColorResponse>>(Arg.Any<List<Color>>()).Returns(list);
        }

        private List<Color> GetAvailableColors(int numberOfColors)
        {
            for (int color = 1; color <= numberOfColors; color++)
                _availableColors.Add(new Color { Description = "color " + color });
            return _availableColors;
        }

        private List<ColorResponse> GetList(int records)
        {
            var response = new List<ColorResponse>();
            for (int color = 1; color <= records; color++)
                response.Add(new ColorResponse { Description = "color " + color });
            return response;            
        }

    }
}
