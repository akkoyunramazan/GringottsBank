using System.ComponentModel;

namespace Transaction.Api.Types
{
    public enum Currency
    {
        Unknown = 0,

        [Description("United States dollar")]
        USD = 840,

        [Description("Pound")]
        GBP = 826,

        [Description("Euro")]
        EUR = 978,

        [Description("Turkish lira")]
        TRY = 949
    }
}