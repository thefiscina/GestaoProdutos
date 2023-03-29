using GestaoProduto.Dominio;
using System;

namespace GestaoProduto.Service.Model
{
    public class ProductRequest
    {
        public int codigo_produto { get; set; }
        public string descricao_produto { get; set; }
        public Boolean situacao_produto { get; set; }
        public DateTime data_fabricacao { get; set; }
        public DateTime data_validade { get; set; }
        public int codigo_fornecedor { get; set; }
        public string descricao_fornecedor { get; set; }
        public string cnpj_fornecedor { get; set; }

    }
}
