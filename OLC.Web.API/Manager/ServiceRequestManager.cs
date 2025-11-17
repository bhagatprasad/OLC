using OLC.Web.API.Models;
using System.Collections.Immutable;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;

namespace OLC.Web.API.Manager
{
    public class ServiceRequestManager : IServiceRequestManager
    {
        private readonly string connectionString;
        public ServiceRequestManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<ServiceRequest> GetServiceRequestByIdAsync(long ticketId)
        {
            ServiceRequest getServiceRequestById = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetServiceRequestByTicketId]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@ticketId", ticketId);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getServiceRequestById = new ServiceRequest();

                    getServiceRequestById.TicketId = Convert.ToInt64(item["TicketId"]);
                    getServiceRequestById.OrderId = item["OrderId"] != DBNull.Value ? Convert.ToInt64(item["OrderId"]) : null;
                    getServiceRequestById.UserId = item["UserId"] != DBNull.Value ? Convert.ToInt64(item["UserId"]) : null;
                    getServiceRequestById.Subject = item["Subject"] != DBNull.Value ? item["Subject"].ToString() : null;
                    getServiceRequestById.Message = item["Message"] != DBNull.Value ? item["Message"].ToString() : null;
                    getServiceRequestById.Category = item["Category"] != DBNull.Value ? item["Category"].ToString() : null;
                    getServiceRequestById.RequestReference = item["RequestReference"] != DBNull.Value ? item["RequestReference"].ToString() : null;
                    getServiceRequestById.Classification = item["Classification"] != DBNull.Value ? item["Classification"].ToString() : null;
                    getServiceRequestById.Priority = item["Priority"] != DBNull.Value ? item["Priority"].ToString() : null;
                    getServiceRequestById.StatusId = item["StatusId"] != DBNull.Value ? Convert.ToInt64(item["StatusId"]) : null;
                    getServiceRequestById.AssignTo = item["AssignTo"] != DBNull.Value ? Convert.ToInt64(item["AssignTo"]) : null;
                    getServiceRequestById.AssignBy = item["AssignBy"] != DBNull.Value ? Convert.ToInt64(item["AssignBy"]) : null;
                    getServiceRequestById.AssignedOn = item["AssignedOn"] != DBNull.Value ? (DateTimeOffset?)item["AssignedOn"] : null;
                    getServiceRequestById.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;
                    getServiceRequestById.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;
                    getServiceRequestById.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;
                    getServiceRequestById.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;
                    getServiceRequestById.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;

                }
            }
            return getServiceRequestById;
        }
        public async Task<List<ServiceRequest>> GetAllServiceRequestsAsync()
        {
            List<ServiceRequest> serviceRequests = new List<ServiceRequest>();

            ServiceRequest getServiceRequest = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllServiceRequests]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getServiceRequest = new ServiceRequest();

                    getServiceRequest.TicketId = Convert.ToInt64(item["TicketId"]);
                    getServiceRequest.OrderId = item["OrderId"] != DBNull.Value ? Convert.ToInt64(item["OrderId"]) : null;
                    getServiceRequest.UserId = item["UserId"] != DBNull.Value ? Convert.ToInt64(item["UserId"]) : null;
                    getServiceRequest.Subject = item["Subject"] != DBNull.Value ? item["Subject"].ToString() : null;
                    getServiceRequest.Message = item["Message"] != DBNull.Value ? item["Message"].ToString() : null;
                    getServiceRequest.Category = item["Category"] != DBNull.Value ? item["Category"].ToString() : null;
                    getServiceRequest.RequestReference = item["RequestReference"] != DBNull.Value ? item["RequestReference"].ToString() : null;
                    getServiceRequest.Classification = item["Classification"] != DBNull.Value ? item["Classification"].ToString() : null;
                    getServiceRequest.Priority = item["Priority"] != DBNull.Value ? item["Priority"].ToString() : null;
                    getServiceRequest.StatusId = item["StatusId"] != DBNull.Value ? Convert.ToInt64(item["StatusId"]) : null;
                    getServiceRequest.AssignTo = item["AssignTo"] != DBNull.Value ? Convert.ToInt64(item["AssignTo"]) : null;
                    getServiceRequest.AssignBy = item["AssignBy"] != DBNull.Value ? Convert.ToInt64(item["AssignBy"]) : null;
                    getServiceRequest.AssignedOn = item["AssignedOn"] != DBNull.Value ? (DateTimeOffset?)item["AssignedOn"] : null;
                    getServiceRequest.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;
                    getServiceRequest.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;
                    getServiceRequest.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;
                    getServiceRequest.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;
                    getServiceRequest.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;
                    serviceRequests.Add(getServiceRequest);
                }
            }
            return serviceRequests;
        }

        public async Task<bool> InsertServiceRequestAsync(ServiceRequest serviceRequest)
        {
            if (serviceRequest != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertServiceRequest]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@orderId", serviceRequest.OrderId);

                sqlCommand.Parameters.AddWithValue("@userId", serviceRequest.UserId);

                sqlCommand.Parameters.AddWithValue("@subject", serviceRequest.Subject);

                sqlCommand.Parameters.AddWithValue("@message", serviceRequest.Message);

                sqlCommand.Parameters.AddWithValue("@category", serviceRequest.Category);

                sqlCommand.Parameters.AddWithValue("@requestReference", serviceRequest.RequestReference);

                sqlCommand.Parameters.AddWithValue("@classification", serviceRequest.Classification);

                sqlCommand.Parameters.AddWithValue("@priority", serviceRequest.Priority);

                sqlCommand.Parameters.AddWithValue("@statusId", serviceRequest.StatusId);

                sqlCommand.Parameters.AddWithValue("@assignTo", serviceRequest.AssignTo);

                sqlCommand.Parameters.AddWithValue("@assignBy", serviceRequest.AssignBy);


                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }

        public async Task<bool> UpdateServiceRequestAsync(ServiceRequest serviceRequest)
        {
            if (serviceRequest != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateServiceRequest]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@ticketId", serviceRequest.TicketId);

                sqlCommand.Parameters.AddWithValue("@subject", serviceRequest.Subject);

                sqlCommand.Parameters.AddWithValue("@message", serviceRequest.Message);

                sqlCommand.Parameters.AddWithValue("@category", serviceRequest.Category);

                sqlCommand.Parameters.AddWithValue("@requestReference", serviceRequest.RequestReference);

                sqlCommand.Parameters.AddWithValue("@classification", serviceRequest.Classification);

                sqlCommand.Parameters.AddWithValue("@priority", serviceRequest.Priority);

                sqlCommand.Parameters.AddWithValue("@statusId", serviceRequest.StatusId);

                sqlCommand.Parameters.AddWithValue("@modifiedBy", serviceRequest.ModifiedBy);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteServiceRequestAsync(long ticketId)
        {
            if (ticketId != 0)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspDeleteServiceRequest]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@ticketId", ticketId);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }
            return false;
        }

        public async Task<bool> InsertServiceRequestRepliesAsync(ServiceRequestReplies serviceRequestReplies)
        {

            if (serviceRequestReplies != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertServiceRequestReply]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@ticketId", serviceRequestReplies.TicketId);
                sqlCommand.Parameters.AddWithValue("@replierId", serviceRequestReplies.ReplierId);
                sqlCommand.Parameters.AddWithValue("@message", serviceRequestReplies.Message);
                sqlCommand.Parameters.AddWithValue("@status", serviceRequestReplies.Status);
                sqlCommand.Parameters.AddWithValue("@isInternal", serviceRequestReplies.IsInternal);
                sqlCommand.Parameters.AddWithValue("@createdBy", serviceRequestReplies.IsInternal);
                sqlCommand.Parameters.AddWithValue("@modifiedBy", serviceRequestReplies.IsInternal);


                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }

        public async Task<List<ServiceRequestReplies>> GetServiceRequestRepliesByTicketIdAsync(long ticketId)
        {
            List<ServiceRequestReplies> serviceRequestRepliess = new List<ServiceRequestReplies>();

            ServiceRequestReplies getServiceRequestReplies = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetServiceRequestRepliesByTicketId]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@ticketId", ticketId);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getServiceRequestReplies = new ServiceRequestReplies();

                    getServiceRequestReplies.Id = Convert.ToInt64(item["Id"]);
                    getServiceRequestReplies.TicketId = item["TicketId"] != DBNull.Value ? Convert.ToInt64(item["TicketId"]) : null;
                    getServiceRequestReplies.ReplierId = item["ReplierId"] != DBNull.Value ? Convert.ToInt64(item["ReplierId"]) : null;
                    getServiceRequestReplies.Message = item["Message"] != DBNull.Value ? item["Message"].ToString() : null;
                    getServiceRequestReplies.Status = item["Status"] != DBNull.Value ? item["Status"].ToString() : null;
                    getServiceRequestReplies.IsInternal = item["IsInternal"] != DBNull.Value ? (bool?)item["IsInternal"] : null;
                    getServiceRequestReplies.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;
                    getServiceRequestReplies.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTime?)item["CreatedOn"] : null;
                    getServiceRequestReplies.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;
                    getServiceRequestReplies.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTime?)item["ModifiedOn"] : null;
                    getServiceRequestReplies.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;
                    serviceRequestRepliess.Add(getServiceRequestReplies);

                }
            }
            return serviceRequestRepliess;
        }

        public async Task<List<ServiceRequest>> GetServiceRequestByUserIdAsync(long userId)
        {
            List<ServiceRequest> serviceRequests = new List<ServiceRequest>();

            ServiceRequest serviceRequest = null;

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetServiceRequestByUserId]", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@userId", userId);
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            da.Fill(dt);
            sqlConnection.Close();
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow item in dt.Rows)
                {

                    serviceRequest = new ServiceRequest();

                    serviceRequest.TicketId = Convert.ToInt64(item["TicketId"]);
                    serviceRequest.OrderId = item["OrderId"] != DBNull.Value ? Convert.ToInt64(item["OrderId"]) : null;
                    serviceRequest.UserId = item["UserId"] != DBNull.Value ? Convert.ToInt64(item["UserId"]) : null;
                    serviceRequest.Subject = item["Subject"] != DBNull.Value ? item["Subject"].ToString() : null;
                    serviceRequest.Message = item["Message"] != DBNull.Value ? item["Message"].ToString() : null;
                    serviceRequest.Category = item["Category"] != DBNull.Value ? item["Category"].ToString() : null;
                    serviceRequest.RequestReference = item["RequestReference"] != DBNull.Value ? item["RequestReference"].ToString() : null;
                    serviceRequest.Classification = item["Classification"] != DBNull.Value ? item["Classification"].ToString() : null;
                    serviceRequest.Priority = item["Priority"] != DBNull.Value ? item["Priority"].ToString() : null;
                    serviceRequest.StatusId = item["StatusId"] != DBNull.Value ? Convert.ToInt64(item["StatusId"]) : null;
                    serviceRequest.AssignTo = item["AssignTo"] != DBNull.Value ? Convert.ToInt64(item["AssignTo"]) : null;
                    serviceRequest.AssignBy = item["AssignBy"] != DBNull.Value ? Convert.ToInt64(item["AssignBy"]) : null;
                    serviceRequest.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;
                    serviceRequest.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;
                    serviceRequest.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;
                    serviceRequest.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;
                    serviceRequest.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;

                    serviceRequests.Add(serviceRequest);
                }
            }
            return serviceRequests;

        }

        public async Task<List<ServiceRequestDetails>> GetAllServiceRequestsWithRepliesAsync()
        {
            List<ServiceRequestDetails> _serviceRequestDetailss = new List<ServiceRequestDetails>();

            var serviceRequests = await GetAllServiceRequestsAsync();

            var serviceRequestRepliess = await GetAllServiceRequestRepliesAsync();

            if (serviceRequests.Any())
            {
                foreach (var item in serviceRequests)
                {
                    ServiceRequestDetails _serviceRequestt = new ServiceRequestDetails();
                    _serviceRequestt.serviceRequest = item;

                    if (serviceRequestRepliess.Any())
                    {
                        var filteredReplies = serviceRequestRepliess.Where(x => x.TicketId == item.TicketId).ToList();

                        if (filteredReplies.Any())
                        {
                            _serviceRequestt.serviceRequestReplies = filteredReplies;
                        }
                    }
                    _serviceRequestDetailss.Add(_serviceRequestt);
                }
            }

            return _serviceRequestDetailss;
        }
        public async Task<List<ServiceRequestReplies>> GetAllServiceRequestRepliesAsync()
        {
            List<ServiceRequestReplies> serviceRequestRepliess = new List<ServiceRequestReplies>();

            ServiceRequestReplies getServiceRequestReplies = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllServiceRequestReplies]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;


            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getServiceRequestReplies = new ServiceRequestReplies();

                    getServiceRequestReplies.Id = Convert.ToInt64(item["Id"]);
                    getServiceRequestReplies.TicketId = item["TicketId"] != DBNull.Value ? Convert.ToInt64(item["TicketId"]) : null;
                    getServiceRequestReplies.ReplierId = item["ReplierId"] != DBNull.Value ? Convert.ToInt64(item["ReplierId"]) : null;
                    getServiceRequestReplies.Message = item["Message"] != DBNull.Value ? item["Message"].ToString() : null;
                    getServiceRequestReplies.Status = item["Status"] != DBNull.Value ? item["Status"].ToString() : null;
                    getServiceRequestReplies.IsInternal = item["IsInternal"] != DBNull.Value ? (bool?)item["IsInternal"] : null;
                    getServiceRequestReplies.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;
                    getServiceRequestReplies.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTime?)item["CreatedOn"] : null;
                    getServiceRequestReplies.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;
                    getServiceRequestReplies.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTime?)item["ModifiedOn"] : null;
                    getServiceRequestReplies.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;
                    serviceRequestRepliess.Add(getServiceRequestReplies);

                }
            }
            return serviceRequestRepliess;
        }

        public async Task<bool> CancelServiceRequestByTicketIdAsync(ServiceRequest serviceRequest)
        {
            if (serviceRequest != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspCancelServiceRequest]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@ticketId", serviceRequest.TicketId);
                sqlCommand.Parameters.AddWithValue("@modifiedBy", serviceRequest.ModifiedBy);
                sqlCommand.Parameters.AddWithValue("@statusId", serviceRequest.StatusId);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }
            return false;
        }

        public async Task<bool> AssingingServiceRequestAsync(ServiceRequest serviceRequest)
        {
            if (serviceRequest != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspAssigningServiceRequest]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@ticketId", serviceRequest.TicketId);
                sqlCommand.Parameters.AddWithValue("@statusId", serviceRequest.StatusId);
                sqlCommand.Parameters.AddWithValue("@assignTo", serviceRequest.AssignTo);
                sqlCommand.Parameters.AddWithValue("@assignedBy", serviceRequest.AssignBy);
                sqlCommand.Parameters.AddWithValue("@assignedOn", serviceRequest.AssignedOn);
                sqlCommand.Parameters.AddWithValue("@modifiedBy", serviceRequest.ModifiedBy);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }
            return false;
        }
    }
}