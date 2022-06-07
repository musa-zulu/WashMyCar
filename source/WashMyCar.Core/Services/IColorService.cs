using WashMyCar.Core.Request;
using WashMyCar.Core.Response;

namespace WashMyCar.Core.Services
{
    public interface IColorService
    {
        Task<IEnumerable<ColorResponse>> GetAllColorsAsync();
        Task<bool> SaveAsync(ColorRequest colorRequest);
        Task<bool> UpdateAsync(ColorRequest colorRequest);
        Task<bool> DeleteAsync(int colorId);
        Task<ColorResponse> GetColorByIdAsync(int colorId);
    }
}
