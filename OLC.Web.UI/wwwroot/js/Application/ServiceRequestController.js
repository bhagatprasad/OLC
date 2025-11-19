function ServiceRequestController() {

    var self = this;
    self.ApplicationUser = {};
    self.UserServiceRequest = [];
    self.CurrentSelectedServiceRequest = {};
    self.categories = [];
    self.priorities = [];
    self.classifications = [];

    self.init = function () {

        self.categories = [
            "Technical Support",
            "Billing Inquiry",
            "Account Management",
            "Feature Request",
            "Bug Report"
        ];

        self.priorities = [
            "Low",
            "Medium",
            "High",
            "Urgent",
            "Critical"
        ];

        self.classifications = [
            "Issue",
            "Suggestion",
            "Query",
            "Feedback"
        ];

        var userinfo = storageService.get('ApplicationUser');

        if (userinfo) {
            self.ApplicationUser = userinfo;
        }

        self.categories.forEach(c => {
            $("#Category").append(`<option value="${c}">${c}</option>`);
        });

        self.priorities.forEach(p => {
            $("#Priority").append(`<option value="${p}">${p}</option>`);
        });
        self.classifications.forEach(p => {
            $("#Classification").append(`<option value="${p}">${p}</option>`);
        });

        GetServiceRequests();


        function GetServiceRequests() {
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
                    req.Priority === "High" ? "bg-danger" :
                        req.Priority === "Medium" ? "bg-warning text-dark" :
                            "bg-secondary";

                const statusText = mapStatus(req.StatusId);
                const statusClass = mapStatusClass(req.StatusId);

                $("#serviceRequestList").append(`
            <tr>
                <td>${req.TicketId}</td>
                <td>${req.OrderId}</td>
                <td>${req.Subject}</td>
                <td>${req.Message}</td>
                <td>${req.Category}</td>
                <td>${req.Classification || ''}</td>
                <td><span class="${priorityClass}">${req.Priority}</span></td>
                <td><span class="${statusClass}">${statusText}</span></td>
                <td><button class="btn btn-success btn-sm viewRequest" data-id="${req.TicketId}"><i class="fa fa-eye"></i></button>
                <button class="btn btn-danger btn-sm cancelRequest" data-id="${req.TicketId}"><i class="fa fa-ban"></i></button></td>
            </tr>
        `);
            });
        }

        $(document).on("click", ".viewRequest", function () {
            var id = $(this).data("id");
            window.location.href = `/ServiceRequest/ServiceRequestDetails?ticketId=${id}`;
        });

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

        $(document).on("click", "#addServiceRequest", function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("Iam getting from add button click");
        });

        $(document).on("click", "#closeSidebar", function () {
            $('#CreateTicketForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });


        $("#CreateTicketForm").on('submit', function (e) {
            e.preventDefault();

            var ticketDetails = {
                TicketId: 0,
                OrderId: $("#OrderId").val() || null,
                RequestReference: generateOrderReference(self.ApplicationUser.Id, 'SR'),
                Classification: $("#Classification").val(),
                UserId: self.ApplicationUser.Id,
                Subject: $("#Subject").val(),
                Message: $("#Message").val(),
                Category: $("#Category").val(),
                Priority: $("#Priority").val(),
                StatusId: statusConstants.New
            };

            var ticket = addCommonProperties(ticketDetails);

            console.log(ticket);

            $.ajax({
                url: '/ServiceRequest/InsertOrUpdateServiceRequest',
                type: 'POST',
                contentType: "application/json",
                data: JSON.stringify(ticket),
                success: function () {
                    $('#sidebar').removeClass('show');
                    $('.modal-backdrop').remove();
                    GetServiceRequests();
                },
                error: function (xhr) {
                    console.log(xhr);
                    alert("Error creating ticket");
                }
            });

        });

        ////$(document).on("click", ".viewRequest", function () {
        ////    let id = $(this).data("id");

        ////    $.ajax({
        ////        url: `/ServiceRequest/ServiceRequestDetails?ticketId = ${ id }`,
        ////        type: 'GET',
        ////        success: function (data) {
        ////            $("#Subject").val(data.subject);
        ////            $("#Message").val(data.message);
        ////            $("#Category").val(data.category);
        ////            $("#Priority").val(data.priority);
        ////            $("#OrderId").val(data.orderId);

        ////            $("#sidebar").addClass("open");
        ////            $("body").append('<div class="modal-backdrop fade show"></div>');
        ////        }
        ////    })
        ////});
        ////self.GetServiceRequestDetails = function (ticketId) {
        ////    $.ajax({
        ////        url: `/ServiceRequest/GetServiceRequestByIdAsync?ticketId=${ticketId}`,
        ////        type: 'GET',
        ////        success: function (data) {

        ////            $("#serviceRequestCardsContainer").html(`
        ////                 <div class="col-12 col-md-6 col-lg-4">
        ////                    <div class="card payment-card">
        ////                        <div class="card-header bg-primary text-white d-flex align-items-center">
        ////                             <i class="fas fa-ticket-alt me-2"></i>
        ////                            <h5 class="mb-0 flex-grow-1">Service Request Details</h5>
        ////                            <span class="badge bg-light text-dark">${data.status ?? 'Open'}</span>
        ////                        </div>

        ////                    <div class="card-body p-0">
        ////                        <table class="table card-table">
        ////                        <tbody>
        ////                            <tr>
        ////                                <td class="fw-semibold text-muted">Subject</td>
        ////                                    <td>${data.subject ?? '--'}</td>
        ////                            </tr>
        ////                            <tr>
        ////                                <td class="fw-semibold text-muted">Message</td>
        ////                                <td>${data.message ?? '--'}</td>
        ////                            </tr>
        ////                            <tr>
        ////                                <td class="fw-semibold text-muted">Category</td>
        ////                                <td>${data.category ?? '--'}</td>
        ////                            </tr>
        ////                            <tr>
        ////                                <td class="fw-semibold text-muted">Priority</td>
        ////                                <td>${data.priority ?? '--'}</td>
        ////                            </tr>
        ////                            <tr>
        ////                                <td class="fw-semibold text-muted">Order ID</td>
        ////                                <td>${data.orderId ?? '--'}</td>
        ////                            </tr>
        ////                            <tr>
        ////                                <td class="fw-semibold text-muted">Classification</td>
        ////                                <td>${data.classification ?? '--'}</td>
        ////                            </tr>
        ////                            <tr>
        ////                                <td class="fw-semibold text-muted">Created</td>
        ////                                <td>${data.createdOn ?? '--'}</td>
        ////                            </tr>
        ////                            <tr>
        ////                                <td class="fw-semibold text-muted">Updated</td>
        ////                                <td>${data.modifiedOn ?? '--'}</td>
        ////                            </tr>
        ////                        </tbody>
        ////                    </table>
        ////                </div>
        ////            </div>
        ////        </div>
        ////            `);
        ////        }
        ////    });
        ////};
       
    };
}