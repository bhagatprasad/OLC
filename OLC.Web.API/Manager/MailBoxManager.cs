using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class MailBoxManager : IMailBoxManager
    {
        private readonly string connectionString;

        public MailBoxManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<MailBox>> GetAllMailBoxesAsync()
        {
            List<MailBox> getAllMailBoxes = new List<MailBox>();

            MailBox getMailBox = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllMailBox]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    getMailBox = new MailBox();
                    getMailBox.Id = Convert.ToInt64(item["Id"]);
                    getMailBox.MessageId = item["MessageId"] != DBNull.Value ? (Guid)item["MessageId"] : Guid.Empty;
                    getMailBox.ReferenceId = item["ReferenceId"] != DBNull.Value ? item["ReferenceId"].ToString() : null;

                    getMailBox.TemplateId = item["TemplateId"] != DBNull.Value ? (long?)item["TemplateId"] : null;
                    getMailBox.TemplateCode = item["TemplateCode"]?.ToString();

                    getMailBox.SenderEmail = item["SenderEmail"]?.ToString();
                    getMailBox.SenderName = item["SenderName"]?.ToString();
                    getMailBox.SenderType = item["SenderType"]?.ToString();

                    getMailBox.RecipientEmail = item["RecipientEmail"]?.ToString();
                    getMailBox.RecipientName = item["RecipientName"]?.ToString();
                    getMailBox.RecipientType = item["RecipientType"]?.ToString();
                    getMailBox.UserId = item["UserId"] != DBNull.Value ? (long?)item["UserId"] : null;

                    getMailBox.Subject = item["Subject"]?.ToString();
                    getMailBox.HtmlContent = item["HtmlContent"]?.ToString();
                    getMailBox.PlainContent = item["PlainContent"]?.ToString();
                    getMailBox.Variables = item["Variables"]?.ToString();

                    getMailBox.HasAttachments = item["HasAttachments"] != DBNull.Value && Convert.ToBoolean(item["HasAttachments"]);
                    getMailBox.AttachmentPaths = item["AttachmentPaths"]?.ToString();

                    getMailBox.Category = item["Category"]?.ToString();
                    getMailBox.CampaignId = item["CampaignId"]?.ToString();
                    getMailBox.Tags = item["Tags"]?.ToString();

                    getMailBox.Status = item["Status"]?.ToString();
                    getMailBox.DeliveryStatus = item["DeliveryStatus"]?.ToString();
                    getMailBox.Priority = item["Priority"]?.ToString();

                    getMailBox.ScheduledFor = item["ScheduledFor"] != DBNull.Value ? (DateTimeOffset?)item["ScheduledFor"] : null;
                    getMailBox.SentOn = item["SentOn"] != DBNull.Value ? (DateTimeOffset?)item["SentOn"] : null;
                    getMailBox.DeliveredOn = item["DeliveredOn"] != DBNull.Value ? (DateTimeOffset?)item["DeliveredOn"] : null;

                    getMailBox.Provider = item["Provider"]?.ToString();
                    getMailBox.ProviderMessageId = item["ProviderMessageId"]?.ToString();
                    getMailBox.ProviderResponse = item["ProviderResponse"]?.ToString();

                    getMailBox.FailureReason = item["FailureReason"]?.ToString();
                    getMailBox.FailureCode = item["FailureCode"]?.ToString();

                    getMailBox.RetryCount = item["RetryCount"] != DBNull.Value ? Convert.ToInt32(item["RetryCount"]) : 0;
                    getMailBox.MaxRetries = item["MaxRetries"] != DBNull.Value ? Convert.ToInt32(item["MaxRetries"]) : 0;
                    getMailBox.NextRetry = item["NextRetry"] != DBNull.Value ? (DateTimeOffset?)item["NextRetry"] : null;

                    getMailBox.OpenCount = item["OpenCount"] != DBNull.Value ? Convert.ToInt32(item["OpenCount"]) : 0;
                    getMailBox.FirstOpenedOn = item["FirstOpenedOn"] != DBNull.Value ? (DateTimeOffset?)item["FirstOpenedOn"] : null;
                    getMailBox.LastOpenedOn = item["LastOpenedOn"] != DBNull.Value ? (DateTimeOffset?)item["LastOpenedOn"] : null;

                    getMailBox.ClickCount = item["ClickCount"] != DBNull.Value ? Convert.ToInt32(item["ClickCount"]) : 0;
                    getMailBox.FirstClickedOn = item["FirstClickedOn"] != DBNull.Value ? (DateTimeOffset?)item["FirstClickedOn"] : null;
                    getMailBox.LastClickedOn = item["LastClickedOn"] != DBNull.Value ? (DateTimeOffset?)item["LastClickedOn"] : null;

                    getMailBox.SenderIP = item["SenderIP"]?.ToString();
                    getMailBox.OpenedIP = item["OpenedIP"]?.ToString();
                    getMailBox.ClickedIP = item["ClickedIP"]?.ToString();
                    getMailBox.UserAgent = item["UserAgent"]?.ToString();
                    getMailBox.DeviceType = item["DeviceType"]?.ToString();

                    getMailBox.CreatedBy = item["CreatedBy"] != DBNull.Value ? (long?)item["CreatedBy"] : null;
                    getMailBox.CreatedOn = (DateTimeOffset)item["CreatedOn"];
                    getMailBox.UpdatedBy = item["UpdatedBy"] != DBNull.Value ? (long?)item["UpdatedBy"] : null;
                    getMailBox.UpdatedOn = item["UpdatedOn"] != DBNull.Value ? (DateTimeOffset?)item["UpdatedOn"] : null;

                    getMailBox.Archived = item["Archived"] != DBNull.Value && Convert.ToBoolean(item["Archived"]);
                    getMailBox.ArchivedOn = item["ArchivedOn"] != DBNull.Value ? (DateTimeOffset?)item["ArchivedOn"] : null;

                    getMailBox.UnsubscribeToken = item["UnsubscribeToken"] != DBNull.Value ? (Guid?)item["UnsubscribeToken"] : null;
                    getMailBox.IsUnsubscribed = item["IsUnsubscribed"] != DBNull.Value && Convert.ToBoolean(item["IsUnsubscribed"]);
                    getMailBox.UnsubscribedOn = item["UnsubscribedOn"] != DBNull.Value ? (DateTimeOffset?)item["UnsubscribedOn"] : null;
                    getMailBox.UnsubscribeReason = item["UnsubscribeReason"]?.ToString();

                    getMailBox.IsEncrypted = item["IsEncrypted"] != DBNull.Value && Convert.ToBoolean(item["IsEncrypted"]);
                    getMailBox.EncryptionKey = item["EncryptionKey"]?.ToString();
                    getAllMailBoxes.Add(getMailBox);
                }
            }
            return getAllMailBoxes;
        }

        public async Task<MailBox> GetMailBoxByIdAsync(long id)
        {
            MailBox getMailBox = null;
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetMailboxById]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@id", id);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    getMailBox = new MailBox();
                    getMailBox.Id = Convert.ToInt64(item["Id"]);
                    getMailBox.MessageId = item["MessageId"] != DBNull.Value ? (Guid)item["MessageId"] : Guid.Empty;
                    getMailBox.ReferenceId = item["ReferenceId"] != DBNull.Value ? item["ReferenceId"].ToString() : null;

                    getMailBox.TemplateId = item["TemplateId"] != DBNull.Value ? (long?)item["TemplateId"] : null;
                    getMailBox.TemplateCode = item["TemplateCode"]?.ToString();

                    getMailBox.SenderEmail = item["SenderEmail"]?.ToString();
                    getMailBox.SenderName = item["SenderName"]?.ToString();
                    getMailBox.SenderType = item["SenderType"]?.ToString();

                    getMailBox.RecipientEmail = item["RecipientEmail"]?.ToString();
                    getMailBox.RecipientName = item["RecipientName"]?.ToString();
                    getMailBox.RecipientType = item["RecipientType"]?.ToString();
                    getMailBox.UserId = item["UserId"] != DBNull.Value ? (long?)item["UserId"] : null;

                    getMailBox.Subject = item["Subject"]?.ToString();
                    getMailBox.HtmlContent = item["HtmlContent"]?.ToString();
                    getMailBox.PlainContent = item["PlainContent"]?.ToString();
                    getMailBox.Variables = item["Variables"]?.ToString();

                    getMailBox.HasAttachments = item["HasAttachments"] != DBNull.Value && Convert.ToBoolean(item["HasAttachments"]);
                    getMailBox.AttachmentPaths = item["AttachmentPaths"]?.ToString();

                    getMailBox.Category = item["Category"]?.ToString();
                    getMailBox.CampaignId = item["CampaignId"]?.ToString();
                    getMailBox.Tags = item["Tags"]?.ToString();

                    getMailBox.Status = item["Status"]?.ToString();
                    getMailBox.DeliveryStatus = item["DeliveryStatus"]?.ToString();
                    getMailBox.Priority = item["Priority"]?.ToString();

                    getMailBox.ScheduledFor = item["ScheduledFor"] != DBNull.Value ? (DateTimeOffset?)item["ScheduledFor"] : null;
                    getMailBox.SentOn = item["SentOn"] != DBNull.Value ? (DateTimeOffset?)item["SentOn"] : null;
                    getMailBox.DeliveredOn = item["DeliveredOn"] != DBNull.Value ? (DateTimeOffset?)item["DeliveredOn"] : null;

                    getMailBox.Provider = item["Provider"]?.ToString();
                    getMailBox.ProviderMessageId = item["ProviderMessageId"]?.ToString();
                    getMailBox.ProviderResponse = item["ProviderResponse"]?.ToString();

                    getMailBox.FailureReason = item["FailureReason"]?.ToString();
                    getMailBox.FailureCode = item["FailureCode"]?.ToString();

                    getMailBox.RetryCount = item["RetryCount"] != DBNull.Value ? Convert.ToInt32(item["RetryCount"]) : 0;
                    getMailBox.MaxRetries = item["MaxRetries"] != DBNull.Value ? Convert.ToInt32(item["MaxRetries"]) : 0;
                    getMailBox.NextRetry = item["NextRetry"] != DBNull.Value ? (DateTimeOffset?)item["NextRetry"] : null;

                    getMailBox.OpenCount = item["OpenCount"] != DBNull.Value ? Convert.ToInt32(item["OpenCount"]) : 0;
                    getMailBox.FirstOpenedOn = item["FirstOpenedOn"] != DBNull.Value ? (DateTimeOffset?)item["FirstOpenedOn"] : null;
                    getMailBox.LastOpenedOn = item["LastOpenedOn"] != DBNull.Value ? (DateTimeOffset?)item["LastOpenedOn"] : null;

                    getMailBox.ClickCount = item["ClickCount"] != DBNull.Value ? Convert.ToInt32(item["ClickCount"]) : 0;
                    getMailBox.FirstClickedOn = item["FirstClickedOn"] != DBNull.Value ? (DateTimeOffset?)item["FirstClickedOn"] : null;
                    getMailBox.LastClickedOn = item["LastClickedOn"] != DBNull.Value ? (DateTimeOffset?)item["LastClickedOn"] : null;

                    getMailBox.SenderIP = item["SenderIP"]?.ToString();
                    getMailBox.OpenedIP = item["OpenedIP"]?.ToString();
                    getMailBox.ClickedIP = item["ClickedIP"]?.ToString();
                    getMailBox.UserAgent = item["UserAgent"]?.ToString();
                    getMailBox.DeviceType = item["DeviceType"]?.ToString();

                    getMailBox.CreatedBy = item["CreatedBy"] != DBNull.Value ? (long?)item["CreatedBy"] : null;
                    getMailBox.CreatedOn = (DateTimeOffset)item["CreatedOn"];
                    getMailBox.UpdatedBy = item["UpdatedBy"] != DBNull.Value ? (long?)item["UpdatedBy"] : null;
                    getMailBox.UpdatedOn = item["UpdatedOn"] != DBNull.Value ? (DateTimeOffset?)item["UpdatedOn"] : null;

                    getMailBox.Archived = item["Archived"] != DBNull.Value && Convert.ToBoolean(item["Archived"]);
                    getMailBox.ArchivedOn = item["ArchivedOn"] != DBNull.Value ? (DateTimeOffset?)item["ArchivedOn"] : null;

                    getMailBox.UnsubscribeToken = item["UnsubscribeToken"] != DBNull.Value ? (Guid?)item["UnsubscribeToken"] : null;
                    getMailBox.IsUnsubscribed = item["IsUnsubscribed"] != DBNull.Value && Convert.ToBoolean(item["IsUnsubscribed"]);
                    getMailBox.UnsubscribedOn = item["UnsubscribedOn"] != DBNull.Value ? (DateTimeOffset?)item["UnsubscribedOn"] : null;
                    getMailBox.UnsubscribeReason = item["UnsubscribeReason"]?.ToString();

                    getMailBox.IsEncrypted = item["IsEncrypted"] != DBNull.Value && Convert.ToBoolean(item["IsEncrypted"]);
                    getMailBox.EncryptionKey = item["EncryptionKey"]?.ToString();
                    
                }
            }
            return getMailBox;

        }

        public async Task<bool> InsertMailBoxAsync(MailBox mailbox)
        {
            if (mailbox != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand cmd = new SqlCommand("[dbo].[uspInsertMailBox]", sqlConnection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MessageId", mailbox.MessageId);
                cmd.Parameters.AddWithValue("@ReferenceId", mailbox.ReferenceId);

                cmd.Parameters.AddWithValue("@TemplateId", mailbox.TemplateId);
                cmd.Parameters.AddWithValue("@TemplateCode", mailbox.TemplateCode);

                cmd.Parameters.AddWithValue("@SenderEmail", mailbox.SenderEmail);
                cmd.Parameters.AddWithValue("@SenderName", mailbox.SenderName);
                cmd.Parameters.AddWithValue("@SenderType", mailbox.SenderType);

                cmd.Parameters.AddWithValue("@RecipientEmail", mailbox.RecipientEmail);
                cmd.Parameters.AddWithValue("@RecipientName", mailbox.RecipientName);
                cmd.Parameters.AddWithValue("@RecipientType", mailbox.RecipientType);
                cmd.Parameters.AddWithValue("@UserId", mailbox.UserId);

                cmd.Parameters.AddWithValue("@Subject", mailbox.Subject);
                cmd.Parameters.AddWithValue("@HtmlContent", mailbox.HtmlContent);
                cmd.Parameters.AddWithValue("@PlainContent", mailbox.PlainContent);
                cmd.Parameters.AddWithValue("@Variables", mailbox.Variables);

                cmd.Parameters.AddWithValue("@HasAttachments", mailbox.HasAttachments);
                cmd.Parameters.AddWithValue("@AttachmentPaths", mailbox.AttachmentPaths);

                cmd.Parameters.AddWithValue("@Category", mailbox.Category);
                cmd.Parameters.AddWithValue("@CampaignId", mailbox.CampaignId);
                cmd.Parameters.AddWithValue("@Tags", mailbox.Tags);

                cmd.Parameters.AddWithValue("@Status", mailbox.Status);
                cmd.Parameters.AddWithValue("@DeliveryStatus", mailbox.DeliveryStatus);
                cmd.Parameters.AddWithValue("@Priority", mailbox.Priority);

                cmd.Parameters.AddWithValue("@ScheduledFor", mailbox.ScheduledFor);
                cmd.Parameters.AddWithValue("@SentOn", mailbox.SentOn);
                cmd.Parameters.AddWithValue("@DeliveredOn", mailbox.DeliveredOn);

                cmd.Parameters.AddWithValue("@Provider", mailbox.Provider);
                cmd.Parameters.AddWithValue("@ProviderMessageId", mailbox.ProviderMessageId);
                cmd.Parameters.AddWithValue("@ProviderResponse", mailbox.ProviderResponse);

                cmd.Parameters.AddWithValue("@FailureReason", mailbox.FailureReason);
                cmd.Parameters.AddWithValue("@FailureCode", mailbox.FailureCode);
                cmd.Parameters.AddWithValue("@RetryCount", mailbox.RetryCount);
                cmd.Parameters.AddWithValue("@MaxRetries", mailbox.MaxRetries);
                cmd.Parameters.AddWithValue("@NextRetry", mailbox.NextRetry);

                cmd.Parameters.AddWithValue("@SenderIP", mailbox.SenderIP);
                cmd.Parameters.AddWithValue("@OpenedIP", mailbox.OpenedIP);
                cmd.Parameters.AddWithValue("@ClickedIP", mailbox.ClickedIP);
                cmd.Parameters.AddWithValue("@UserAgent", mailbox.UserAgent);
                cmd.Parameters.AddWithValue("@DeviceType", mailbox.DeviceType);

                cmd.Parameters.AddWithValue("@CreatedBy", mailbox.CreatedBy);

                cmd.Parameters.AddWithValue("@UnsubscribeToken", mailbox.UnsubscribeToken);
                cmd.Parameters.AddWithValue("@IsUnsubscribed", mailbox.IsUnsubscribed);
                cmd.Parameters.AddWithValue("@UnsubscribedOn", mailbox.UnsubscribedOn);
                cmd.Parameters.AddWithValue("@UnsubscribeReason", mailbox.UnsubscribeReason);

                cmd.Parameters.AddWithValue("@IsEncrypted", mailbox.IsEncrypted);
                cmd.Parameters.AddWithValue("@EncryptionKey", mailbox.EncryptionKey);

                cmd.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }
    }
}
