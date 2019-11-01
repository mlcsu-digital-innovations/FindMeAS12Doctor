namespace Fmas12d.Data.Entities
{
    public class ExaminationDetail : BaseEntity, IExaminationDetail
    {
        public int ExaminationId { get; set; }
        public virtual Examination Examination { get; set; }
        public int ExaminationDetailTypeId { get; set; }
        public virtual ExaminationDetailType ExaminationDetailType { get; set; }
    }
}