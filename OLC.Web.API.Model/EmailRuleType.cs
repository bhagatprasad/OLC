using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC.Web.API.Models
{
    public class EmailRuleType
    {
        public long Id { get; set; }

        public string RuleCode { get; set; } 

        public string? RuleName { get; set; }

        public string? Description { get; set; }

        public long? CreatedBy { get; set; }

        public DateTimeOffset? CreatedOn { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }

        public bool? IsActive { get; set; }
    }
}
