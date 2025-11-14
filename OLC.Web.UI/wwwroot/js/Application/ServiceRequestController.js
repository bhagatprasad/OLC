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
                loadServiceRequests();
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    function loadServiceRequests() {
        $("#serviceRequestList").empty();

        self.UserServiceRequest.forEach(req => {
            const priorityClass =
                req.priority === "High" ? "bg-danger" :
                    req.priority === "Medium" ? "bg-warning text-dark" :
                        "bg-secondary";

            const statusText = mapStatus(req.statusId);
            const statusClass = mapStatusClass(req.statusId);

            $("#serviceRequestList").append(`
                <tr>
                <td>
                ${req.TicketId}
                </td>
                 <td>
                ${req.OrderId}
                </td>
                 <td>
                ${req.UserId}
                </td>
                 <td>
                ${req.Subject}
                </td>
                 <td>
                ${req.Message}
                </td>
                 <td>
                ${req.Category}
                </td>
                    <td>
                ${req.OrderId}
                </td>
                 <td>
                ${req.UserId}
                </td>
                 <td>
                ${req.Subject}
                </td>
                 <td>
                ${req.Message}
                </td>
                 <td>
                ${req.Category}
                </td>
                </tr>
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
}