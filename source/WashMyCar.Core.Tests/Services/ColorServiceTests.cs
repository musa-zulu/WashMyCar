using AutoMapper;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private ColorRequest? _request;
        private ColorService? _service;
        private List<Color>? _availableColors;
        private IColorRepository? _colorRepositoryMock;
        private IMapper? _mapper;

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

            _colorRepositoryMock.GetAllAsync().Returns(_availableColors);

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
        public void Construct_GivenThatMappingEngineIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => new ColorService(_colorRepositoryMock, null));
            //---------------Test Result -----------------------
            Assert.AreEqual("mapper", ex.ParamName);
        }

        [Test]
        public async Task GetAllColorsAsync_GivenNoColorExist_ShouldReturnEmptyList()
        {
            //---------------Set up test pack-------------------            
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await _service?.GetAllColorsAsync();
            //---------------Test Result -----------------------
            Assert.IsEmpty(results);
        }

        [Test]
        public async Task GetAllColorsAsync_GivenAColorsExist_ShouldReturnThatColor()
        {
            //---------------Set up test pack-------------------
            _availableColors = GetAvailableColors(1);
            var list = GetList(1);
            ResolveMapper(list);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await _service?.GetAllColorsAsync();
            //---------------Test Result -----------------------
            Assert.NotNull(results);
            Assert.AreEqual(1, results?.Count());
            Assert.AreEqual("color 1", results?.FirstOrDefault()?.Description);
        }

        [Test]
        public async Task GetAllColorsAsync_GivenTwoColorsExist_ShouldReturnThoseColors()
        {
            //---------------Set up test pack-------------------
            _availableColors = GetAvailableColors(2);
            List<ColorResponse> list = GetList(2);
            ResolveMapper(list);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await _service?.GetAllColorsAsync();
            //---------------Test Result -----------------------
            Assert.NotNull(results);
            Assert.AreEqual(2, results?.Count());
            Assert.AreEqual("color 1", results?.FirstOrDefault()?.Description);
        }

        [Test]
        public async Task GetAllColorsAsync_GivenManyColorsExist_ShouldReturnThoseColors()
        {
            //---------------Set up test pack-------------------
            _availableColors = GetAvailableColors(4);
            var list = GetList(4);
            ResolveMapper(list);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await _service?.GetAllColorsAsync();
            //---------------Test Result -----------------------
            Assert.NotNull(results);
            Assert.AreEqual(4, results?.Count());
            Assert.AreEqual("color 4", results?.LastOrDefault()?.Description);
        }

        [Test]
        public async Task GetAllColorsAsync_ShouldCallGetAll()
        {
            //---------------Set up test pack------------------- 
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _ = await _service?.GetAllColorsAsync();
            //---------------Test Result -----------------------            
            _colorRepositoryMock?.Received(1).GetAllAsync();
        }

        [Test]
        public async Task GetAllColorsAsync_ShouldReturnListOfTypeColorResponse()
        {
            //---------------Set up test pack-------------------
            _availableColors = GetAvailableColors(1);
            List<ColorResponse> list = GetList(1);
            ResolveMapper(list);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _ = await _service?.GetAllColorsAsync();
            //---------------Test Result -----------------------
            Assert.IsInstanceOf<List<ColorResponse>>(list);
        }

        [Test]
        public async Task SaveAsync_GivenValidRequest_ShouldCallSaveFromRepo()
        {
            //---------------Set up test pack-------------------
            Color color = CreateColor();
            _mapper?.Map<Color>(Arg.Any<ColorRequest>())
                .Returns(color);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            await _service?.SaveAsync(_request);
            //---------------Test Result -----------------------            
            _colorRepositoryMock?.Received(1).SaveAsync(Arg.Any<Color>());
        }

        [Test]
        public async Task SaveAsync_GivenValidRequest_ShouldCallMappingEngine()
        {
            //---------------Set up test pack-------------------
            Color color = CreateColor();
            _mapper?.Map<Color>(Arg.Any<ColorRequest>())
                .Returns(color);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            await _service?.SaveAsync(_request);
            //---------------Test Result -----------------------            
            _mapper?.Received(1).Map<Color>(Arg.Any<ColorRequest>());
        }

        [Test]
        public async Task SaveAsync_GivenValidRequestAndObjectIsSaved_ShouldReturnTrue()
        {
            //---------------Set up test pack-------------------
            Color color = CreateColor();
            _mapper?.Map<Color>(Arg.Any<ColorRequest>())
                .Returns(color);
            _colorRepositoryMock?.SaveAsync(Arg.Any<Color>()).Returns(true);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await _service?.SaveAsync(_request);
            //---------------Test Result -----------------------            
            Assert.IsTrue(results);
        }

        [Test]
        public async Task SaveAsync_GivenValidRequestAndObjectIsNotSaved_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            Color color = CreateColor();
            _mapper?.Map<Color>(Arg.Any<ColorRequest>())
                .Returns(color);
            _colorRepositoryMock?.SaveAsync(null).Returns(false);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await _service?.SaveAsync(_request);
            //---------------Test Result -----------------------            
            Assert.IsFalse(results);
        }

        [Test]
        public async Task UpdateAsync_GivenValidRequest_ShouldCallMappingEngine()
        {
            //---------------Set up test pack-------------------            
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            await _service?.UpdateAsync(_request);
            //---------------Test Result -----------------------            
            _mapper?.Received(1).Map<Color>(Arg.Any<ColorRequest>());
        }

        [Test]
        public async Task UpdateAsync_GivenValidRequest_ShouldCallUpdateFromRepo()
        {
            //---------------Set up test pack-------------------
            Color color = CreateColor();
            _mapper?.Map<Color>(Arg.Any<ColorRequest>())
                .Returns(color);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            await _service?.UpdateAsync(_request);
            //---------------Test Result -----------------------            
            _colorRepositoryMock?.Received(1).UpdateAsync(Arg.Any<Color>());
        }

        [Test]
        public async Task UpdateAsync_GivenValidRequest_ShouldUpdateColor()
        {
            //---------------Set up test pack-------------------
            Color color = CreateColor();
            _mapper?.Map<Color>(Arg.Any<ColorRequest>())
                .Returns(color);
            _colorRepositoryMock?.UpdateAsync(color).Returns(true);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await _service?.UpdateAsync(_request);
            //---------------Test Result -----------------------            
            Assert.IsTrue(results);
        }

        [Test]
        public async Task UpdateAsync_GivenValidRequestAndColorWasNotUpdated_ShouldNotUpdateColor()
        {
            //---------------Set up test pack-------------------
            Color color = CreateColor();
            _mapper?.Map<Color>(Arg.Any<ColorRequest>())
                .Returns(color);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await _service?.UpdateAsync(_request);
            //---------------Test Result -----------------------            
            Assert.IsFalse(results);
        }

        [Test]
        public async Task GetColorByIdAsync_GivenValidColorId_ShouldCallGetByIdAsyncFromRepo()
        {
            //---------------Set up test pack-------------------
            var colorId = Guid.NewGuid();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _ = await _service?.GetColorByIdAsync(colorId);
            //---------------Test Result -----------------------            
            _colorRepositoryMock?.Received(1).GetColorByIdAsync(colorId);
        }

        [Test]
        public async Task GetColorByIdAsync_GivenValidResponse_ShouldCallMappingEngine()
        {
            //---------------Set up test pack-------------------
            ColorResponse color = GetList(1)?.FirstOrDefault();
            _mapper?.Map<ColorResponse>(Arg.Any<Color>())
                .Returns(color);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            await _service?.GetColorByIdAsync(color.ColorId);
            //---------------Test Result -----------------------            
            _mapper?.Received(1).Map<ColorResponse>(Arg.Any<Color>());
        }

        [Test]
        public async Task GetColorByIdAsync_GivenColorExistOnRepo_ShouldReturnThatColor()
        {
            //---------------Set up test pack-------------------
            var color = CreateColor();
            var response = new ColorResponse
            {
                ColorId = color.ColorId,
                Description = color.Description
            };
            _mapper?.Map<ColorResponse>(color)
                .Returns(response);

            _colorRepositoryMock.GetColorByIdAsync(color.ColorId).Returns(color);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = await _service?.GetColorByIdAsync(color.ColorId);
            //---------------Test Result -----------------------            
            Assert.IsNotNull(result);
            Assert.AreEqual(response, result);
        }

        [Test]
        public async Task GetColorByIdAsync_ShouldReturnColorOfTypeColorResponse()
        {
            //---------------Set up test pack-------------------
            var color = CreateColor();
            var response = new ColorResponse
            {
                ColorId = color.ColorId,
                Description = color.Description
            };
            _mapper?.Map<ColorResponse>(color)
                .Returns(response);

            _colorRepositoryMock.GetColorByIdAsync(color.ColorId).Returns(color);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = await _service?.GetColorByIdAsync(color.ColorId);
            //---------------Test Result -----------------------
            Assert.IsInstanceOf<ColorResponse>(result);
        }

        [Test]
        public async Task DeleteAsync_GivenValidColorId_ShouldCallGetById()
        {
            //---------------Set up test pack-------------------
            var colorId = Guid.NewGuid();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _ = await _service?.DeleteAsync(colorId);
            //---------------Test Result -----------------------            
            _colorRepositoryMock?.Received(1).GetColorByIdAsync(colorId);
        }

        [Test]
        public async Task DeleteAsync_GivenColorIsNull_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------            
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = await _service?.DeleteAsync(Guid.Empty);
            //---------------Test Result -----------------------            
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteAsync_GivenColorIsNotNull_ShouldCallDeleteAsync()
        {
            //---------------Set up test pack-------------------
            var color = CreateColor();
            _colorRepositoryMock?.GetColorByIdAsync(color.ColorId).
                Returns(color);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _ = await _service?.DeleteAsync(color.ColorId);
            //---------------Test Result -----------------------            
            _colorRepositoryMock?.Received(1).DeleteAsync(color);
        }

        private static Color CreateColor()
        {
            return new Color
            {
                ColorId = Guid.NewGuid(),
                Description = "white",
            };
        }

        private void ResolveMapper(List<ColorResponse> list)
        {
            _mapper?.Map<List<ColorResponse>>(Arg.Any<List<Color>>()).Returns(list);
        }

        private List<Color>? GetAvailableColors(int numberOfColors)
        {
            for (int color = 1; color <= numberOfColors; color++)
                _availableColors?.Add(new Color { Description = "color " + color });
            return _availableColors;
        }

        private static List<ColorResponse> GetList(int records)
        {
            var response = new List<ColorResponse>();
            for (int color = 1; color <= records; color++)
                response.Add(new ColorResponse { Description = "color " + color });
            return response;
        }
    }
}
