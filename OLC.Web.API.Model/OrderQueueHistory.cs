using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC.Web.API.Models
{
   public class OrderQueueHistory: OrderQueue
    {

        public long Id { get; set; }
        public long OrderQueueId { get; set; }
        public long? PaymentOrderId { get; set; }
        public long? ExecutiveId { get; set; }

        public string? OrderReference { get; set; }
        public int Priority { get; set; }

        public DateTimeOffset? AssignedOn { get; set; }
        public DateTimeOffset? ProcessingStartedOn { get; set; }
        public DateTimeOffset? ProcessingCompletedOn { get; set; }

        public string? QueueStatus { get; set; }
        public int? RetryCount { get; set; }
        public string? FailureReason { get; set; }

        public string? Metadata { get; set; }
        public string? Description { get; set; }
        public string ActionType { get; set; } = string.Empty;

        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }

        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }

        public bool IsActive { get; set; }
    }
}
