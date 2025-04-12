using AutoMapper;
using ExpressVoitures.Data.Dto;
using ExpressVoitures.Data.Entities;
using ExpressVoitures.Data.Models;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpressVoitures.Services
{
    public interface IExpressVoituresService
    {
        Task<List<HomeCarModel>> GetAllHomeCarsAsync();
        Task<VoitureModel?> GetCarAsync(int id);
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

        public async Task<List<HomeCarModel>> GetAllHomeCarsAsync()
        {
            var listAllCars = _mapper.Map<List<CarDto>>(await _context.Cars.ToListAsync());
            var listAllPrices = _mapper.Map<List<PriceDto>>(await _context.Prices.ToListAsync());
            
            var listAllHomeCars = listAllCars.Select(car => new HomeCarModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                ManufactureDate = car.ManufactureDate,
                SellingPrice = listAllPrices.FirstOrDefault(p => p.CarId == car.Id).Selling,
            }).ToList();
            
            return listAllHomeCars;
        }

        public async Task<VoitureModel?> GetCarAsync(int id)
        {
            var voitureContext = await _context.Cars.Where(a => a.VinCode == codeVin)
                                                        .Include(v => v.EventHistory)
                                                        .Include(v => v.Price)
                                                        .Include(v => v.Repairs)
                                                        .FirstOrDefaultAsync();

            if (voitureContext == null)
            {
                return null;
            }
            var voiture = _mapper.Map(voitureContext, new VoitureModel());
            return voiture;
        }

        //public async Task<VoitureModel> CreateVoitureAsync(VoitureModel model)
        //{
        //    var voiture = new CarDto
        //    {
        //        VinCode = model.CodeVin,
        //        Brand = model.Marque,
        //        Model = model.Modele,
        //        Version = model.Finition,
        //        ManufactureDate = model.Annee,
        //        ImagePath = model.ImagePath,
        //        Price = new PriceDto
        //        {
        //            Purchase = model.Prix.PrixAchat,
        //        },
        //        EventHistory = new EventHistoryDto
        //        {
        //            Purchase = model.Date.DateAchat,
        //            Selling = model.Date.DateVente
        //        },
        //        Repairs = model.Reparations.Select(r => new RepairDto
        //        {
        //            Description = r.Description,
        //            Price = r.Prix,
        //            DaysOfWork = r.Duree
        //        }).ToList()
        //    };
        //    await UpdatePrix(voiture);
        //    await _context.Cars.AddAsync(voiture);
        //    await _context.SaveChangesAsync();

        //    return _mapper.Map<VoitureModel>(voiture);
        //}

        //public async Task<VoitureModel> UpdateCarAsync(VoitureModel model)
        //{
        //    var voiture = await _context.Cars.Where(v => v.VinCode == model.CodeVin)
        //                                         .Include(v => v.EventHistory)
        //                                         .Include(v => v.Price)
        //                                         .Include(v => v.Repair)
        //                                         .FirstOrDefaultAsync();

        //    if (voiture == null)
        //    {
        //        throw new Exception($"Car with VinCode {model.CodeVin} not found.");
        //    }

        //    voiture.VinCode = model.CodeVin;
        //    voiture.Brand = model.Marque;
        //    voiture.Modele = model.Modele;
        //    voiture.Version = model.Finition;
        //    voiture.Year = model.Annee;
        //    voiture.ImagePath = model.ImagePath;
        //    voiture.Price!.Purchase = model.Prix!.PrixAchat;
        //    voiture.EventHistory!.DateAchat = model.Date!.DateAchat;
        //    voiture.EventHistory.DateMiseEnVente = model.Date.DateMiseEnVente;
        //    voiture.EventHistory.DateVente = model.Date.DateVente;
        //    if (model.Reparations != null)
        //    {
        //        if (voiture.Repair != null)
        //            voiture.Repair.Clear();
        //        foreach (var reparation in model.Reparations)
        //        {
        //            voiture.Repair!.Add(new RepairDto
        //            {
        //                Description = reparation.Description,
        //                Price = reparation.Prix,
        //                DaysOfWork = reparation.Duree
        //            });
        //        }
        //    }
        //    await UpdatePrix(voiture);
        //    await _context.SaveChangesAsync();
        //    return _mapper.Map<VoitureModel>(voiture);
        //}

        //public async Task<VoitureModel> DeleteCarAsync(string codeVin)
        //{
        //    var voiture = await _context.Cars.FindAsync(codeVin) ?? throw new Exception($"Car with VinCode {codeVin} not found.");
        //    _context.Cars.Remove(voiture);
        //    await _context.SaveChangesAsync();
        //    return _mapper.Map<VoitureModel>(voiture);
        //}

        //public async Task UpdatePrix(CarDto voiture)
        //{
        //    voiture.Price!.PrixReparation = voiture.Repairs.Sum(r => r.Prix);
        //    voiture.Price!.PrixVente = voiture.Price.PrixAchat + voiture.Price.PrixReparation + 500;
        //    await _context.SaveChangesAsync();

        //}

        //public async Task UpdateDate(CarDto voiture)
        //{
        //    var daysOfReparation = voiture.Repairs.Sum(r => r.Duree);
        //    voiture.EventHistory!.DateMiseEnVente = DateTime.Now.AddDays(daysOfReparation);
        //    await _context.SaveChangesAsync();
        //}
    }
}

