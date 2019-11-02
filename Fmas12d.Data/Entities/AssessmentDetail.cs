namespace Fmas12d.Data.Entities
{
    public class AssessmentDetail : BaseEntity, IAssessmentDetail
    {
        public int AssessmentId { get; set; }
        public virtual Assessment Assessment { get; set; }
        public int AssessmentDetailTypeId { get; set; }
        public virtual AssessmentDetailType AssessmentDetailType { get; set; }
    }
}