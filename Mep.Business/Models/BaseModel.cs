using System;
using System.ComponentModel.DataAnnotations;

namespace Mep.Business.Models
{
    public abstract class BaseModel : IBaseModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTimeOffset ModifiedAt { get; set; }
        [Required]
        public int ModifiedBy { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}