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

        public async Task<IEnumerable<ColorResponse>> GetAllColorsAsync()
        {
            var response = await _colorRepository.GetAllAsync();
            return _mapper.Map<List<ColorResponse>>(response) ?? new List<ColorResponse>();
        }

        public async Task<bool> SaveAsync(ColorRequest colorRequest)
        {
            var colorObj = _mapper.Map<Color>(colorRequest);
            colorObj.ColorId = Guid.NewGuid();
            bool isSaved = await _colorRepository.SaveAsync(colorObj);
            return isSaved;
        }

        public async Task<bool> UpdateAsync(ColorRequest colorRequest)
        {         
            var colorObj = _mapper.Map<Color>(colorRequest);
            var isUpdated = await _colorRepository.UpdateAsync(colorObj);
            return isUpdated;
        }

        public async Task<bool> Delete(Guid colorId)
        {
            throw new NotImplementedException();
        }
    }
}
