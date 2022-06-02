using WashMyCar.Core.Request;
using WashMyCar.Core.Response;

namespace WashMyCar.Core.Services
{
    public interface IColorService
    {
        IEnumerable<ColorResponse> GetAllColors();
        bool Save(ColorRequest colorRequest);
        bool Update(ColorRequest colorRequest);
        bool Delete(Guid colorId);
    }
}
