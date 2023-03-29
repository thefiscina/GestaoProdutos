using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GestaoProduto.Dominio
{
    public interface IProductDomain
    {
        List<Product> Listar(Expression<Func<Product, bool>> filter = null, Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null, int? skip = null, int? take = null);
        Product Obter(int id);
        Product ObterPorCodigo(int codigo);
        Product Salvar(Product product);
        Product Alterar(Product product, int codigo);
        Product Inativar(int codigo , bool ativo);

        int Quantidade(Expression<Func<Product, bool>> filter = null);
    }

    public class ProductDomain : IProductDomain
    {
        IRepository<Product> _baseRepository;
        public ProductDomain(DbContext context)
        {
            _baseRepository = new Repository<Product>(context);
        }

        public Product Salvar(Product product)
        {
            if (product.CodigoProduto  == 0)
            {
                throw new Exception("Codigo obrigatorio");
            }

            if (String.IsNullOrWhiteSpace(product.DescricaoProduto))
            {
                throw new Exception("Descrição obrigatorio");
            }

            if (product.DataFabricacao > product.DataValidade || product.DataFabricacao == product.DataValidade)
            {
                throw new Exception("Data de fabricação que não pode ser maior ou igual a data de validade");
            }

            Product _product = ObterPorCodigo(product.CodigoProduto);

            if (_product != null)
            {
                throw new Exception("Codigo existente");
            }

            _baseRepository.Save(product);
            _baseRepository.SaveChanges();
            return product;
        }

        public Product Alterar(Product product, int codigo)
        {
            if (product.CodigoProduto == 0)
            {
                throw new Exception("Codigo obrigatorio");
            }

            if (String.IsNullOrWhiteSpace(product.DescricaoProduto))
            {
                throw new Exception("Descrição obrigatorio");
            }

            if (product.DataFabricacao > product.DataValidade || product.DataFabricacao == product.DataValidade)
            {
                throw new Exception("Data de fabricação que não pode ser maior ou igual a data de validade");
            }

            Product _product = ObterPorCodigo(codigo);
            if (_product == null)
            {
                throw new System.Exception("Produto não encontrado");
            }

            _product.CodigoProduto = product.CodigoProduto;
            _product.SituacaoProduto = product.SituacaoProduto;
            _product.DescricaoProduto = product.DescricaoProduto;
            _product.CodigoFornecedor = product.CodigoFornecedor;
            _product.DescricaoFornecedor = product.DescricaoFornecedor;
            _product.DataFabricacao = product.DataFabricacao;
            _product.DataValidade = product.DataValidade;
            _product.cnpjFornecedor = product.cnpjFornecedor;

            _baseRepository.Update(_product);
            _baseRepository.SaveChanges();
            return _product;
        }

        public Product ObterPorCodigo(int codigo)
        {
            return _baseRepository.Consultar(PredicateBuilder.New<Product>().And(a => a.CodigoProduto == codigo)).FirstOrDefault();
        }

        public Product Obter(int id)
        {
            return _baseRepository.GetById(id);
        }

        public List<Product> Listar(Expression<Func<Product, bool>> filter = null, Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null, int? skip = null, int? take = null)
        {
            return _baseRepository.Listar(filter, orderBy, skip, take);
        }

        public int Quantidade(Expression<Func<Product, bool>> filter = null)
        {
            return _baseRepository.Contar(filter);
        }

        public Product Inativar(int codigo, bool ativo)
        {
            Product _product = ObterPorCodigo(codigo);
            if (_product == null)
            {
                throw new System.Exception("Produto não encontrado");
            }

            _product.SituacaoProduto = ativo;
            _baseRepository.Update(_product);
            _baseRepository.SaveChanges();
            return _product;
        }
    }
}