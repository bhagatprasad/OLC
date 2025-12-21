using System.ComponentModel.DataAnnotations;

namespace OLC.Web.UI.Models
{
    public class CampaignType
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Campaign name is required")]
        [StringLength(50, ErrorMessage = "Campaign name cannot exceed 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campaign code is required")]
        [StringLength(10, ErrorMessage = "Campaign code cannot exceed 50 characters")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Campaign Description is required")]
        [StringLength(500, ErrorMessage = "Campaign Description cannot exceed 50 characters")]
        public string Description { get; set; }

        public long? CreatedBy { get; set; }

        public DateTimeOffset? CreatedOn { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }

        public bool? IsActive { get; set; }
    }
}
