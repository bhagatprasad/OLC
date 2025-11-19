function ServiceRequestDetailsController() {
    var self = this;
    self.TicketId = null;
    var action = [];
    var dataObjects = [];
    self.ServiceRequestDetails = {};


    action.push("/ServiceRequest/GetServiceRequestByTicketId");
    self.init = function () {
        $(".se-pre-con").show();
        $("#errorState").addClass("d-none");

        self.TicketId = getQueryStringParameter("ticketId");
        console.log("Ticket Id:", self.TicketId);

        if (!self.TicketId) {
            self.showErrorState("Invalid  Ticket ID");
            return;
        }

        dataObjects.push({ ticketId: self.TicketId });

        var requests = actions.map((action, index) => {
            var ajaxConfig = {
                url: action,
                method: 'GET',
                timeout: 30000
            };
            if (index === 0) {
                ajaxConfig.data = dataObjects[0];
            }
            return $.ajax(ajaxConfig);
        });

        $.when.apply($, requests).done(function () {
            var responses = arguments;
            console.log('Service request details response:', responses);

            if (!responses[0] || !responses[0][0] || !responses[0][0].data) {
                self.showErrorState("Invalid response from server");
                return;
            }

            self.ServiceRequestDetails = responses[0][0].data;
            self.renderCards();
            $(".se-pre-con").hide();
        }).fail(function (error) {
            console.error('One or more requests failed:', error);
            self.showErrorState("Failed to load payment details");
            $(".se-pre-con").hide();
        });


        self.renderCards() = function () {
            const container = document.getElementById("serviceRequestCardsContainer");
            if (!container) return;

            const cardsHTML = [
                self.generateServiceRequestInfoCard(),
            ].join('');

            container.innerHTML = cardsHTML;
            self.bindCardData();
        }

        self.generateServiceRequestInfoCard = function () {
            return `
        <div class="col-12 col-md-6 col-lg-4">
            <div class="card service request-card">
                <div class="card-header bg-primary text-white d-flex align-items-center">
                    <i class="fas fa-ticket-alt me-2"></i>
                    <h5 class="mb-0 flex-grow-1">Service Request Details</h5>
                    <span class="badge bg-light text-dark">${data.status ?? 'Open'}</span>
                </div>

                <div class="card-body p-0">
                    <table class="table card-table">
                        <tbody>
                            <tr>
                                <td class="fw-semibold text-muted">Subject</td>
                                <td>${data.subject ?? '--'}</td>
                            </tr>
                            <tr>
                                <td class="fw-semibold text-muted">Message</td>
                                <td>${data.message ?? '--'}</td>
                            </tr>
                            <tr>
                                <td class="fw-semibold text-muted">Category</td>
                                <td>${data.category ?? '--'}</td>
                            </tr>
                            <tr>
                                <td class="fw-semibold text-muted">Priority</td>
                                <td>${data.priority ?? '--'}</td>
                            </tr>
                            <tr>
                                <td class="fw-semibold text-muted">Order ID</td>
                                <td>${data.orderId ?? '--'}</td>
                            </tr>
                            <tr>
                                <td class="fw-semibold text-muted">Classification</td>
                                <td>${data.classification ?? '--'}</td>
                            </tr>
                            <tr>
                                <td class="fw-semibold text-muted">Created</td>
                                <td>${data.createdOn ?? '--'}</td>
                            </tr>
                            <tr>
                                <td class="fw-semibold text-muted">Updated</td>
                                <td>${data.modifiedOn ?? '--'}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
       `;
        }

        self.bindCardData = function () {
            const data = self.ServiceRequestDetails;
            if (!data) return;
        }






    }