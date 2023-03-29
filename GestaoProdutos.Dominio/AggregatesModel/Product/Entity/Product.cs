using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoProduto.Dominio
{
    public class Product : Entity
    {
        [Column("codigo_produto")]
        public int CodigoProduto { get; set; }
        [Column("descricao_produto")]
        public string DescricaoProduto { get; set; }

        [Column("situacao_produto")]
        public Boolean SituacaoProduto { get; set; } = false;

        [Column("data_fabricacao")]
        public DateTime DataFabricacao { get; set; }

        [Column("data_validade")]
        public DateTime DataValidade { get; set; }

        [Column("codigo_fornecedor")]
        public int CodigoFornecedor { get; set; }

        [Column("descricao_fornecedor")]
        public string DescricaoFornecedor { get; set; }

        [Column("cnpj_fornecedor")]
        public string cnpjFornecedor { get; set; }

    }
}
