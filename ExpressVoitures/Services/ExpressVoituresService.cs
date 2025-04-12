using AutoMapper;
using ExpressVoitures.Data.Dto;
using ExpressVoitures.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Services
{
    public interface IExpressVoituresService
    {
        Task<List<HomeCarModel>> GetAllHomeCars();
        Task<VoitureProfileModel?> GetCarAsync(int id);
        //Task<VoitureModel> CreateVoitureAsync(VoitureModel model);
        Task<VoitureProfileModel> UpdateCarAsync(VoitureProfileModel model);
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

        public async Task<List<HomeCarModel>> GetAllHomeCars()
        {
            List<VoitureDto> listAllCars = await _context.Voitures.Include(v => v.Date)
                                                                  .Include(v => v.Prix)
                                                                  .ToListAsync();

            List<HomeCarModel> listHomeCars = listAllCars.Select(v => new HomeCarModel
            {
                Id = v.Id,
                Marque = v.Marque,
                Modele = v.Modele,
                DateFabrication = v.DateFabrication,
                ImagePath = v.ImagePath,
                PrixVente = v.Prix.PrixVente,
                IsAvailable = v.Date.DateVente == null 
            }).ToList();

            return listHomeCars;
        }

        public async Task<VoitureProfileModel?> GetCarAsync(int id)
        {
            var voitureDto = await _context.Voitures.Where(a => a.Id == id)
                                                        .Include(v => v.Date)
                                                        .Include(v => v.Prix)
                                                        .Include(v => v.Reparation)
                                                        .ThenInclude(r => r.Types)
                                                        .FirstOrDefaultAsync();

            if (voitureDto == null)
            {
                return null;
            }
            
            return MapVoitureDtoToVoitureProfileModel(voitureDto);
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

        public async Task<VoitureProfileModel> UpdateCarAsync(VoitureProfileModel model)
        {
            var voitureDto = await _context.Voitures.Where(v => v.Id == model.Voiture.Id)
                                                 .Include(v => v.Date)
                                                 .Include(v => v.Prix)
                                                 .Include(v => v.Reparation)
                                                 .ThenInclude(r => r.Types)
                                                 .FirstOrDefaultAsync();

            if (voitureDto == null)
            {
                throw new Exception($"Voiture with Id {model.Voiture.Id} not found.");
            }

            UpdateDtoFromProfileModel(voitureDto, model);

            await _context.SaveChangesAsync();
            
            return MapVoitureDtoToVoitureProfileModel(voitureDto);
        }

        //        public async Task<VoitureModel> DeleteCarAsync(string codeVin)
        //        {
        //            var voiture = await _context.Voitures.FindAsync(codeVin) ?? throw new Exception($"Voiture with CodeVin {codeVin} not found.");
        //            _context.Voitures.Remove(voiture);
        //            await _context.SaveChangesAsync();
        //            return _mapper.Map<VoitureModel>(voiture);
        //        }

        private static void UpdateDtoFromProfileModel(VoitureDto voitureDto, VoitureProfileModel model)
        {
            voitureDto.CodeVin = model.Voiture.CodeVin;
            voitureDto.Marque = model.Voiture.Marque;
            voitureDto.Modele = model.Voiture.Modele;
            voitureDto.Finition = model.Voiture.Finition;
            voitureDto.DateFabrication = model.Voiture.DateFabrication;
            voitureDto.ImagePath = model.Voiture.ImagePath;
            voitureDto.Date.DateAchat = model.Date.DateAchat;
            voitureDto.Date.DateMiseEnVente = model.Date.DateMiseEnVente;
            voitureDto.Date.DateVente = model.Date.DateVente;
            if (model.Reparation.Types != null)
            {
                voitureDto.Reparation.Types = model.Reparation.Types.Select(r => new TypeDto
                {
                    Id = r.Id,
                    Description = r.Description,
                    Prix = r.Prix,
                    Duree = r.Duree
                }).ToList();
            }
            else
            {
                voitureDto.Reparation.Types.Clear();
            }
            // Update Prix after Reparation
            voitureDto.Prix.PrixAchat = model.Prix.PrixAchat;
            voitureDto.Reparation.PrixTotal = voitureDto.Reparation.Types.Sum(r => r.Prix);
            voitureDto.Prix.PrixReparation = voitureDto.Reparation.PrixTotal;
            voitureDto.Prix.PrixVente = voitureDto.Prix.PrixAchat + voitureDto.Prix.PrixReparation + 500;
        }


        private static VoitureProfileModel MapVoitureDtoToVoitureProfileModel(VoitureDto voitureDto)
        {
            return new VoitureProfileModel
            {
                Voiture = new VoitureModel
                {
                    Id = voitureDto.Id,
                    CodeVin = voitureDto.CodeVin,
                    Marque = voitureDto.Marque,
                    Modele = voitureDto.Modele,
                    Finition = voitureDto.Finition,
                    DateFabrication = voitureDto.DateFabrication,
                    ImagePath = voitureDto.ImagePath
                },
                Date = new DateModel
                {
                    Id = voitureDto.Date.Id,
                    DateAchat = voitureDto.Date.DateAchat,
                    DateMiseEnVente = voitureDto.Date.DateMiseEnVente,
                    DateVente = voitureDto.Date.DateVente
                },
                Prix = new PrixModel
                {
                    Id = voitureDto.Prix.Id,
                    PrixAchat = voitureDto.Prix.PrixAchat,
                    PrixReparation = voitureDto.Prix.PrixReparation,
                    PrixVente = voitureDto.Prix.PrixVente
                },
                Reparation = new ReparationModel
                {
                    Id = voitureDto.Reparation.Id,
                    PrixTotal = voitureDto.Reparation.PrixTotal,
                    DureeTotal = voitureDto.Reparation.DureeTotal,
                    Types = voitureDto.Reparation.Types.Select(r => new TypeModel
                    {
                        Id = r.Id,
                        Description = r.Description,
                        Prix = r.Prix,
                        Duree = r.Duree
                    }).ToList()
                }
            };
        }

    }
}

