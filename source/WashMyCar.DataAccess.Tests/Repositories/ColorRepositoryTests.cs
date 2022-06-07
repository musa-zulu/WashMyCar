using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using WashMyCar.Core.Domain;
using WashMyCar.DataAccess.Repositories;
using WashMyCar.DataAccess.Tests.Helpers;

namespace WashMyCar.DataAccess.Tests.Repositories
{
    [TestFixture]
    public class ColorRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public ColorRepositoryTests()
        {
            // For SQLite InMemory database option
            _options = ApplicationDbContextHelperTests.ApplicationDbContextOptionsSQLiteInMemory();
            ApplicationDbContextHelperTests.CreateDataBaseSQLiteInMemory(_options);

            // For EF Core InMemory database option
            //_options = ApplicationDbContextHelperTests.ApplicationDbContextOptionsEfCoreInMemory();
            //ApplicationDbContextHelperTests.CreateDataBaseEfCoreInMemory(_options);
        }

        [Test]
        public async Task GetAll_ShouldReturnAListOfColors_WhenColorsExist()
        {
            //---------------Set up test pack-------------------
            await using var context = new ApplicationDbContext(_options);
            var colorRepository = new ColorRepository(context);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var colors = await colorRepository.GetAll();
            //---------------Test Result -----------------------
            Assert.NotNull(colors);
            Assert.IsInstanceOf<List<Color>>(colors);
        }
    }
}
