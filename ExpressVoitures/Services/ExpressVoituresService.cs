using ExpressVoitures.Context;
using ExpressVoitures.Data.Dto;
using ExpressVoitures.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Services()
{
    public interface IExpressVoituresService
    {
        Task<List<HomeCarModel>> GetAllHomeCars();
        Task<VoitureProfileModel?> GetCarAsync(int id);
        Task<VoitureProfileModel> CreateVoitureAsync(VoitureProfileModel model);
        Task<VoitureProfileModel> UpdateCarAsync(VoitureProfileModel model);
        Task DeleteCarAsync(int id);
        Task<VoitureProfileModel> GetNewVoitureProfileModel();
    }

    public class ExpressVoituresService : IExpressVoituresService
    {
        private readonly ExpressVoituresContext _context;

        public ExpressVoituresService(ExpressVoituresContext context)
        {
            _context = context;
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

            return await MapDtoToProfileModel(voitureDto);
        }

        public async Task<VoitureProfileModel> CreateVoitureAsync(VoitureProfileModel model)
        {
            var typesSelectedId = model.Types.Where(t => t.IsSelected == true).Select(a => a.Id).ToList();
            var typesSelected = await _context.Types.Where(t => typesSelectedId.Contains(t.Id)).ToListAsync();
            var sumPrixReparation = typesSelected.Sum(t => t.Prix);
            var sumDureeReparation = typesSelected.Sum(t => t.Duree);

            var voitureDto = new VoitureDto
            {
                CodeVin = model.Voiture.CodeVin,
                Marque = model.Voiture.Marque,
                Modele = model.Voiture.Modele,
                Finition = model.Voiture.Finition,
                AnneeFabrication = model.Voiture.AnneeFabrication,
                ImagePath = model.Voiture.ImageVoiture is null ? "/images/default.jpg" : await UploadImageAsync(model),
                Reparation = new ReparationDto
                {
                    Types = typesSelected,
                    PrixTotal = typesSelected.Sum(t => t.Prix),
                    DureeTotal = typesSelected.Sum(t => t.Duree)
                },
                Prix = new PrixDto
                {
                    PrixAchat = model.Prix.PrixAchat,
                    PrixReparation = sumPrixReparation,
                    PrixVente = model.Prix.PrixAchat + sumPrixReparation + 500
                },
                Date = new DateDto
                {
                    DateAchat = model.Date.DateAchat,
                    DateMiseEnVente = model.Date.DateAchat.AddDays(sumDureeReparation),
                }

            };
            await _context.Voitures.AddAsync(voitureDto);
            await _context.SaveChangesAsync();

            return await MapDtoToProfileModel(voitureDto);
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

            await UpdateDtoFromProfileModel(voitureDto, model);

            return await MapDtoToProfileModel(voitureDto);
        }

        public async Task DeleteCarAsync(int id)
        {
            var voiture = await _context.Voitures.FindAsync(id) ?? throw new Exception($"Voiture with id '{id}' not found.");
            _context.Voitures.Remove(voiture);
            if (voiture.ImagePath != null && voiture.ImagePath != "/images/imagedefault.jpg")
            {
                var imageVoiturePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot") + voiture.ImagePath;
                if (File.Exists(imageVoiturePath))
                {
                    File.Delete(imageVoiturePath);
                }
            }

            await _context.SaveChangesAsync();
        }

        private async Task UpdateDtoFromProfileModel(VoitureDto voitureDto, VoitureProfileModel model)
        {
            // Update Reparations properties first
            var selectedTypesId = model.Types.Where(t => t.IsSelected).Select(t => t.Id).ToList();
            var selectedTypes = _context.Types.Where(t => selectedTypesId.Contains(t.Id)).ToList();
            if (voitureDto.Reparation.Types is List<TypeDto> typeList)
            {
                typeList.Clear();
                foreach (var type in selectedTypes)
                {
                    typeList.Add(type);
                }
                voitureDto.Reparation.Types = typeList;
            }
            voitureDto.Reparation.PrixTotal = selectedTypes.Sum(t => t.Prix);
            voitureDto.Reparation.DureeTotal = selectedTypes.Sum(t => t.Duree);

            // Update Voiture properties
            voitureDto.CodeVin = model.Voiture.CodeVin;
            voitureDto.Marque = model.Voiture.Marque;
            voitureDto.Modele = model.Voiture.Modele;
            voitureDto.Finition = model.Voiture.Finition;
            voitureDto.AnneeFabrication = model.Voiture.AnneeFabrication;
            if (model.Voiture.ImageVoiture != null && model.Voiture.ImageVoiture.Length > 0)
            {
                voitureDto.ImagePath = await UploadImageAsync(model);
            }

            // Update Date properties
            voitureDto.Date.DateAchat = model.Date.DateAchat;
            voitureDto.Date.DateMiseEnVente = model.Date.DateAchat.AddDays(model.Reparation.DureeTotal);
            voitureDto.Date.DateVente = model.Date.DateVente;

            // Update Prix properties
            voitureDto.Prix.PrixAchat = model.Prix.PrixAchat;
            voitureDto.Prix.PrixReparation = voitureDto.Reparation.PrixTotal;
            voitureDto.Prix.PrixVente = voitureDto.Prix.PrixAchat + voitureDto.Prix.PrixReparation + 500;

            await _context.SaveChangesAsync();
        }

        private async Task<VoitureProfileModel> MapDtoToProfileModel(VoitureDto voitureDto)
        {
            var listAllTypes = await _context.Types.ToListAsync();
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
                Types = listAllTypes.Select(r => new TypeModel
                {
                    Id = r.Id,
                    Description = r.Description,
                    Prix = r.Prix,
                    Duree = r.Duree,
                    IsSelected = voitureDto.Reparation.Types.Any(t => t.Id == r.Id) // Check if the type is selected in the dto
                }).ToList()
            };
        }

        public async Task<VoitureProfileModel> GetNewVoitureProfileModel()
        {
            var listTypesDto = await _context.Types.ToListAsync();
            var listTypesModel = listTypesDto.Select(t => new TypeModel
            {
                Id = t.Id,
                Description = t.Description,
                Prix = t.Prix,
                Duree = t.Duree,
                IsSelected = false
            }).ToList();

            var newVoitureProfileModel = new VoitureProfileModel
            {
                Voiture = new VoitureModel(),
                Date = new DateModel(),
                Prix = new PrixModel(),
                Reparation = new ReparationModel(),
                Types = listTypesModel
            };
            return newVoitureProfileModel;
        }

        private static async Task<string> UploadImageAsync(VoitureProfileModel model)
        {
            string index = Guid.NewGuid().ToString().ElementAt(0).ToString();
            var nameDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", model.Voiture.CodeVin);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", nameDirectory);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            else
            {
                var files = Directory.GetFiles(filePath);
                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }

            string nameFile = $"/{model.Voiture.CodeVin}_{index}.jpg";
            using (var stream = new FileStream($"{filePath}{nameFile}", FileMode.OpenOrCreate))
            {
                
                await model.Voiture.ImageVoiture!.CopyToAsync(stream);
            }
            
            return $"/images/{model.Voiture.CodeVin}/{nameFile}";
            
        }
    }
}

