using AutoMapper;
using ExpressVoitures.Data.Dto;
using ExpressVoitures.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Services
{
    public interface IExpressVoituresService
    {
        Task<List<HomeCarModel>> GetAllHomeCars();
        Task<VoitureProfileModel?> GetCarAsync(int id);
        Task<VoitureProfileModel> CreateVoitureAsync(VoitureProfileModel model);
        Task<VoitureProfileModel> UpdateCarAsync(VoitureProfileModel model);
        Task<VoitureProfileModel> DeleteCarAsync(int id);
        Task<List<TypeModel>> GetListTypeModel();
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
                AnneeFabrication = v.AnneeFabrication,
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

        public async Task<VoitureProfileModel> CreateVoitureAsync(VoitureProfileModel model)
        {
            var voiture = new VoitureDto
            {
                CodeVin = model.Voiture.CodeVin,
                Marque = model.Voiture.Marque,
                Modele = model.Voiture.Modele,
                Finition = model.Voiture.Finition,
                AnneeFabrication = model.Voiture.AnneeFabrication,
                ImagePath = model.Voiture.ImagePath,
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
                    PrixTotal = model.Reparation.PrixTotal,
                    DureeTotal = model.Reparation.DureeTotal,
                    Types = model.Types.Select(t => new TypeDto
                    {
                        Description = t.Description,
                        Prix = t.Prix,
                        Duree = t.Duree
                    }).ToList()
                }
            };
            await _context.Voitures.AddAsync(voiture);
            await _context.SaveChangesAsync();

            return MapVoitureDtoToVoitureProfileModel(voiture);
        }

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

        public async Task<VoitureProfileModel> DeleteCarAsync(int id)
        {
            var voiture = await _context.Voitures.FindAsync(id) ?? throw new Exception($"Voiture with id '{id}' not found.");
            _context.Voitures.Remove(voiture);
            await _context.SaveChangesAsync();
            return MapVoitureDtoToVoitureProfileModel(voiture);
        }

        private void UpdateDtoFromProfileModel(VoitureDto voitureDto, VoitureProfileModel model)
        {
            UpdateReparations(voitureDto, model);

            voitureDto.CodeVin = model.Voiture.CodeVin;
            voitureDto.Marque = model.Voiture.Marque;
            voitureDto.Modele = model.Voiture.Modele;
            voitureDto.Finition = model.Voiture.Finition;
            voitureDto.AnneeFabrication = model.Voiture.AnneeFabrication;
            voitureDto.ImagePath = model.Voiture.ImagePath;
            voitureDto.Date.DateAchat = model.Date.DateAchat;
            voitureDto.Date.DateMiseEnVente = model.Date.DateMiseEnVente;
            voitureDto.Date.DateVente = model.Date.DateVente;
            voitureDto.Prix.PrixAchat = model.Prix.PrixAchat;
            voitureDto.Reparation.PrixTotal = voitureDto.Reparation.Types.Sum(r => r.Prix);
            voitureDto.Prix.PrixReparation = voitureDto.Reparation.PrixTotal;
            voitureDto.Prix.PrixVente = voitureDto.Prix.PrixAchat + voitureDto.Prix.PrixReparation + 500;
        }

        private void UpdateReparations(VoitureDto voitureDto, VoitureProfileModel model)
        {
            // We check the model, if a list a Type exist and not empty
            if (model.Types != null && model.Types.Any())
            {
                var typeIdsToKeep = model.Types.Where(t => t.Id > 0).Select(t => t.Id).ToList();
                var typesToRemove = voitureDto.Reparation.Types.Where(t => !typeIdsToKeep.Contains(t.Id)).ToList();

                // We remove all the type deleted
                foreach (var typeToRemove in typesToRemove)
                {
                    _context.Remove(typeToRemove);
                }

                // Then we check all the type here
                foreach (var typeModel in model.Types)
                {
                    // If the type already exist, it has a Id != 0, so it will be updated
                    if (typeModel.Id > 0)
                    {
                        var existingType = voitureDto.Reparation.Types.FirstOrDefault(t => t.Id == typeModel.Id);
                        if (existingType != null)
                        {
                            existingType.Description = typeModel.Description;
                            existingType.Prix = typeModel.Prix;
                            existingType.Duree = typeModel.Duree;
                        }
                    }
                    // If the type Id = 0, we create a new one
                    else
                    {
                        var newType = new TypeDto
                        {
                            Description = typeModel.Description,
                            Prix = typeModel.Prix,
                            Duree = typeModel.Duree,
                        };
                        voitureDto.Reparation.Types.Add(newType);
                    }
                }
            }
            // If the model list of type is empty then remove in the dto
            else
            {
                foreach (var type in voitureDto.Reparation.Types.ToList())
                {
                    _context.Remove(type);
                }
            }
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
                    AnneeFabrication = voitureDto.AnneeFabrication,
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
                    
                },
                Types = voitureDto.Reparation.Types.Select(r => new TypeModel
                {
                    Id = r.Id,
                    Description = r.Description,
                    Prix = r.Prix,
                    Duree = r.Duree
                }).ToList()
            };
        }

        public async Task<List<TypeModel>> GetListTypeModel()
        {
            List<TypeDto> listTypeDto = await _context.Types.ToListAsync();
            List<TypeModel> listTypeModel = listTypeDto.Select(r => new TypeModel
            {
                Id = r.Id,
                Description = r.Description,
                Prix = r.Prix,
                Duree = r.Duree
            }).ToList();
            return listTypeModel;
            
        }

    }
}

