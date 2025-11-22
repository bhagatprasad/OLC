namespace OLC.Web.Email.Service.Templates
{
    public class KycSuccessTemplate
    {
        public static string ComposeEmailAsync(string username, string kycId, string verificationDate, string remarks)
        {
            return $@"
<div style='
    font-family: Arial, Helvetica, sans-serif;
    background: linear-gradient(135deg, #0f4c75, #3282b8);
    padding: 30px;
    border-radius: 20px;
    color: white;
'>
    <div style='
        background: rgba(255, 255, 255, 0.08);
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
            KYC Verification Successful ✔️
        </h2>

        <p style='
            font-size: 18px;
            margin-top: 20px;
            line-height: 1.6;
        '>
            Hello <strong style='color:#ffd369;'>{username}</strong>,
        </p>

        <p style='font-size: 17px; line-height: 1.6;'>
            We are pleased to inform you that your KYC verification has been 
            <strong style='color:#ffd369;'>successfully completed</strong>.
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
                <strong style='color:#fff;'>KYC Reference ID:</strong>
                <span style='color:#ffd369;'>{kycId}</span>
            </p>

            <p style='font-size: 16px; margin:6px 0;'>
                <strong style='color:#fff;'>Verified On:</strong>
                <span style='color:#ffd369;'>{verificationDate}</span>
            </p>

            <p style='font-size: 16px; margin:6px 0;'>
                <strong style='color:#fff;'>Remarks:</strong>
                <span style='color:#ffd369;'>{remarks}</span>
            </p>

        </div>

        <p style='
            font-size: 16px;
            line-height: 1.6;
        '>
            You can now enjoy uninterrupted access to all features of our platform.
            If you need any assistance, feel free to reach out to our support team.
        </p>

        <p style='
            font-size: 16px;
            margin-top: 15px;
        '>
            Thank you for completing your verification with us.
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
