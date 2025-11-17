using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IServiceRequest
    {
        Task<ServiceRequest> GetServiceRequestByIdAsync(long ticketId);
        Task<List<ServiceRequest>> GetAllServiceRequestsAsync();
        Task<bool> InsertServiceRequestAsync(ServiceRequest serviceRequest);
        Task<bool> UpdateServiceRequestAsync(ServiceRequest serviceRequest);
        Task<bool> DeleteServiceRequestAsync(long ticketId);
        Task<List<ServiceRequest>> GetServiceRequestByUserId(long userId);
        Task<bool> CancelServiceRequestByTicketIdAsync(ServiceRequest serviceRequest);
        Task<bool> AssingingServiceRequestAsync(ServiceRequest serviceRequest);
    }
}
