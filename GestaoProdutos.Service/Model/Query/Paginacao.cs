using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProduto.Service.Model.Query
{
    public class Paginacao
    {
        public int pagina_atual { get; set; }
        public int pagina_total { get; set; }
        public int quantidade_pagina { get; set; }
        public int quantidade_total { get; set; }
    }
}
