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
            var response = await _colorRepository.GetAll();
            return _mapper.Map<List<ColorResponse>>(response) ?? new List<ColorResponse>();
        }

        public async Task<bool> SaveAsync(ColorRequest colorRequest)
        {
            var colorObj = _mapper.Map<Color>(colorRequest);            
            var isSaved = await _colorRepository.Add(colorObj);
            return isSaved;
        }

        public async Task<bool> UpdateAsync(ColorRequest colorRequest)
        {         
            var colorObj = _mapper.Map<Color>(colorRequest);
            var isUpdated = await _colorRepository.Update(colorObj);
            return isUpdated;
        }

        public async Task<ColorResponse> GetColorByIdAsync(int colorId)
        {
            var color = await _colorRepository.GetById(colorId);
            var response = _mapper.Map<ColorResponse>(color);
            return response;
        }

        public async Task<bool> DeleteAsync(int colorId)
        {
            var color = await _colorRepository.GetById(colorId);

            if (color is null)
                return false;

            var isDeleted = await _colorRepository.Remove(color);
            
            return isDeleted;
        }
    }
}
