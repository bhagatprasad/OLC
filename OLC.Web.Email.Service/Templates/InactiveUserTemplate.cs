namespace OLC.Web.Email.Service.Templates
{
    public class InactiveUserTemplate
    {
        public static string ComposeEmailAsync(string username, string lastActiveDate, string reactivateUrl, string supportEmail)
        {
            return $@"
<div style='
    font-family: Arial, Helvetica, sans-serif;
    background: linear-gradient(135deg, #546E7A, #455A64);
    padding: 30px;
    border-radius: 20px;
    color: white;
'>
    <div style='
        background: rgba(0,0,0,0.35);
        padding: 30px;
        border-radius: 15px;
        border: 1px solid rgba(255,255,255,0.2);
        box-shadow: 0px 6px 14px rgba(0,0,0,0.45);
    '>

        <h2 style='
            font-size: 28px;
            margin-top: 0;
            text-align: center;
            text-shadow: 1px 1px 4px #000;
            padding-bottom: 8px;
            border-bottom: 1px solid rgba(255,255,255,0.25);
        '>
            Account Inactivity Notice
        </h2>

        <p style='
            font-size: 18px;
            margin-top: 20px;
            line-height: 1.6;
        '>
            Hi <strong style='color:#ffe27a;'>{username}</strong>,
        </p>

        <p style='font-size: 17px; line-height: 1.6;'>
            We noticed that you haven’t used your account since 
            <strong style='color:#ffe27a;'>{lastActiveDate}</strong>.  
            To ensure your account stays active and secure, we kindly request you to log in again.
        </p>

        <div style='
            text-align:center;
            margin-top: 25px;
        '>
            <a href='{reactivateUrl}' style='
                background:#ffe27a;
                color:#000;
                padding: 12px 22px;
                border-radius: 8px;
                text-decoration: none;
                font-weight: bold;
            '>
                Reactivate Account
            </a>
        </div>

        <p style='font-size: 16px; margin-top: 25px; line-height: 1.6;'>
            If you believe this message was sent in error or need help with your account,  
            feel free to reach out to our support team at  
            <a href='mailto:{supportEmail}' style='color:#ffe27a;'>{supportEmail}</a>.
        </p>

        <p style='
            text-align:center;
            margin-top: 25px;
            font-size: 14px;
            opacity: 0.85;
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
