﻿using WashMyCar.Core.DataContracts;
using WashMyCar.Core.Request;
using WashMyCar.Core.Response;

namespace WashMyCar.Core.Services
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;

        public ColorService(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository ?? throw new ArgumentNullException(nameof(colorRepository));
        }

        public IEnumerable<ColorResponse> GetAllColors()
        {
            return _colorRepository.GetAll();
        }

        public void Save(ColorRequest colorRequest)
        {
            if (colorRequest is null)
                throw new ArgumentNullException(nameof(colorRequest));
        }
    }
}
