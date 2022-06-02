using WashMyCar.Core.DataContracts;
using WashMyCar.Core.Domain;
using WashMyCar.Core.Request;
using WashMyCar.Core.Response;
using AutoMapper;

namespace WashMyCar.Core.Services
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public ColorService(IColorRepository colorRepository, IMapper mapper)
        {
            _colorRepository = colorRepository ?? throw new ArgumentNullException(nameof(colorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IEnumerable<ColorResponse> GetAllColors()
        {
            var response = _colorRepository.GetAll();
            return _mapper.Map<List<ColorResponse>>(response) ?? new List<ColorResponse>();
        }

        public bool Save(ColorRequest colorRequest)
        {
            if (colorRequest is null)
                throw new ArgumentNullException(nameof(colorRequest));

            var colorObj = _mapper.Map<Color>(colorRequest);
            colorObj.ColorId = Guid.NewGuid();
            bool isSaved = _colorRepository.Save(colorObj);
            return isSaved;
        }

        public bool Update(ColorRequest colorRequest)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid colorId)
        {
            throw new NotImplementedException();
        }
    }
}
