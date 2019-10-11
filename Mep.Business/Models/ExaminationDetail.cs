namespace Mep.Business.Models
{
    public class ExaminationDetail : BaseModel
    {
        public int ExaminationId { get; set; }
        public virtual Examination Examination { get; set; }
        public int ExaminationDetailTypeId { get; set; }
        public virtual ExaminationDetailType ExaminationDetailType { get; set; }
    }
}