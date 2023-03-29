using GestaoProduto.Dominio.Service;
using GestaoProduto.Service.Model;
using GestaoProduto.Service.Model.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _service;    
        public ProductController(ILogger<ProductController> logger, IProductService service)
        {
            _logger = logger;
            _service = service;
          
        }

        [HttpGet]
        [SwaggerOperation(Description = "Filtrando a partir de parâmetros e paginando a resposta")]
        public ProductResponsePaginacao Listar([FromQuery] ProductQuery query)
        {
            ProductResponsePaginacao retorno = new ProductResponsePaginacao();
            Paginacao paginacao;
            retorno.dados = _service.Listar(query, out paginacao);
            retorno.paginacao = paginacao;
            return retorno;
        }


        [HttpGet("{codigo}")]
        [SwaggerOperation(Description = "Recuperar um registro por código")]
        public ProductResponse ObterPorCodigo(int codigo)
        {
            return _service.ObterPorCodigo(codigo);
        }

        [HttpPost]
        [SwaggerOperation(Description = "Inserir um produto")]
        public ProductResponse Salvar([FromBody] ProductRequest bodyRequest)
        {
            return _service.Salvar(bodyRequest);
        }


        [HttpPut("{codigo}")]
        [SwaggerOperation(Description = "Alterar um produto")]
        public ProductResponse Alterar(int codigo, [FromBody] ProductRequest bodyRequest)
        {
            return _service.Alterar(codigo, bodyRequest);
        }

        [HttpPatch("{codigo}")]
        [SwaggerOperation(Description = "Alterar status do produto")]
        public ProductResponse Pacth(int codigo, [FromBody] PacthRequest bodyRequest)
        {
            return _service.Excluir(codigo, bodyRequest);
        }
    }
}