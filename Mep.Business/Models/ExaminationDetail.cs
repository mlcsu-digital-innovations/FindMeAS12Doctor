namespace Mep.Business.Models
{
  public class ExaminationDetail : BaseModel
  {
    public ExaminationDetail() { }
    public ExaminationDetail(Data.Entities.ExaminationDetail entity) : base(entity)
    {
      if (entity == null) return;

      ExaminationId = entity.ExaminationId;
      // TODO Examination
      ExaminationDetailTypeId = entity.ExaminationDetailTypeId;
      ExaminationDetailType = new ExaminationDetailType(entity.ExaminationDetailType);
    }

    public int ExaminationId { get; set; }
    public virtual Examination Examination { get; set; }
    public int ExaminationDetailTypeId { get; set; }
    public virtual ExaminationDetailType ExaminationDetailType { get; set; }
  }
}