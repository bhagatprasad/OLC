namespace OLC.Web.UI.Models
{
    public class ServiceRequestDetails
    {
        public ServiceRequestDetails()
        {
            serviceRequest = new ServiceRequest();
            serviceRequestReplies = new List<ServiceRequestReplies>();
        }
        public ServiceRequest serviceRequest { get; set; }
        public List<ServiceRequestReplies> serviceRequestReplies { get; set; }
    }
}
