namespace OLC.Web.Email.Service.Templates
{
    public class KycRejectedTemplate
    {
        public static string ComposeEmailAsync(string username, string kycId, string rejectedReason, string supportEmail, string reapplyUrl)
        {
            return $@"
<div style='
    font-family: Arial, Helvetica, sans-serif;
    background: linear-gradient(135deg, #4A6CF7, #6A5ACD);
    padding: 30px;
    border-radius: 20px;
    color: white;
'>
    <div style='
        background: rgba(0, 0, 0, 0.35);
        padding: 30px;
        border-radius: 15px;
        border: 1px solid rgba(255,255,255,0.18);
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
            KYC Verification Failed ❌
        </h2>

        <p style='
            font-size: 18px;
            margin-top: 20px;
            line-height: 1.6;
        '>
            Hello <strong style='color:#ffe27a;'>{username}</strong>,
        </p>

        <p style='font-size: 17px; line-height: 1.6;'>
            We regret to inform you that your KYC verification 
            (<strong style='color:#ffe27a;'>{kycId}</strong>)  
            has been <strong style='color:#ffe27a;'>rejected</strong>.
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
                <strong style='color:#fff;'>Reason for Rejection:</strong>
                <span style='color:#ffe27a;'>{rejectedReason}</span>
            </p>

        </div>

        <p style='
            font-size: 16px;
            line-height: 1.6;
        '>
            Please review the issue and upload your corrected documents again.
            You may re-submit your KYC by clicking the button below.
        </p>

        <div style='text-align:center; margin-top: 20px;'>
            <a href='{reapplyUrl}' style='
                background:#ffe27a;
                color:#000;
                padding:12px 20px;
                border-radius:8px;
                text-decoration:none;
                font-weight:bold;
            '>
                Re-Submit KYC
            </a>
        </div>

        <p style='
            font-size: 16px;
            margin-top: 20px;
        '>
            If you need help or have questions, feel free to contact our support team at  
            <a href='mailto:{supportEmail}' style='color:#ffe27a;'>{supportEmail}</a>.
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

