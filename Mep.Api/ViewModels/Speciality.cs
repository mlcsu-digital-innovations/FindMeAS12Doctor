using System.ComponentModel.DataAnnotations;
using Mep.Api.SharedModels;

namespace Mep.Api.ViewModels
{
  public class Speciality : NameDescription
    {           
        [Required]
        public int Id { get; set; }
    }
}