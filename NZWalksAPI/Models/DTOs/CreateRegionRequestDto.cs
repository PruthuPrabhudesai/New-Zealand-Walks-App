using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTOs
{
    public class CreateRegionRequestDto
    {
        [Required] // comes from system.componentmodel.dataannotations
        [MinLength(3, ErrorMessage = "Code has to be a minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code has to be a maximum of 3 characters")]
        public string Code { get; set; }
        [Required] // comes from system.componentmodel.dataannotations
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
