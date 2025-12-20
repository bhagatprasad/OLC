using System.ComponentModel.DataAnnotations;

namespace OLC.Web.UI.Models
{

    public class EmailCampaign
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "Campaign name is required")]
        [StringLength(50, ErrorMessage = "Campaign name cannot exceed 50 characters")]
        [Display(Name = "Campaign Name")]
        public string CampaignName { get; set; }

        [Required(ErrorMessage = "Campaign Type is required")]
        [StringLength(20, ErrorMessage = "Campaign type cannot exceed 20 characters")]
        [Display(Name = "Campaign Type")]
        public string CampaignType { get; set; }

        [Required(ErrorMessage = "Campaign Description is required")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTimeOffset StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTimeOffset EndDate { get; set; }


        public long? CreatedBy { get; set; }

        public DateTimeOffset? CreatedOn { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }

        public bool? IsActive { get; set; } = true;
    }
}
