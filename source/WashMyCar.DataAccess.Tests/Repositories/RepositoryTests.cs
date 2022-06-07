using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WashMyCar.Core.Domain;
using WashMyCar.DataAccess;
using WashMyCar.DataAccess.Repositories;
using WashMyCar.DataAccess.Tests.Helpers;

namespace WashMyCar.DataAccess.Tests.Repositories
{
    /// <summary>
    /// Example of how to create tests for the abstract base class
    /// </summary>
    public class RepositoryTests
    {
        private DbContextOptions<ApplicationDbContext> _options;

        [SetUp]
        public void Setup()
        {            
            _options = ApplicationDbContextHelperTests.ApplicationDbContextOptionsSQLiteInMemory();
            ApplicationDbContextHelperTests.CreateDataBaseSQLiteInMemory(_options);
        }

        [Test]
        public async Task GetAll_ShouldReturnAListOfColors_WhenColorsExist()
        {
            //---------------Set up test pack-------------------      
            await using var context = new ApplicationDbContext(_options);
            var repository = new RepositoryConcreteClass(context);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------            
            var colors = await repository.GetAll();
            //---------------Test Result -----------------------
            Assert.NotNull(colors);
            Assert.IsInstanceOf<List<Color>>(colors);
        }

        [Test]
        public async Task GetAll_ShouldReturnAnEmptyList_WhenColorsDoNotExist()
        {
            //---------------Set up test pack-------------------                              
            await using var context = new ApplicationDbContext(_options);
            var repository = new RepositoryConcreteClass(context);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------  
            var colors = await repository.GetAll();
            //---------------Test Result -----------------------
            Assert.NotNull(colors);
            Assert.IsEmpty(colors);
            Assert.IsInstanceOf<List<Color>>(colors);
        }

        [Test]
        public async Task GetAll_ShouldReturnAListOfColorsWithCorrectValues_WhenColorsExist()
        {
            //---------------Set up test pack-------------------                                  
            await using var context = new ApplicationDbContext(_options);
            ApplicationDbContextHelperTests.CreateData(context);
            var expectedColors = CreateColorsList();
            var repository = new RepositoryConcreteClass(context);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------    
            var colorsList = await repository.GetAll();
            //---------------Test Result -----------------------
            Assert.AreEqual(3, colorsList.Count);
            Assert.AreEqual(expectedColors[0].Id, colorsList[0].Id);
            Assert.AreEqual(expectedColors[0].Description, colorsList[0].Description);
            Assert.AreEqual(expectedColors[1].Id, colorsList[1].Id);
            Assert.AreEqual(expectedColors[1].Description, colorsList[1].Description);
            Assert.AreEqual(expectedColors[2].Id, colorsList[2].Id);
            Assert.AreEqual(expectedColors[2].Description, colorsList[2].Description);
        }

        [Test]
        public async Task GetById_ShouldReturnNull_WhenColorWithSearchedIdDoesNotExist()
        {
            //---------------Set up test pack-------------------            
            await using var context = new ApplicationDbContext(_options);
            var repository = new RepositoryConcreteClass(context);
            //---------------Assert Precondition----------------
            //---------------Execute Test ---------------------- 
            var color = await repository.GetById(0);
            //---------------Test Result -----------------------
            Assert.Null(color);
        }

        [Test]
        public async Task GetById_ShouldReturnColorsWithSearchedId_WhenColorWithSearchedIdExist()
        {
            //---------------Set up test pack-------------------      
            await using var context = new ApplicationDbContext(_options);
            ApplicationDbContextHelperTests.CreateData(context);
            var repository = new RepositoryConcreteClass(context);
            //---------------Assert Precondition----------------
            //---------------Execute Test ---------------------- 
            var color = await repository.GetById(1);
            //---------------Test Result -----------------------
            Assert.NotNull(color);
            Assert.IsInstanceOf<Color>(color);
        }

        [Test]
        public async Task AddColor_ShouldAddColorWithCorrectValues_WhenColorIsValid()
        {
            //---------------Set up test pack-------------------
            Color colorToAdd = new();
            //---------------Assert Precondition----------------
            //---------------Execute Test ---------------------- 
            await using (var context = new ApplicationDbContext(_options))
            {
                var repository = new RepositoryConcreteClass(context);
                colorToAdd = CreateColor();
                await repository.Add(colorToAdd);
            }
            //---------------Test Result -----------------------
            await using (var context = new ApplicationDbContext(_options))
            {
                var colorResult = await context.Colors.Where(b => b.Id == 4).FirstOrDefaultAsync();
                Assert.NotNull(colorResult);
                Assert.IsInstanceOf<Color>(colorToAdd);
                Assert.AreEqual(colorToAdd.Id, colorResult?.Id);
                Assert.AreEqual(colorToAdd.Description, colorResult?.Description);
            }
        }

        [Test]
        public async Task UpdateColor_ShouldUpdateColorWithCorrectValues_WhenColorIsValid()
        {
            //---------------Set up test pack-------------------
            Color colorToUpdate = new();
            await using (var context = new ApplicationDbContext(_options))
            {
                ApplicationDbContextHelperTests.CreateData(context);
                colorToUpdate = await context.Colors.Where(b => b.Id == 1)
                                       .FirstOrDefaultAsync();
                colorToUpdate.Description = "Updated Description";
            }
            //---------------Assert Precondition----------------
            //---------------Execute Test ---------------------- 
            await using (var context = new ApplicationDbContext(_options))
            {
                var repository = new RepositoryConcreteClass(context);
                await repository.Update(colorToUpdate);
            }
            //---------------Execute Test ---------------------- 
            await using (var context = new ApplicationDbContext(_options))
            {
                var updatedColor = await context.Colors.Where(b => b.Id == 1).FirstOrDefaultAsync();

                Assert.NotNull(updatedColor);
                Assert.IsInstanceOf<Color>(updatedColor);
                Assert.AreEqual(colorToUpdate.Id, updatedColor.Id);
                Assert.AreEqual(colorToUpdate.Description, updatedColor.Description);
            }
        }

        [Test]
        public async Task Remove_ShouldRemoveColor_WhenColorIsValid()
        {
            //---------------Set up test pack-------------------
            Color colorToRemove = new();
            await using (var context = new ApplicationDbContext(_options))
            {
                ApplicationDbContextHelperTests.CreateData(context);
                colorToRemove = await context.Colors.Where(c => c.Id == 2).FirstOrDefaultAsync();
            }
            //---------------Assert Precondition----------------
            //---------------Execute Test ---------------------- 
            await using (var context = new ApplicationDbContext(_options))
            {
                var repository = new RepositoryConcreteClass(context);
                await repository.Remove(colorToRemove);
            }
            //---------------Execute Test ---------------------- 
            await using (var context = new ApplicationDbContext(_options))
            {
                var ColorRemoved = await context.Colors.Where(c => c.Id == 2).FirstOrDefaultAsync();
                Assert.Null(ColorRemoved);
            }
        }

        private Color CreateColor()
        {
            return new Color()
            {
                Id = 4,
                Description = "Color Test 4",
            };
        }

        private static List<Color> CreateColorsList()
        {
            return new List<Color>()
            {
                new Color { Id = 1, Description = "Color Test 1" },
                new Color { Id = 2, Description = "Color Test 2" },
                new Color { Id = 3, Description = "Color Test 3" }
            };
        }
    }
}

internal class RepositoryConcreteClass : Repository<Color>
{
    internal RepositoryConcreteClass(ApplicationDbContext bookStoreDbContext) : base(bookStoreDbContext)
    {
    }
}