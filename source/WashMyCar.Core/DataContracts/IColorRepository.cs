using WashMyCar.Core.Response;

namespace WashMyCar.Core.DataContracts
{
    public interface IColorRepository
    {        
        public IEnumerable<ColorResponse> GetAll();
    }
}
