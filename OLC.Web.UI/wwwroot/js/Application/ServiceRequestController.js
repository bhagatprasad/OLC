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
            const container = $("#serviceRequestList");
            container.empty();

            // Create responsive container
            container.html(`
        <div class="table-responsive d-none d-md-block">
            <table class="table table-striped table-hover">
                <thead>
                    <tr>
                        <th>Ticket Referance</th>
                        <th>Order ID</th>
                        <th>Subject</th>
                        <th>Message</th>
                        <th>Category</th>
                        <th>Classification</th>
                        <th>Priority</th>
                        <th>Status</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody id="serviceRequestTableBody"></tbody>
            </table>
        </div>
        <div class="row d-md-none" id="serviceRequestCards"></div>
    `);

            const tableBody = $("#serviceRequestTableBody");
            const cardsContainer = $("#serviceRequestCards");

            self.UserServiceRequest.forEach((req, index) => {
                const priorityClass = getPriorityClass(req.Priority);
                const statusText = mapStatus(req.StatusId);
                const statusClass = mapStatusClass(req.StatusId);

                // Alternate row class for table
                const rowClass = index % 2 === 0 ? '' : 'table-active';

                // Table row (desktop)
                tableBody.append(`
            <tr class="${rowClass}">
                <td>${req.RequestReference}</td>
                <td>${req.OrderId}</td>
                <td>${req.Subject}</td>
                <td class="text-truncate" style="max-width: 200px;" title="${req.Message}">${req.Message}</td>
                <td>${req.Category}</td>
                <td>${req.Classification || ''}</td>
                <td><span class="badge ${priorityClass}">${req.Priority}</span></td>
                <td><span class="badge ${statusClass}">${statusText}</span></td>
                <td>
                    <div class="btn-group btn-group-sm">
                        <button class="btn btn-success viewRequest" data-id="${req.TicketId}" title="View Details">
                            <i class="fa fa-eye"></i>
                        </button>
                        <button class="btn btn-danger cancelRequest" data-id="${req.TicketId}" title="Cancel Request">
                            <i class="fa fa-ban"></i>
                        </button>
                    </div>
                </td>
            </tr>
        `);

                cardsContainer.append(`
            <div class="col-12 mb-3">
                <div class="card service-request-card ${index % 2 === 0 ? 'border-primary' : 'border-secondary'}">
                    <div class="card-header d-flex justify-content-between align-items-center">
                        <strong>Ticket #${req.RequestReference}</strong>
                        <span class="badge ${priorityClass}">${req.Priority}</span>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-6">
                                <small class="text-muted">Order ID:</small>
                                <p class="mb-1">${req.OrderId}</p>
                            </div>
                            <div class="col-6">
                                <small class="text-muted">Category:</small>
                                <p class="mb-1">${req.Category}</p>
                            </div>
                        </div>
                        
                        <div class="row mt-2">
                            <div class="col-12">
                                <small class="text-muted">Subject:</small>
                                <p class="mb-1 fw-bold">${req.Subject}</p>
                            </div>
                        </div>
                        
                        <div class="row mt-2">
                            <div class="col-12">
                                <small class="text-muted">Message:</small>
                                <p class="mb-1 text-truncate">${req.Message}</p>
                            </div>
                        </div>
                        
                        <div class="row mt-2">
                            <div class="col-6">
                                <small class="text-muted">Classification:</small>
                                <p class="mb-1">${req.Classification || 'N/A'}</p>
                            </div>
                            <div class="col-6">
                                <small class="text-muted">Status:</small>
                                <p class="mb-1"><span class="badge ${statusClass}">${statusText}</span></p>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <div class="btn-group w-100">
                            <button class="btn btn-success viewRequest" data-id="${req.TicketId}">
                                <i class="fa fa-eye"></i> View
                            </button>
                            <button class="btn btn-danger cancelRequest" data-id="${req.TicketId}">
                                <i class="fa fa-ban"></i> Cancel
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        `);
            });

            attachEventHandlers();
        }
        function getPriorityClass(priority) {
            switch (priority?.toLowerCase()) {
                case 'high': return 'bg-danger';
                case 'medium': return 'bg-warning text-dark';
                case 'low': return 'bg-secondary';
                default: return 'bg-light text-dark';
            }
        }
        function attachEventHandlers() {
            $(".viewRequest").off('click').on('click', function () {
                const ticketId = $(this).data('id');
                viewServiceRequest(ticketId);
            });

            $(".cancelRequest").off('click').on('click', function () {
                const ticketId = $(this).data('id');
                cancelServiceRequest(ticketId);
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

    };
}