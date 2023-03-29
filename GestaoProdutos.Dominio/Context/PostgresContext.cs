using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;

namespace GestaoProduto.Dominio
{
    public sealed class PostgresContext : DbContext
    {
        string _stringConexao;
        public PostgresContext(string stringConexao)
        {
            _stringConexao = stringConexao;
        }

        public DataSet ExecutarConsulta(string sqlConsulta)
        {           
            NpgsqlConnection connection = new NpgsqlConnection(_stringConexao);
            connection.Open();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sqlConsulta, connection);
            DataSet dsGenerico = new DataSet();
            adapter.Fill(dsGenerico);
            adapter.Dispose();
            connection.Close();
            return dsGenerico;
        }
    }
}
