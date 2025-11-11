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
    }
}
