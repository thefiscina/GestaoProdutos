using GestaoProduto.Dominio;
using GestaoProduto.Service.Model.Query;
using System;
using System.Collections.Generic;

namespace GestaoProduto.Service.Model
{
    public class ProductResponse
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

    public class ProductResponsePaginacao
    {
        public List<ProductResponse> dados { get; set; }
        public Paginacao paginacao { get; set; }
    }

}

