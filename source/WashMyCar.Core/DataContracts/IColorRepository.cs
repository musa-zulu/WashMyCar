using WashMyCar.Core.Domain;

namespace WashMyCar.Core.DataContracts
{
    public interface IColorRepository
    {        
        Task<List<Color>> GetAllAsync();
        Task<Color> GetColorByIdAsync(Guid colorId);
        Task<bool> SaveAsync(Color color);
        Task<bool> UpdateAsync(Color color);
        Task<bool> DeleteAsync(Color color);      
    }
}
