using WashMyCar.Core.Domain;

namespace WashMyCar.Core.DataContracts
{
    public interface IColorRepository
    {        
        List<Color> GetAll();
        bool Save(Color color);
    }
}
