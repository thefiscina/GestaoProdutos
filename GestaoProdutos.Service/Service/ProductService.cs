using AutoMapper;
using GestaoProduto.Service.Map;
using GestaoProduto.Service.Model;
using GestaoProduto.Service.Model.Query;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestaoProduto.Dominio.Service
{
    public interface IProductService
    {


        //Recuperar um registro por código;
        ProductResponse ObterPorCodigo(int codigo_produto);

        //Listar registros
        //Filtrando a partir de parâmetros e paginando a resposta
        List<ProductResponse> Listar(ProductQuery query, out Paginacao paginacao);

        //Inserir
        //Criar validação para o campo data de fabricação que não poderá ser maior ou igual a data de validade.
        ProductResponse Salvar(ProductRequest product);


        //Editar
        //Criar validação para o campo data de fabricação que não poderá ser maior ou igual a data de validade.
        ProductResponse Alterar(int codigo, ProductRequest product);

        //Excluir
        //A exclusão deverá ser lógica, atualizando o campo situação com o valor Inativo.
        ProductResponse Excluir(int codigo, PacthRequest product);


    }

    public class ProductService : IProductService
    {
        IProductDomain _domain;
        IMapper _mapper;
        public ProductService(GestaoProdutoContext context)
        {
            _mapper = AutoMapping.mapper;
            _domain = new ProductDomain(context);
        }
        public ProductResponse Salvar(ProductRequest product)
        {
            var _entidadeDominio = _mapper.Map<Product>(product);
            _entidadeDominio = _domain.Salvar(_entidadeDominio);
            return _mapper.Map<ProductResponse>(_entidadeDominio);
        }

        public ProductResponse Alterar(int codigo, ProductRequest product)
        {
            var _request = _mapper.Map<Product>(product);

            _request = _domain.Alterar(_request, codigo);

            return _mapper.Map<ProductResponse>(_request);
        }

        public ProductResponse ObterPorCodigo(int codigo_produto)
        {
            ExpressionStarter<Product> filter = PredicateBuilder.New<Product>(a => true);

            if (codigo_produto == 0)
                throw new Exception("Codigo inexistente");

            filter.And(a => a.CodigoProduto == codigo_produto);
            return _mapper.Map<ProductResponse>(_domain.Listar(filter).FirstOrDefault());
        }

        public ProductResponse Obter(int id)
        {
            return _mapper.Map<ProductResponse>(_domain.Obter(id));
        }

        public List<ProductResponse> Listar(ProductQuery query, out Paginacao paginacao)
        {
            ExpressionStarter<Product> filter = PredicateBuilder.New<Product>(a => true);


            filter.And(a => a.SituacaoProduto == query.situacao_produto);

            if (query.codigo_produto > 0)
                filter.And(a => a.CodigoProduto == query.codigo_produto);

            if (query.codigo_fornecedor > 0)
                filter.And(a => a.CodigoFornecedor == query.codigo_fornecedor);

            int? skip = null;
            if (query.pagina != null && query.quantidade != null)
            {
                skip = (query.pagina - 1) * query.quantidade;
            }

            List<Product> _retorno = _domain.Listar(filter, n => n.OrderBy(x => x.CodigoProduto), skip, query.quantidade);

            if (query.pagina.HasValue && query.quantidade.HasValue)
            {
                int quantidade = _domain.Quantidade(filter);
                paginacao = new Paginacao();
                paginacao.pagina_atual = (int)query.pagina;
                paginacao.quantidade_pagina = (int)query.quantidade;
                paginacao.quantidade_total = quantidade;
                paginacao.pagina_total = (int)Math.Ceiling((double)quantidade / (int)query.quantidade);
            }
            else
            {
                paginacao = null;
            }

            return _mapper.Map<List<ProductResponse>>(_retorno);
        }

        public ProductResponse Excluir(int codigo, PacthRequest product)
        {
            var _request = _domain.Inativar(codigo, product.situacao_produto);
            return _mapper.Map<ProductResponse>(_request);
        }
    }
}