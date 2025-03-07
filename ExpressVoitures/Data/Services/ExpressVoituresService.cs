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
        Task DeleteVoitureAsync(VoitureModel model);
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

        public async Task<VoitureModel> CreateVoitureAsync(VoitureModel voiture)
        {
            var a = new VoitureDto {
                CodeVin = voiture.CodeVin,
                Marque = voiture.Marque,
                Modele = voiture.Modele,
                Finition = voiture.Finition,
                Annee = voiture.Annee,
            };


            await _context.Voitures.AddRangeAsync(a);
            _context.SaveChanges();
            return voiture;
        }

        public Task<VoitureModel> UpdateVoitureAsync(VoitureModel voiture)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteVoitureAsync(VoitureModel model)
        {
            await _context.Voitures.Where(a => a.CodeVin == model.CodeVin).ExecuteDeleteAsync();
            return;
        }

    }
}
