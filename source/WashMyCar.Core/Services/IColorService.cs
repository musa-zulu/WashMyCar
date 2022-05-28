using WashMyCar.Core.Request;
using WashMyCar.Core.Response;

namespace WashMyCar.Core.Services
{
    public interface IColorService
    {
        IEnumerable<ColorResponse> GetAllColors();
        void Save(ColorRequest colorRequest);
    }
}
