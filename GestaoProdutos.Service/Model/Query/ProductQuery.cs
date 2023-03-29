namespace GestaoProduto.Service.Model
{
    public class ProductQuery 
    {
        public bool situacao_produto { get; set; } = true;
        public int codigo_produto { get; set; }
        public int codigo_fornecedor { get; set; }
        public int? pagina { get; set; }
        public int? quantidade { get; set; }

    }
}
