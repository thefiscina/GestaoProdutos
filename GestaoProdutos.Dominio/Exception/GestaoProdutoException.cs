using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestaoProduto.Dominio
{
    public class GestaoProdutoException : Exception
    {
        public GestaoProdutoMenssage GestaoProdutoMenssage { get; set; }
        public ExceptionEnum StatusCode { get; set; } = ExceptionEnum.BadRequest;
        public string ContentType { get; set; } = @"text/json";

        public GestaoProdutoException() : base()
        {
        }

        public GestaoProdutoException(string message) : base(message)
        {
            this.GestaoProdutoMenssage = new GestaoProdutoMenssage { Codigo = this.StatusCode.ToString(), Messagem = message };
        }
        public GestaoProdutoException(ExceptionEnum status, string message) : base(message)
        {
            this.StatusCode = status;
            this.GestaoProdutoMenssage = new GestaoProdutoMenssage { Codigo = this.StatusCode.ToString(), Messagem = message };
        }

        public GestaoProdutoException(ExceptionEnum status, string message, List<GestaoProdutoError> erros) : base(message)
        {
            this.StatusCode = status;
            this.GestaoProdutoMenssage = new GestaoProdutoMenssage { Codigo = this.StatusCode.ToString(), Messagem = message, Erros = erros };
        }

        public GestaoProdutoException(ExceptionEnum status, List<GestaoProdutoError> erros)
        {
            this.StatusCode = status;
            if (erros.Count > 0)
                this.GestaoProdutoMenssage = new GestaoProdutoMenssage { Codigo = erros.FirstOrDefault().Codigo, Messagem = erros.FirstOrDefault().Messagem, Erros = erros };
        }

        public GestaoProdutoException(ExceptionEnum status, GestaoProdutoMenssage menssage)
        {
            this.StatusCode = status;
            this.GestaoProdutoMenssage = menssage;
        }

        public GestaoProdutoException(string message, Exception innerException) : base(message, innerException)
        {
            this.GestaoProdutoMenssage = new GestaoProdutoMenssage { Codigo = this.StatusCode.ToString(), Messagem = message, Reference = innerException };
        }

        public GestaoProdutoException(ExceptionEnum status, string message, Exception innerException) : base(message, innerException)
        {
            this.StatusCode = status;
            this.GestaoProdutoMenssage = new GestaoProdutoMenssage { Codigo = this.StatusCode.ToString(), Messagem = message, Reference = innerException };
        }

        public GestaoProdutoException(ExceptionEnum status, string message, Exception innerException, List<GestaoProdutoError> erros) : base(message, innerException)
        {
            this.StatusCode = status;
            this.GestaoProdutoMenssage = new GestaoProdutoMenssage { Codigo = this.StatusCode.ToString(), Messagem = message, Reference = innerException, Erros = erros };
        }

        internal static GestaoProdutoException DeserializeObject(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<GestaoProdutoException>(json);
            }
            catch
            {
                return new GestaoProdutoException();
            }
        }

        internal string SerializeObject()
        {
            try
            {
                return JsonConvert.SerializeObject(this.GestaoProdutoMenssage, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });

                //return JsonConvert.SerializeObject(this.GestaoProdutoError);
            }
            catch
            {
                return "";
            }
        }
    }

    public enum ExceptionEnum
    {
        NotFound = 404,
        BadRequest = 400,
        Unauthorized = 401,
        InternalServerError = 500,
    }

    public class GestaoProdutoMenssage
    {
        [JsonProperty("codigo", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Codigo { get; set; } = "";

        [JsonProperty("messagem", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Messagem { get; set; } = "";

        [JsonProperty("erros", DefaultValueHandling = DefaultValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Error)]
        public List<GestaoProdutoError> Erros { get; set; }

        [JsonProperty("reference", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public object Reference { get; set; }
    }

    public class GestaoProdutoError
    {
        [JsonProperty("codigo", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Codigo { get; set; } = "";

        [JsonProperty("messagem", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Messagem { get; set; } = "";

        [JsonProperty("propriedade ", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Propriedade { get; set; } = "";
    }
}
