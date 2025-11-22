namespace OLC.Web.Email.Service.Templates
{
    public class ServiceRequestTemplate
    {
        public static string ComposeEmailAsync(string username, string ticketId, string issueTitle, string issueDescription, string createdDate, string supportEmail)
        {
            return $@"
<div style='
    font-family: Arial, Helvetica, sans-serif;
    background: linear-gradient(135deg, #5A73E8, #4FC3F7);
    padding: 30px;
    border-radius: 20px;
    color: white;
'>
    <div style='
        background: rgba(0, 0, 0, 0.35);
        padding: 30px;
        border-radius: 15px;
        border: 1px solid rgba(255,255,255,0.2);
        box-shadow: 0px 6px 14px rgba(0,0,0,0.45);
    '>

        <h2 style='
            font-size: 28px;
            margin-top: 0;
            text-align:center;
            text-shadow: 1px 1px 5px #000;
            padding-bottom: 10px;
            border-bottom: 1px solid rgba(255,255,255,0.25);
        '>
            Service Request Created ✔️
        </h2>

        <p style='font-size: 18px; margin-top: 20px; line-height: 1.6;'>
            Hello <strong style='color:#ffe27a;'>{username}</strong>,
        </p>

        <p style='font-size: 17px; line-height: 1.6;'>
            Your service request has been successfully created in our system.  
            Our support team will review your issue and get back to you soon.
        </p>

        <div style='
            background: rgba(255,255,255,0.15);
            padding: 20px;
            border-radius: 12px;
            border: 1px solid rgba(255,255,255,0.25);
            margin-top: 25px;
            margin-bottom: 25px;
            box-shadow: inset 0 0 10px rgba(255,255,255,0.2);
        '>

            <p style='font-size: 16px; margin:6px 0;'>
                <strong style='color:#fff;'>Ticket ID:</strong>
                <span style='color:#ffe27a;'>{ticketId}</span>
            </p>

            <p style='font-size: 16px; margin:6px 0;'>
                <strong style='color:#fff;'>Issue Title:</strong> 
                <span style='color:#ffe27a;'>{issueTitle}</span>
            </p>

            <p style='font-size: 16px; margin:6px 0;'>
                <strong style='color:#fff;'>Description:</strong> 
                <span style='color:#ffe27a;'>{issueDescription}</span>
            </p>

            <p style='font-size: 16px; margin:6px 0;'>
                <strong style='color:#fff;'>Created On:</strong> 
                <span style='color:#ffe27a;'>{createdDate}</span>
            </p>

        </div>

        <p style='
            font-size: 16px;
            line-height: 1.6;
        '>
            Our team is actively working to ensure your request is handled quickly.  
            If you need further assistance, feel free to reach out to us anytime.
        </p>

        <p style='font-size: 16px; margin-top: 20px;'>
            Contact Support: 
            <a href='mailto:{supportEmail}' style='color:#ffe27a;'>{supportEmail}</a>
        </p>

        <p style='
            text-align:center;
            margin-top:30px;
            font-size: 14px;
            opacity:0.85;
            border-top: 1px solid rgba(255,255,255,0.25);
            padding-top: 10px;
        '>
            © 2025 Your Company — All Rights Reserved
        </p>

    </div>
</div>";
        }
    }
}
