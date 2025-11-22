using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IServiceRequestManager
    {
        Task<ServiceRequest> GetServiceRequestByIdAsync(long ticketId);
        Task<List<ServiceRequest>> GetAllServiceRequestsAsync();
        Task<bool> InsertServiceRequestAsync(ServiceRequest serviceRequest);
        Task<bool> UpdateServiceRequestAsync(ServiceRequest serviceRequest);
        Task<bool> DeleteServiceRequestAsync(long ticketId);
        Task<bool>InsertServiceRequestRepliesAsync(ServiceRequestReplies serviceRequestReplies);
        Task<List<ServiceRequestReplies>> GetServiceRequestRepliesByTicketIdAsync(long ticketId);
        Task<List<ServiceRequest>> GetServiceRequestByUserIdAsync(long userId);
        Task<List<ServiceRequestDetails>> GetAllServiceRequestsWithRepliesAsync();
        Task<ServiceRequestDetails> GetServiceRequestWithRepliesAsync(long ticketId);
        Task<List<ServiceRequestReplies>> GetAllServiceRequestRepliesAsync();
        Task<bool> CancelServiceRequestByTicketIdAsync(ServiceRequest serviceRequest);
        Task<bool> AssingingServiceRequestAsync(ServiceRequest serviceRequest);
    }
}
