function ServiceRequestController() {
    var self = this;

    self.ApplicationUser = {};
    self.UserServiceRequest = [];
    self.CurrentSelectedServiceRequest = {};

    self.init = function () {
        var userinfo = storageService.get('ApplicationUser');

        if (userinfo) {
            self.ApplicationUser = userinfo;
        }

        GetServiceRequest();
    };

    function GetServiceRequest() {
        $.ajax({
            url: '/ServiceRequest/GetServiceRequestByUserId',
            type: 'GET',
            data: { userId: self.ApplicationUser.Id },
            cache: false,
            success: function (response) {
                self.UserServiceRequest = response && response.data ? response.data : [];
                loadServiceRequests(self.UserServiceRequest);
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    function loadServiceRequests(serviceRequests) {
        $("#serviceRequestList").empty();

        serviceRequests.forEach(req => {
            const priorityClass =
                req.priority === "High" ? "bg-danger" :
                    req.priority === "Medium" ? "bg-warning text-dark" :
                        "bg-secondary";

            const statusText = mapStatus(req.statusId);
            const statusClass = mapStatusClass(req.statusId);

            $("#serviceRequestList").append(`
                <div class="ticket-card p-3 mb-3 border rounded shadow-sm" onclick="viewTicket(${req.ticketId})" style="cursor:pointer;">
                    <div class="d-flex justify-content-between align-items-center mb-1">
                        <h6 class="fw-bold mb-0">${req.subject}</h6>
                        <span class="badge ${priorityClass}">${req.priority ?? "Normal"}</span>
                    </div>
                    <p class="text-muted mb-0">
                        <i class="fa-solid fa-headset me-1"></i>${req.category ?? "N/A"} • 
                        <span class="badge ${statusClass}">${statusText}</span> • 
                        ${formatDate(req.createdOn)}
                    </p>
                </div>
            `);
        });
    }

    window.viewTicket = function (ticketId) {
        $.ajax({
            url: `/ServiceRequest/GetServiceRequestByIdAsync/${ticketId}`,
            type: 'GET',
            dataType: 'json',
            success: function (req) {
                if (!req) return;

                const details = `
                    <div class="row">
                        <div class="col-md-6">
                            <p><strong>Ticket ID:</strong> ${req.ticketId}</p>
                            <p><strong>Subject:</strong> ${req.subject}</p>
                            <p><strong>Category:</strong> ${req.category}</p>
                            <p><strong>Priority:</strong> ${req.priority}</p>
                            <p><strong>Status:</strong> ${mapStatus(req.statusId)}</p>
                        </div>
                        <div class="col-md-6">
                            <p><strong>Created On:</strong> ${formatDate(req.createdOn)}</p>
                            <p><strong>Modified On:</strong> ${formatDate(req.modifiedOn)}</p>
                            <p><strong>Assigned To:</strong> ${req.assignTo ?? 'N/A'}</p>
                            <p><strong>Request Reference:</strong> ${req.requestReference ?? 'N/A'}</p>
                        </div>
                    </div>
                    <hr>
                    <p><strong>Message:</strong></p>
                    <p class="text-muted">${req.message ?? "No message provided."}</p>
                `;

                $("#ticketDetails").html(details);
                new bootstrap.Modal(document.getElementById('ticketModal')).show();
            }
        });
    };

    function formatDate(date) {
        if (!date) return "N/A";
        return new Date(date).toLocaleString();
    }

    function mapStatus(statusId) {
        switch (statusId) {
            case 1: return "Open";
            case 2: return "In Progress";
            case 3: return "Closed";
            case 4: return "Waiting for Customer";
            default: return "Unknown";
        }
    }

    function mapStatusClass(statusId) {
        switch (statusId) {
            case 1: return "bg-primary";
            case 2: return "bg-info text-dark";
            case 3: return "bg-success";
            case 4: return "bg-warning text-dark";
            default: return "bg-secondary";
        }
    }

    function submitTicket() {

        var ticket = {
            TicketId: $("#TicketId").val() || 0,
            UserId: $("#UserId").val(),
            Subject: $("#Subject").val(),
            Category: $("#Category").val(),
            Priority: $("#Priority").val(),
            Message: $("#Message").val()
        };

        $.ajax({
            url: '/ServiceRequest/InsertOrUpdateServiceRequest',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(ticket),
            success: function (res) {
                if (res === true) {
                    alert("Ticket submitted successfully!");
                    window.location.href = "ServiceRequest/Index";
                }
            },
            error: function (err) {
                alert("Error submitting ticket!");
                console.log(err);
            }
        });



    }
}