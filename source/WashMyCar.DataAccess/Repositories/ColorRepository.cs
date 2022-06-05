using WashMyCar.Core.DataContracts;
using WashMyCar.Core.Domain;

namespace WashMyCar.DataAccess.Repositories
{
    public class ColorRepository : IColorRepository
    {
        private readonly ApplicationDbContext _context;

        public ColorRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<bool> DeleteAsync(Color color)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid colorId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Color>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Color> GetColorByIdAsync(Guid colorId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAsync(Color color)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Color color)
        {
            throw new NotImplementedException();
        }
    }
}
