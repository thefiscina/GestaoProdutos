using System.ComponentModel;

namespace GestaoProduto.Dominio
{
    public enum ProductType
    {
        [Description("COMUM")]
        COMUM = 0,
        [Description("PROFISSIONAL")]
        PROFISSIONAL = 1,
        [Description("ADMIN")]
        ADMIN = 2
    }
}
