namespace OLC.Web.UI.Helper
{
    public static class UitlityConverter
    {
        public static long ConvertToCents(decimal? amount)
        {
            // Multiply by 100 to convert to cents, round to nearest whole number
            long cents = (long)Math.Round(amount.Value * 100, MidpointRounding.AwayFromZero);

            // Optional: Check for overflow (if amount is extremely large)
            if (cents < long.MinValue || cents > long.MaxValue)
            {
                throw new OverflowException("Amount too large to convert to cents.");
            }

            return cents;
        }
    }
}
