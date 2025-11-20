function ServiceRequestDetailsController() {
    var self = this;
    self.TicketId = null;
    var actions = [];
    var dataObjects = [];
    self.ApplicationUser = {};
    self.ServiceRequestDetails = {};

    actions.push("/ServiceRequest/GetServiceRequestWithReplies");

    self.init = function () {
        var appUserInfo = storageService.get('ApplicationUser');
        console.log(appUserInfo);
        if (appUserInfo) {
            self.ApplicationUser = appUserInfo;
        }

        $(".se-pre-con").show();

        self.TicketId = getQueryStringParameter("ticketId");
        console.log("Ticket Id:", self.TicketId);

        if (!self.TicketId) {
            self.showErrorState("Invalid Ticket ID");
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

            self.ServiceRequestDetails = responses[0].data;
            self.renderConversation();

        }).fail(function (error) {
            console.error('One or more requests failed:', error);
            $(".se-pre-con").hide();
        });
    };

    self.renderConversation = function () {
        const container = $("#serviceRequestCardsContainer");
        if (!container) return;

        container.empty(); // Clear existing content

        const conversationHTML = self.generateConversationTimeline();
        container.append(conversationHTML);
        $(".se-pre-con").hide();

        // Attach event handlers after rendering
        self.attachEventHandlers();
    };

    self.generateConversationTimeline = function () {
        const serviceRequest = self.ServiceRequestDetails.serviceRequest;
        const replies = self.ServiceRequestDetails.serviceRequestReplies || [];

        // Sort replies by creation date
        const sortedReplies = replies.sort((a, b) => new Date(a.CreatedOn) - new Date(b.CreatedOn));

        let conversationHTML = `
            <div class="card" style="width:100%;">
                <div class="card-header bg-primary text-white">
                    <h5 class="mb-0">💬 Service Request Conversation #${serviceRequest.TicketId}</h5>
                </div>
                <div class="card-body p-0">
                    <div class="conversation-timeline">
        `;

        // Add main request as first item
        conversationHTML += self.generateMainRequestItem(serviceRequest);

        // Add all replies
        sortedReplies.forEach((reply, index) => {
            conversationHTML += self.generateReplyItem(reply, index);
        });

        // Add reply form at the end
        conversationHTML += self.generateReplyFormItem();

        conversationHTML += `
                    </div>
                </div>
            </div>
        `;

        return conversationHTML;
    };

    self.generateMainRequestItem = function (serviceRequest) {
        return `
            <div class="conversation-item">
                <div class="timeline-marker bg-primary"></div>
                <div class="conversation-content">
                    <div class="card mb-3">
                        <div class="card-header d-flex justify-content-between align-items-center bg-light">
                            <div>
                                <strong class="text-primary">🎫 Original Request</strong>
                                <span class="badge ${self.getStatusBadgeClass(serviceRequest.StatusId)} ms-2">
                                    ${serviceRequest.StatusId || 'Open'}
                                </span>
                            </div>
                            <small class="text-muted">${self.formatDate(serviceRequest.CreatedOn)}</small>
                        </div>
                        <div class="card-body">
                            <div class="row mb-3">
                                <div class="col-md-6">
                                    <table class="table table-sm table-borderless">
                                        <tr>
                                            <td style="width: 120px; color: #6c757d; font-weight: bold;">Subject:</td>
                                            <td>${serviceRequest.Subject || '--'}</td>
                                        </tr>
                                        <tr>
                                            <td style="color: #6c757d; font-weight: bold;">Category:</td>
                                            <td>${serviceRequest.Category || '--'}</td>
                                        </tr>
                                        <tr>
                                            <td style="color: #6c757d; font-weight: bold;">Priority:</td>
                                            <td>${serviceRequest.Priority || '--'}</td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-md-6">
                                    <table class="table table-sm table-borderless">
                                        <tr>
                                            <td style="width: 120px; color: #6c757d; font-weight: bold;">Order ID:</td>
                                            <td>${serviceRequest.OrderId || '--'}</td>
                                        </tr>
                                        <tr>
                                            <td style="color: #6c757d; font-weight: bold;">Classification:</td>
                                            <td>${serviceRequest.Classification || '--'}</td>
                                        </tr>
                                        <tr>
                                            <td style="color: #6c757d; font-weight: bold;">Assigned To:</td>
                                            <td>${serviceRequest.AssignTo || '--'}</td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="message-content">
                                <strong style="color: #6c757d;">Message:</strong>
                                <div class="alert alert-primary mt-2">
                                    ${serviceRequest.Message || 'No message provided.'}
                                </div>
                            </div>
                            <div class="conversation-actions mt-3">
                                <button class="btn btn-sm btn-outline-primary reply-to-item" 
                                        data-item-type="request"
                                        data-item-id="${serviceRequest.TicketId}">
                                    💬 Reply to Request
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        `;
    };

    self.generateReplyItem = function (reply, index) {
        const isInternal = reply.IsInternal;
        const internalBadge = isInternal ? '<span class="badge bg-warning ms-2">Internal Note</span>' : '';
        const bgClass = isInternal ? 'bg-warning-subtle' : 'bg-light';

        return `
            <div class="conversation-item">
                <div class="timeline-marker ${isInternal ? 'bg-warning' : 'bg-success'}"></div>
                <div class="conversation-content">
                    <div class="card mb-3 ${bgClass}" style="width:100%;">
                        <div class="card-header d-flex justify-content-between align-items-center">
                            <div>
                                <strong class="${isInternal ? 'text-warning' : 'text-success'}">
                                    ${isInternal ? '🔒 Internal Reply' : '💬 Public Reply'}
                                </strong>
                                ${internalBadge}
                                <span class="badge ${self.getStatusBadgeClass(reply.Status)} ms-2">
                                    ${reply.Status || 'Pending'}
                                </span>
                            </div>
                            <div>
                                <small class="text-muted">${self.formatDate(reply.CreatedOn)}</small>
                                <span class="text-muted mx-2">•</span>
                                <small class="text-muted">By User #${reply.ReplierId}</small>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="message-content mb-3">
                                <div class="alert ${isInternal ? 'alert-warning' : 'alert-success'}">
                                    ${reply.Message || 'No message content.'}
                                </div>
                            </div>
                            <div class="conversation-actions">
                                <button class="btn btn-sm btn-outline-primary reply-to-item" 
                                        data-item-type="reply"
                                        data-item-id="${reply.Id}"
                                        data-replier-id="${reply.ReplierId}">
                                    💬 Reply
                                </button>
                                ${isInternal ? `
                                <button class="btn btn-sm btn-outline-warning ms-2 toggle-visibility" 
                                        data-reply-id="${reply.Id}">
                                    👥 Make Public
                                </button>
                                ` : `
                                <button class="btn btn-sm btn-outline-secondary ms-2 toggle-visibility" 
                                        data-reply-id="${reply.Id}">
                                    🔒 Make Internal
                                </button>
                                `}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        `;
    };

    self.generateReplyFormItem = function () {
        return `
            <div class="conversation-item">
                <div class="timeline-marker bg-info"></div>
                <div class="conversation-content">
                    <div class="card mb-3 bg-info-subtle">
                        <div class="card-header bg-info text-white">
                            <h6 class="mb-0">✍️ Add New Reply</h6>
                        </div>
                        <div class="card-body">
                            <form id="replyForm">
                                <div class="mb-3">
                                    <label for="replyMessage" class="form-label">Your Message</label>
                                    <textarea class="form-control" id="replyMessage" rows="4" 
                                              placeholder="Type your reply here..." required></textarea>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="mb-3">
                                            <label for="replyStatus" class="form-label">Update Status</label>
                                            <select class="form-select" id="replyStatus">
                                                <option value="">Keep Current Status</option>
                                                <option value="Pending">Pending</option>
                                                <option value="In Progress">In Progress</option>
                                                <option value="Resolved">Resolved</option>
                                                <option value="Closed">Closed</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="mb-3">
                                            <label class="form-label">Visibility</label>
                                            <div class="form-check">
                                                <input class="form-check-input" type="radio" name="visibility" id="visibilityPublic" value="false" checked>
                                                <label class="form-check-label" for="visibilityPublic">
                                                    Public (Customer visible)
                                                </label>
                                            </div>
                                            <div class="form-check">
                                                <input class="form-check-input" type="radio" name="visibility" id="visibilityInternal" value="true">
                                                <label class="form-check-label" for="visibilityInternal">
                                                    Internal Note
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="d-flex justify-content-between align-items-center">
                                    <div>
                                        <button type="button" class="btn btn-secondary btn-sm" onclick="self.clearReplyForm()">
                                            Clear Form
                                        </button>
                                    </div>
                                    <div>
                                        <button type="submit" class="btn btn-success">
                                            📤 Submit Reply
                                        </button>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        `;
    };

    self.getStatusBadgeClass = function (status) {
        switch (status) {
            case 'Resolved': return 'bg-success';
            case 'Closed': return 'bg-secondary';
            case 'In Progress': return 'bg-warning';
            case 'Pending': return 'bg-info';
            default: return 'bg-primary';
        }
    };

    self.attachEventHandlers = function () {
        // Handle main reply form submission
        $('#replyForm').on('submit', function (e) {
            e.preventDefault();
            self.submitReply();
        });

        // Handle reply-to-item buttons (both request and replies)
        $('.reply-to-item').on('click', function () {
            const itemType = $(this).data('item-type');
            const itemId = $(this).data('item-id');
            const replierId = $(this).data('replier-id');
            self.quoteItem(itemType, itemId, replierId);
        });

        // Handle toggle visibility buttons
        $('.toggle-visibility').on('click', function () {
            const replyId = $(this).data('reply-id');
            self.toggleReplyVisibility(replyId);
        });
    };

    self.quoteItem = function (itemType, itemId, replierId) {
        let message = '';

        if (itemType === 'request') {
            const serviceRequest = self.ServiceRequestDetails.serviceRequest;
            message = `> **Original Request:**\n> ${serviceRequest.Message.replace(/\n/g, '\n> ')}\n\n`;
        } else if (itemType === 'reply') {
            const reply = self.ServiceRequestDetails.serviceRequestReplies.find(r => r.Id === itemId);
            if (reply) {
                const prefix = reply.IsInternal ? 'Internal Note' : 'Reply';
                message = `> **${prefix} from User #${replierId}:**\n> ${reply.Message.replace(/\n/g, '\n> ')}\n\n`;
            }
        }

        $('#replyMessage').val(message + $('#replyMessage').val());
        $('#replyMessage').focus();

        // Scroll to reply form
        $('html, body').animate({
            scrollTop: $('#replyForm').offset().top - 100
        }, 500);
    };

    self.submitReply = function () {
        const message = $('#replyMessage').val();
        const status = $('#replyStatus').val();
        const isInternal = $('input[name="visibility"]:checked').val() === 'true';

        if (!message.trim()) {
            alert('Please enter a message');
            return;
        }

        $(".se-pre-con").show();

        const replyData = {
            TicketId: self.TicketId,
            ReplierId: self.ApplicationUser.Id,
            Message: message,
            Status: status,
            IsInternal: isInternal,
        };

        var serviceRequestReply = addCommonProperties(replyData);
        $.ajax({
            url: '/ServiceRequest/InsertServiceRequestReply',
            method: 'POST',
            data: JSON.stringify(serviceRequestReply),
            contentType: 'application/json',
            timeout: 30000
        }).done(function (response) {
            console.log('Reply submitted successfully:', response);
            self.refreshData();
        }).fail(function (error) {
            console.error('Failed to submit reply:', error);
            $(".se-pre-con").hide();
            alert('Failed to submit reply. Please try again.');
        });
    };

    self.toggleReplyVisibility = function (replyId) {
        if (confirm('Are you sure you want to change the visibility of this reply?')) {
            $(".se-pre-con").show();

            $.ajax({
                url: '/ServiceRequest/ToggleReplyVisibility',
                method: 'POST',
                data: JSON.stringify({ replyId: replyId }),
                contentType: 'application/json'
            }).done(function (response) {
                console.log('Reply visibility updated:', response);
                self.refreshData();
            }).fail(function (error) {
                console.error('Failed to update reply visibility:', error);
                $(".se-pre-con").hide();
            });
        }
    };

    self.clearReplyForm = function () {
        $('#replyForm')[0].reset();
        $('#replyMessage').val('');
    };

    self.refreshData = function () {
        $.ajax({
            url: '/ServiceRequest/GetServiceRequestWithReplies',
            method: 'GET',
            data: { ticketId: self.TicketId },
            timeout: 30000
        }).done(function (response) {
            self.ServiceRequestDetails = response.data;
            self.renderConversation();
        }).fail(function (error) {
            console.error('Failed to refresh data:', error);
            $(".se-pre-con").hide();
        });
    };

    self.formatDate = function (dateString) {
        if (!dateString) return '--';
        const date = new Date(dateString);
        return date.toLocaleString();
    };

    self.showErrorState = function (message) {
        const container = $("#serviceRequestCardsContainer");
        if (container) {
            container.html(`
                <div class="alert alert-danger text-center">
                    <h4>❌ Error</h4>
                    <p>${message}</p>
                    <a href="/" class="btn btn-primary">Go Home</a>
                </div>
            `);
        }
        $(".se-pre-con").hide();
    };
}
const conversationStyles = `
<style>
.conversation-timeline {
    position: relative;
    padding: 20px 0;
}
.conversation-item {
    display: flex;
    margin-bottom: 0;
    position: relative;
}
.timeline-marker {
    width: 16px;
    height: 16px;
    border-radius: 50%;
    margin-right: 20px;
    margin-top: 20px;
    flex-shrink: 0;
    position: relative;
    z-index: 2;
}
.conversation-content {
    flex: 1;
    min-width: 0;
}
.conversation-item:not(:last-child) .conversation-content::before {
    content: '';
    position: absolute;
    left: -28px;
    top: 0;
    bottom: -20px;
    width: 2px;
    background: #dee2e6;
    z-index: 1;
}
.conversation-actions {
    border-top: 1px solid #e9ecef;
    padding-top: 15px;
    margin-top: 15px;
}
.message-content pre {
    white-space: pre-wrap;
    word-wrap: break-word;
    margin: 0;
}
</style>
`;