//using AutoMapper;
//using ExpressVoitures.Data.Dto;
//using ExpressVoitures.Data.Entities;
//using Microsoft.EntityFrameworkCore;

//namespace ExpressVoitures
//{
//    public interface ICarRepository
//    {
//        Task<List<Car>> GetAllCarsAsync();
//        //Task<Car?> GetCarAsync(string codeVin);
//        //Task<Car> CreateCarAsync(Car car);
//        //Task<Car> UpdateCarAsync(Car car);
//        //Task<bool> DeleteCarAsync(string codeVin);
//    }

//    public class CarRepository : ICarRepository
//    {
//        private readonly ExpressVoituresContext _context;
//        private readonly IMapper _mapper;

//        public CarRepository(ExpressVoituresContext context, IMapper mapper) 
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public async Task<List<Car>> GetAllCarsAsync()
//        {
//            var listAllCars = await _context.Cars.ToListAsync();
//            return _mapper.Map<List<CarDto>>(listAllCars);
//        }

//    }
//}
