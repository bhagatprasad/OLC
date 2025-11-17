namespace OLC.Web.API.Models
{
    public class ServiceRequestDetails
    {
            public ServiceRequest ServiceRequest { get; set; }
            public List<ServiceRequestReplies> ServiceRequestReplies { get; set; }
    }
}
