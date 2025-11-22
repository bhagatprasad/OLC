using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IServiceRequestService
    {
        Task<ServiceRequest> GetServiceRequestByIdAsync(long ticketId);
        Task<List<ServiceRequest>> GetAllServiceRequestsAsync();
        Task<bool> InsertServiceRequestAsync(ServiceRequest serviceRequest);
        Task<bool> UpdateServiceRequestAsync(ServiceRequest serviceRequest);
        Task<bool> DeleteServiceRequestAsync(long ticketId);
        Task<List<ServiceRequest>> GetServiceRequestByUserId(long userId);
        Task<bool> CancelServiceRequestByTicketIdAsync(ServiceRequest serviceRequest);
        Task<bool> AssingingServiceRequestAsync(ServiceRequest serviceRequest);
        Task<ServiceRequestDetails> GetServiceRequestWithRepliesAsync(long ticketId);
        Task<bool> InsertServiceRequestRepliesAsync(ServiceRequestReplies serviceRequestReplies);
    }
}
