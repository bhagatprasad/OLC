using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public ServiceRequestService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<bool> AssingingServiceRequestAsync(ServiceRequest serviceRequest)
        {
            return await _repositoryFactory.SendAsync<ServiceRequest, bool>(HttpMethod.Post, "ServiceRequest/AssingingServiceRequestAsync", serviceRequest);
        }

        public async Task<bool> CancelServiceRequestByTicketIdAsync(ServiceRequest serviceRequest)
        {
            return await _repositoryFactory.SendAsync<ServiceRequest, bool>(HttpMethod.Post, "ServiceRequest/AssingingServiceRequestAsync", serviceRequest);
        }

        public async Task<bool> DeleteServiceRequestAsync(long ticketId)
        {
            var url = Path.Combine("ServiceRequest/DeleteServiceRequestAsync", ticketId.ToString());
            return await _repositoryFactory.SendAsync<bool>(HttpMethod.Delete, url);
        }

        public async Task<List<ServiceRequest>> GetAllServiceRequestsAsync()
        {
            return await _repositoryFactory.SendAsync<List<ServiceRequest>>(HttpMethod.Get, "ServiceRequest/GetAllServiceRequestsAsync");
        }

        public async Task<ServiceRequest> GetServiceRequestByIdAsync(long ticketId)
        {
            var url = Path.Combine("ServiceRequest/GetServiceRequestByIdAsync", ticketId.ToString());
            return await _repositoryFactory.SendAsync<ServiceRequest>(HttpMethod.Get, url);
        }

        public async Task<List<ServiceRequest>> GetServiceRequestByUserId(long userId)
        {
            var url = Path.Combine("ServiceRequest/GetServiceRequestByUserAsync", userId.ToString());
            return await _repositoryFactory.SendAsync<List<ServiceRequest>>(HttpMethod.Get, url);
        }

        public async Task<ServiceRequestDetails> GetServiceRequestWithRepliesAsync(long ticketId)
        {
            var url = Path.Combine("ServiceRequest/GetServiceRequestWithRepliesAsync", ticketId.ToString());
            return await _repositoryFactory.SendAsync<ServiceRequestDetails>(HttpMethod.Get, url);
        }

        public async Task<bool> InsertServiceRequestAsync(ServiceRequest serviceRequest)
        {
            return await _repositoryFactory.SendAsync<ServiceRequest, bool>(HttpMethod.Post, "ServiceRequest/InsertServiceRequestAsync", serviceRequest);
        }

        public async Task<bool> InsertServiceRequestRepliesAsync(ServiceRequestReplies serviceRequestReplies)
        {
            return await _repositoryFactory.SendAsync<ServiceRequestReplies, bool>(HttpMethod.Post, "ServiceRequest/InsertServiceRequestRepliesAsync", serviceRequestReplies);
        }

        public async Task<bool> UpdateServiceRequestAsync(ServiceRequest serviceRequest)
        {
            return await _repositoryFactory.SendAsync<ServiceRequest, bool>(HttpMethod.Post, "ServiceRequest/UpdateServiceRequestAsync", serviceRequest);
        }

    }
}
