using WashMyCar.Core.Domain;

namespace WashMyCar.Core.DataContracts
{
    public interface IColorRepository
    {        
        Task<List<Color>> GetAllAsync();
        Task<bool> SaveAsync(Color color);
        Task<bool> UpdateAsync(Color color);
    }
}
