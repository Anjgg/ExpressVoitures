using AutoMapper;
using ExpressVoitures.Data.Dto;
using ExpressVoitures.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Data.Services
{
    public interface IExpressVoituresService
    {
        Task<List<VoitureModel>> GetAllCars();
        Task<VoitureModel?> GetVoitureAsync(string codeVin);
        Task<VoitureModel> CreateVoitureAsync(VoitureModel voiture);
        Task<VoitureModel> UpdateVoitureAsync(VoitureModel model);
        Task<VoitureModel> DeleteVoitureAsync(string codeVin);
        Task<(bool, string)> CheckIfModified(VoitureModel model);
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

        public async Task<VoitureModel?> GetVoitureAsync(string codeVin)
        {
            var voitureContext = await _context.Voitures.Where(a => a.CodeVin == codeVin).FirstOrDefaultAsync();
            if (voitureContext == null)
            {
                return null;
            }
            return _mapper.Map<VoitureModel>(voitureContext);
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

        public async Task<VoitureModel> UpdateVoitureAsync(VoitureModel model)
        {
            await _context.Voitures
                        .Where(a => a.CodeVin == model.CodeVin)
                        .ExecuteUpdateAsync(setPropertyCalls => setPropertyCalls
                            .SetProperty(v => v.Marque, model.Marque)
                            .SetProperty(v => v.Modele, model.Modele)
                            .SetProperty(v => v.Finition, model.Finition)
                            .SetProperty(v => v.Annee, model.Annee)
                        );
            var voiture = await _context.Voitures.Where(a => a.CodeVin == model.CodeVin).FirstOrDefaultAsync();
            return _mapper.Map<VoitureModel>(voiture);
        }

        public async Task<(bool, string)> CheckIfModified(VoitureModel voitureModel)
        {
            var voitureDto = await _context.Voitures.Where(a => a.CodeVin == voitureModel.CodeVin).FirstOrDefaultAsync();
            if (voitureDto == null)
            {
                string errorMessage = $"VoitureDto not found with the CodeVin : {voitureModel.CodeVin}.";
                return (false, errorMessage);
            }
            var voitureModelV2 = _mapper.Map<VoitureModel>(voitureDto);

            bool isModified = !voitureModelV2.Equals(voitureModel);
            return (isModified, string.Empty);
        }

        public async Task<VoitureModel> DeleteVoitureAsync(string codeVin)
        {
            var voiture = await _context.Voitures.Where(a => a.CodeVin == codeVin).FirstOrDefaultAsync();
            await _context.Voitures.Where(a => a.CodeVin == codeVin).ExecuteDeleteAsync();
            return _mapper.Map<VoitureModel>(voiture);
        }

    }
}
