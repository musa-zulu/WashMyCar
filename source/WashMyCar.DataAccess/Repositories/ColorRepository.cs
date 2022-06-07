using WashMyCar.Core.DataContracts;
using WashMyCar.Core.Domain;

namespace WashMyCar.DataAccess.Repositories
{
    public class ColorRepository : Repository<Color>, IColorRepository
    {        
        public ColorRepository(ApplicationDbContext context) : base(context)
        {           
        }
    }
}
