namespace Transaction.Api.Types
{
    public static class TransactionErrorCode
    {
        public const int AccountDoesNotExistError = 9001;
        public const int InsufficientBalance = 9002;
        public const int InvalidAmount = 9003;
        public const int InvalidCurrencyError = 9004;
        public const int CurrencyMismatchError = 9005;
    }
}