namespace Fmas12d.Business.Models
{
  public class AssessmentDetail : BaseModel
  {
    public AssessmentDetail() { }
    public AssessmentDetail(Data.Entities.AssessmentDetail entity) : base(entity)
    {
      if (entity == null) return;

      AssessmentId = entity.AssessmentId;
      // TODO Assessment
      AssessmentDetailTypeId = entity.AssessmentDetailTypeId;
      AssessmentDetailType = new AssessmentDetailType(entity.AssessmentDetailType);
    }

    public int AssessmentId { get; set; }
    public virtual Assessment Assessment { get; set; }
    public int AssessmentDetailTypeId { get; set; }
    public virtual AssessmentDetailType AssessmentDetailType { get; set; }
  }
}