using ExpressVoitures.Database.Dto;

namespace ExpressVoitures.Database.TableDeLiaison
{
    public class OperationReparationDto
    {
        public int OperationId { get; set; }

        public OperationDto OperationDto { get; set; }

        public int ReparationId { get; set; }

        public ReparationDto ReparationDto { get; set; }
    }
}
