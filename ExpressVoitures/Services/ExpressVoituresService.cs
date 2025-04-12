using AutoMapper;
using ExpressVoitures.Data.Dto;
using ExpressVoitures.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Services
{
    public interface IExpressVoituresService
    {
        Task<List<HomeCarModel>> GetAllHomeCars();
        //Task<VoitureModel?> GetCarAsync(string codeVin);
        //Task<VoitureModel> CreateVoitureAsync(VoitureModel model);
        //Task<VoitureModel> UpdateCarAsync(VoitureModel model);
        //Task<VoitureModel> DeleteCarAsync(string codeVin);
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

        private async Task<List<VoitureDto>> GetAllCars()
        {
            List<VoitureDto> listAllCars = await _context.Voitures.Include(v => v.Date)
                                                                  .Include(v => v.Prix)
                                                                  .Include(v => v.Reparation)
                                                                  .Include(v => v.Reparation.Types)
                                                                  .ToListAsync();
            return listAllCars;
        }

        public async Task<List<HomeCarModel>> GetAllHomeCars()
        {
            var listAllCars = await GetAllCars();
            var listHomeCars = listAllCars.Select(v => new HomeCarModel
            {
                Id = v.Id,
                Marque = v.Marque,
                Modele = v.Modele,
                Annee = v.Annee,
                PrixVente = v.Prix.PrixVente
            }).ToList();

            return listHomeCars;
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
            var voiture = _mapper.Map(voitureContext, new VoitureModel());
            return voiture;
        }

        //        public async Task<VoitureModel> CreateVoitureAsync(VoitureModel model)
        //        {
        //            var voiture = new VoitureDto
        //            {
        //                CodeVin = model.CodeVin,
        //                Marque = model.Marque,
        //                Modele = model.Modele,
        //                Finition = model.Finition,
        //                Annee = model.Annee,
        //                ImagePath = model.ImagePath,
        //                Prix = new PrixDto
        //                {
        //                    PrixAchat = model.Prix.PrixAchat,
        //                    PrixReparation = model.Prix.PrixReparation,
        //                    PrixVente = model.Prix.PrixVente
        //                },
        //                Date = new DateDto
        //                {
        //                    DateAchat = model.Date.DateAchat,
        //                    DateMiseEnVente = model.Date.DateMiseEnVente,
        //                    DateVente = model.Date.DateVente
        //                },
        //                Reparations = model.Reparations.Select(r => new ReparationDto
        //                {
        //                    Description = r.Description,
        //                    Prix = r.Prix,
        //                    Duree = r.Duree
        //                }).ToList()
        //            };
        //            await _context.Voitures.AddAsync(voiture);
        //            await _context.SaveChangesAsync();

        //            return _mapper.Map<VoitureModel>(voiture);
        //        }

        //        public async Task<VoitureModel> UpdateCarAsync(VoitureModel model)
        //        {
        //            var voiture = await _context.Voitures.Where(v => v.CodeVin == model.CodeVin)
        //                                                 .Include(v => v.Date)
        //                                                 .Include(v => v.Prix)
        //                                                 .Include(v => v.Reparations)
        //                                                 .FirstOrDefaultAsync();

        //            if (voiture == null)
        //            {
        //                throw new Exception($"Voiture with CodeVin {model.CodeVin} not found.");
        //            }

        //            voiture.CodeVin = model.CodeVin;
        //            voiture.Marque = model.Marque;
        //            voiture.Modele = model.Modele;
        //            voiture.Finition = model.Finition;
        //            voiture.Annee = model.Annee;
        //            voiture.ImagePath = model.ImagePath;
        //            voiture.Prix!.PrixAchat = model.Prix!.PrixAchat;
        //            voiture.Date!.DateAchat = model.Date!.DateAchat;
        //            voiture.Date.DateMiseEnVente = model.Date.DateMiseEnVente;
        //            voiture.Date.DateVente = model.Date.DateVente;
        //            if (model.Reparations != null)
        //            {
        //                if (voiture.Reparations != null)
        //                    voiture.Reparations.Clear();
        //                foreach (var reparation in model.Reparations)
        //                {
        //                    voiture.Reparations!.Add(new ReparationDto
        //                    {
        //                        Description = reparation.Description,
        //                        Prix = reparation.Prix,
        //                        Duree = reparation.Duree
        //                    });
        //                }
        //            }
        //            await UpdatePrix(voiture);
        //            await _context.SaveChangesAsync();
        //            return _mapper.Map<VoitureModel>(voiture);
        //        }

        //        public async Task<VoitureModel> DeleteCarAsync(string codeVin)
        //        {
        //            var voiture = await _context.Voitures.FindAsync(codeVin) ?? throw new Exception($"Voiture with CodeVin {codeVin} not found.");
        //            _context.Voitures.Remove(voiture);
        //            await _context.SaveChangesAsync();
        //            return _mapper.Map<VoitureModel>(voiture);
        //        }

        //        public async Task UpdatePrix(VoitureDto voiture)
        //        {
        //            voiture.Prix!.PrixReparation = voiture.Reparations.Sum(r => r.Prix);
        //            voiture.Prix!.PrixVente = voiture.Prix.PrixAchat + voiture.Prix.PrixReparation + 500;
        //            await _context.SaveChangesAsync();

        //        }
        //    }
    }
}

