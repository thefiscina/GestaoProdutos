using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoProduto.Dominio
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory?.CreateLogger<ExceptionMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (GestaoProdutoException ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                //context.Response.Clear();
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.StatusCode = (int)ex.StatusCode;
                context.Response.ContentType = ex.ContentType;
                await context.Response.WriteAsync(ex.SerializeObject());
            }

            catch (UnauthorizedAccessException ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }
                //context.Response.Clear();
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.StatusCode = 401;
                context.Response.ContentType = @"text/plain";
                await context.Response.WriteAsync(ex.Message);
            }

            catch (ArgumentNullException ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                List<GestaoProdutoError> erros = new List<GestaoProdutoError>();
                try { erros.Add(new GestaoProdutoError { Codigo = "0", Propriedade = "Message", Messagem = ex.Message }); } catch { }
                try { erros.Add(new GestaoProdutoError { Codigo = "1", Propriedade = "ParamName", Messagem = ex.ParamName }); } catch { }

                GestaoProdutoException GestaoProdutoException = new GestaoProdutoException(ExceptionEnum.BadRequest, "Argumento nulo ou inválido", ex, erros);
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.StatusCode = (int)GestaoProdutoException.StatusCode;
                context.Response.ContentType = GestaoProdutoException.ContentType;
                await context.Response.WriteAsync(GestaoProdutoException.SerializeObject());
                return;
            }

            catch (DbUpdateException ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                List<GestaoProdutoError> erros = new List<GestaoProdutoError>();
                try { erros.Add(new GestaoProdutoError { Codigo = ((Npgsql.PostgresException)ex.InnerException).Code, Propriedade = ((Npgsql.PostgresException)ex.InnerException).ColumnName, Messagem = ((Npgsql.PostgresException)ex.InnerException).MessageText }); } catch { }

                GestaoProdutoException GestaoProdutoException = new GestaoProdutoException(ExceptionEnum.BadRequest, "Ocorreu erro inesperado", erros);                
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.StatusCode = (int)GestaoProdutoException.StatusCode;
                context.Response.ContentType = GestaoProdutoException.ContentType;
                await context.Response.WriteAsync(GestaoProdutoException.SerializeObject());
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                List<GestaoProdutoError> erros = new List<GestaoProdutoError>();
                try { erros.Add(new GestaoProdutoError { Codigo = "0", Propriedade = "Message", Messagem = ex.Message }); } catch { }

                GestaoProdutoException GestaoProdutoException = new GestaoProdutoException(ExceptionEnum.InternalServerError, "Ocorreu erro inesperado no servidor", ex, erros);
                //context.Response.Clear();
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.StatusCode = (int)GestaoProdutoException.StatusCode;
                context.Response.ContentType = GestaoProdutoException.ContentType;
                await context.Response.WriteAsync(GestaoProdutoException.SerializeObject());
                return;
            }
        }
    }
}