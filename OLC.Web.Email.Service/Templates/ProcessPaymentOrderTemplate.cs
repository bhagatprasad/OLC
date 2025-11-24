namespace OLC.Web.Email.Service.Templates
{
    public static class ProcessPaymentOrderTemplate
    {
        public static string ComposeEmailAsync(string orderReferance,string orderStatus,string paymentStatus,string depositeStatus,string username)
        {
            return $@"
        <div style='
            font-family: Arial, Helvetica, sans-serif;
            background: linear-gradient(135deg, #6a5acd, #00c9a7);
            padding: 30px;
            border-radius: 20px;
            color: white;
        '>
            <div style='
                background: rgba(0, 0, 0, 0.40);
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
                    padding-bottom: 8px;
                    border-bottom: 1px solid rgba(255,255,255,0.25);
                '>
                    Order Update
                </h2>

                <p style='
                    font-size: 18px;
                    margin-top: 20px;
                    line-height: 1.6;
                '>
                    Hi <strong style='color:#ffe066;'>{username}</strong>,
                </p>

                <p style='
                    font-size: 17px;
                    line-height: 1.6;
                '>
                    Your order <strong style='color:#ffe066;'>{orderReferance}</strong> has received an important update in our system.
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
                        <strong style='color:#fff;'>Order Status:</strong> 
                        <span style='color:#ffe066;'>{orderStatus}</span>
                    </p>

                    <p style='font-size: 16px; margin:6px 0;'>
                        <strong style='color:#fff;'>Payment Status:</strong> 
                        <span style='color:#ffe066;'>{paymentStatus}</span>
                    </p>

                    <p style='font-size: 16px; margin:6px 0;'>
                        <strong style='color:#fff;'>Deposit Status:</strong> 
                        <span style='color:#ffe066;'>{depositeStatus}</span>
                    </p>

                </div>

                <p style='
                    font-size: 16px;
                    line-height: 1.6;
                '>
                    Our system works continuously to ensure that your transactions are processed smoothly.  
                    If you have any questions or need support, our team is here to help you anytime.
                </p>

                <p style='
                    font-size: 16px;
                    margin-top: 15px;
                '>
                    Thank you for choosing our service.  
                    We truly appreciate your trust.
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
