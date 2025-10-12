﻿namespace OLC.Web.API.Models
{
    public class User
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
        public long? RoleId { get; set; }
        public DateTimeOffset? LastPasswordChangedOn { get; set; }
        public bool? IsBlocked { get; set; }
        public bool? IsApproved { get; set; }
        public long? ApprovedBy { get; set; }
        public DateTimeOffset? ApprovedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
