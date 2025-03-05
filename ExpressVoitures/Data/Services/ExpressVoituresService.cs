using AutoMapper;
using ExpressVoitures.Data.Dto;
using ExpressVoitures.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Data.Services
{
    public interface IExpressVoituresService
    {
        Task<List<VoitureModel>> GetAllCars();
        Task<VoitureModel> GetVoitureAsync(int id);
        Task<VoitureModel> CreateVoitureAsync(VoitureModel voiture);
        Task<VoitureModel> UpdateVoitureAsync(VoitureModel voiture);
        Task DeleteVoitureAsync(int id);
    }

    public class ExpressVoituresService : IExpressVoituresService
    {
        private readonly ExpressVoituresContext _context;
        private readonly IMapper _mapper;

        public ExpressVoituresService(ExpressVoituresContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<VoitureModel>> GetAllCars()
        {
            List<VoitureDto> listAllCars = await _context.Voitures.Where(a => a.CodeVin != null).ToListAsync();
            return _mapper.Map<List<VoitureModel>>(listAllCars);
        }

        public Task<VoitureModel> GetVoitureAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<VoitureModel> CreateVoitureAsync(VoitureModel voiture)
        {
            throw new NotImplementedException();
        }

        public Task<VoitureModel> UpdateVoitureAsync(VoitureModel voiture)
        {
            throw new NotImplementedException();
        }

        public Task DeleteVoitureAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
