using AutoMapper;
using ExpressVoitures.Data.Dto;
using ExpressVoitures.Data.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpressVoitures.Services
{
    public interface IExpressVoituresService
    {
        Task<List<VoitureModel>> GetAllCars();
        Task<VoitureModel?> GetCarAsync(string codeVin);
        Task<VoitureModel> CreateVoitureAsync(VoitureModel model);
        Task<VoitureModel> UpdateCarAsync(VoitureModel model);
        Task<VoitureModel> DeleteCarAsync(string codeVin);
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
            List<VoitureDto> listAllCars = await _context.Voitures.Include(v => v.Date)
                                                                  .Include(v => v.Prix)
                                                                  .Include(v => v.Reparation)
                                                                  .ToListAsync();
            return _mapper.Map<List<VoitureModel>>(listAllCars);
        }

        public async Task<VoitureModel?> GetCarAsync(string codeVin)
        {
            var voitureContext = await _context.Voitures.Where(a => a.CodeVin == codeVin)
                                                        .Include(v => v.Date)
                                                        .Include(v => v.Prix)
                                                        .Include(v => v.Reparation)
                                                        .FirstOrDefaultAsync();
            if (voitureContext == null)
            {
                return null;
            }
            return _mapper.Map<VoitureModel>(voitureContext);
        }

        public async Task<VoitureModel> CreateVoitureAsync(VoitureModel model)
        {
            var voiture = new VoitureDto
            {
                CodeVin = model.CodeVin,
                Marque = model.Marque,
                Modele = model.Modele,
                Finition = model.Finition,
                Annee = model.Annee,
                ImagePath = model.ImagePath,
                Prix = new PrixDto
                {
                    PrixAchat = model.Prix.PrixAchat,
                    PrixReparation = model.Prix.PrixReparation,
                    PrixVente = model.Prix.PrixVente
                },
                Date = new DateDto
                {
                    DateAchat = model.Date.DateAchat,
                    DateMiseEnVente = model.Date.DateMiseEnVente,
                    DateVente = model.Date.DateVente
                },
                Reparation = new ReparationDto
                {
                }
            };
            await _context.Voitures.AddAsync(voiture);
            await _context.SaveChangesAsync();

            return _mapper.Map<VoitureModel>(voiture);
        }

        public async Task<VoitureModel> UpdateCarAsync(VoitureModel model)
        {
            var voiture = await _context.Voitures.FindAsync(model.CodeVin) ?? throw new Exception($"Voiture with CodeVin {model.CodeVin} not found.");
            _context.Entry(voiture).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();
            return _mapper.Map<VoitureModel>(voiture);
        }

        public async Task<VoitureModel> DeleteCarAsync(string codeVin)
        {
            var voiture = await _context.Voitures.FindAsync(codeVin) ?? throw new Exception($"Voiture with CodeVin {codeVin} not found.");
            _context.Voitures.Remove(voiture);
            await _context.SaveChangesAsync();
            return _mapper.Map<VoitureModel>(voiture);
        }

    }
}
