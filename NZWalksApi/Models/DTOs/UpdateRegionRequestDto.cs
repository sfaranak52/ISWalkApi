using System.ComponentModel.DataAnnotations;

namespace ISWalksApi.Models.DTOs
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code hase to be minimum of 3 charecteres")]
        [MaxLength(3, ErrorMessage = "Code hase to be maximum of 3 charecteres")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name hase to be maximum of 100 charecteres")]
        public string Name { get; set; }
        public string? RegionImgUrl { get; set; }
    }
}
