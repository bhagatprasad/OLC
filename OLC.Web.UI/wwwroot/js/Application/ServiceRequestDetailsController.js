function ServiceRequestDetailsController() {
    var self = this;
    self.TicketId = null;
    var actions = [];
    var dataObjects = [];

    self.ServiceRequestDetails = {};

    actions.push("/ServiceRequest/GetServiceRequestByTicketId");


    self.init = function () {
        $(".se-pre-con").show();

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

            self.ServiceRequestDetails = responses[0].data;
            self.renderCards();

        }).fail(function (error) {
            console.error('One or more requests failed:', error);
            $(".se-pre-con").hide();
        });


        self.renderCards = function () {
            const container = $("#serviceRequestCardsContainer");
            if (!container) return;

            const cardsHTML = [
                self.generateServiceRequestInfoCard(),
            ].join('');

            container.append(cardsHTML);
            $(".se-pre-con").hide();
        }

        self.generateServiceRequestInfoCard = function () {
            return `
            <!DOCTYPE html>
                <html>
                    <head>
                        <meta charset="UTF-8">
                            <title>Service Request Notification</title>
                    </head>

                    <body style="margin:0; padding:0; background:#f5f6fa; font-family: Arial, sans-serif;">

                        <table width="100%" cellpadding="0" cellspacing="0" style="background:#f5f6fa; padding: 20px 0;">
                            <tr>
                                <td align="center">
                                   <table width="600" cellpadding="0" cellspacing="0" style="background:#ffffff; border-radius:6px; overflow:hidden; box-shadow:0 2px 10px rgba(0,0,0,0.08);">
                                    <tr>
                                        <td style="background:#0d6efd; color:#ffffff; padding:20px; text-align:center; font-size:24px; font-weight:bold;">
                                            Service Request Update
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 20px 30px;">
                                            <p style="font-size:16px; color:#333; margin:0 0 20px;">
                                                Hello,
                                                    <br><br>
                                                Below are the latest details regarding your service request.
                                            </p>

                                    <table width="100%" cellpadding="0" cellspacing="0" style="border:1px solid #ddd; border-radius:6px;">
                                        <tr>
                                            <td style="background:#0d6efd; color:#ffffff; padding:12px 16px; font-size:18px; font-weight:bold;">
                                                🎫 Service Request Details
                                                <span style="float:right; background:#ffffff; color:#000; padding:4px 8px; border-radius:4px; font-size:12px;">
                                                    ${self.ServiceRequestDetails.StatusId}
                                                </span>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="padding:0;">
                                    <table width="100%" cellpadding="8" cellspacing="0" style="border-collapse:collapse;">
                                        <tr>
                                            <td style="color:#6c757d; font-weight:bold; width:200px;">Subject</td>
                                            <td>${self.ServiceRequestDetails.Subject ?? '--'}</td>
                                        </tr>
                                        <tr>
                                            <td style="color:#6c757d; font-weight:bold;">Message</td>
                                            <td>${self.ServiceRequestDetails.Message ?? '--'}</td>
                                        </tr>
                                        <tr>
                                            <td style="color:#6c757d; font-weight:bold;">Category</td>
                                            <td>${self.ServiceRequestDetails.Category ?? '--'}</td>
                                        </tr>
                                        <tr>
                                            <td style="color:#6c757d; font-weight:bold;">Priority</td>
                                            <td>${self.ServiceRequestDetails.Priority ?? '--'}</td>
                                        </tr>
                                        <tr>
                                            <td style="color:#6c757d; font-weight:bold;">Order ID</td>
                                            <td>${self.ServiceRequestDetails.OrderId ?? '--'}</td>
                                        </tr>
                                        <tr>
                                            <td style="color:#6c757d; font-weight:bold;">Classification</td>
                                            <td>${self.ServiceRequestDetails.Classification ?? '--'}</td>
                                        </tr>
                                        <tr>
                                            <td style="color:#6c757d; font-weight:bold;">Assigned To</td>
                                            <td>${self.ServiceRequestDetails.AssignTo ?? '--'}</td>
                                        </tr>
                                        <tr>
                                            <td style="color:#6c757d; font-weight:bold;">Created By</td>
                                            <td>${self.ServiceRequestDetails.CreatedBy ?? '--'}</td>
                                        </tr>
                                        <tr>
                                            <td style="color:#6c757d; font-weight:bold;">Last Updated</td>
                                            <td>${self.ServiceRequestDetails.ModifiedOn ?? '--'}</td>
                                        </tr>
                                    </table>
                                  </td>
                                </tr>
                            </table>

                                <h3 style="margin-top:30px; color:#333;">Latest Reply</h3>

                                <div style="background:#f1f1f1; padding:15px; border-radius:6px; font-size:14px; color:#333;">
                                     ${self.ServiceRequestDetails.ReplyMessage ?? 'No replies yet.'}
                                </div>
 
                                <div style="margin:30px 0; text-align:center;">
                                    <a href="${self.DetailsPageUrl}" 
                                        style="background:#0d6efd; padding:12px 20px; color:#fff; font-size:16px; border-radius:4px; text-decoration:none;">
                                        View Full Request
                                    </a>
                                </div>

                                </td>
                            </tr>
                        <tr>
                            <td style="background:#f0f0f0; padding:15px; text-align:center; color:#6c757d; font-size:12px;">
                                This is an automated message. Please do not reply directly to this email.<br>
                                © ${new Date().getFullYear()} Your Company Name. All rights reserved.
                            </td>
                        </tr>
                    </table>
                  </td>
                </tr>
               </table>
            </body>
            </html>
            }
            `;
        }
    }
}