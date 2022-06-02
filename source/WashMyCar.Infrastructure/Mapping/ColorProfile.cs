using AutoMapper;
using WashMyCar.Core.Domain;
using WashMyCar.Core.Request;
using WashMyCar.Core.Response;

namespace WashMyCar.Infrastructure.Mapping
{
    public class ColorProfile : Profile
    {
        public ColorProfile()
        {
            CreateMap<ColorRequest, Color>().ReverseMap();                
            CreateMap<ColorResponse, Color>().ReverseMap();                
            CreateMap<List<ColorResponse>, List<Color>>().ReverseMap();                
        }
    }
}
