namespace OLC.Web.Email.Service.Templates
{
    public static class PlacePaymentOrderTemaplate
    {
        public static string ComposeEmailAsync(string orderReferance, string orderStatus, string paymentStatus, string depositeStatus)
        {
            return $"<h1> Hello your order changed its status</h1> </br> The current state of the order is {orderStatus}";
        }
    }
}
