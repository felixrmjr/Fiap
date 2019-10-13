using Dapper;
using Fiap.Domain.Entities;
using Fiap.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Fiap.Domain.Repository
{
    public class ParceriaRepository : RepositoryBase<Parceria, int>, IParceriaRepository
    {
        private string _selectBase = @"SELECT Codigo, Titulo, Descricao, UrlPagina, Empresa, DataInicio, DataTermino, QtdLikes, DataHoraCadastro FROM vParceria";

        public IEnumerable<Parceria> ObterTodas()
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                return con.Query<Parceria>(_selectBase);
            }
        }

        public Parceria ObterPorId(int codigo)
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                var where = $"WHERE Codigo = @Codigo";
                return con.QueryFirstOrDefault<Parceria>($"{_selectBase} {where}", new { Codigo = codigo });
            }
        }

        public object ObterParceriaPorNome(string termoPesquisa)
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                var where = @"WHERE @DataAtual BETWEEN DataInicio AND DataTermino AND (Titulo LIKE @TermoPesquisa OR Descricao LIKE @TermoPesquisa OR Empresa LIKE @TermoPesquisa)";
                return con.Query<Parceria>($"{_selectBase} {where}", new { DataAtual = DateTime.Now, TermoPesquisa = $"%{termoPesquisa}%" });

            }
        }

        public void Salvar(Parceria parceria, int operacao)
        {
            var param = new DynamicParameters();
            param.Add("@operacao", operacao);
            param.Add("@titulo", parceria.Titulo);
            param.Add("@descricao", parceria.Descricao);
            param.Add("@urlPagina", parceria.UrlPagina);
            param.Add("@empresa", parceria.Empresa);
            param.Add("@dataInicio", parceria.DataInicio);
            param.Add("@dataTermino", parceria.DataTermino);
            param.Add("@qtdLikes", parceria.QtdLikes);
            param.Add("@codigo", parceria.Codigo);

            using (var con = new SqlConnection(ConnectionString))
            {
                con.Execute("spParceria", param, null, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Parceria> ObterTodasValidas()
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                var where = $"WHERE @DataAtual BETWEEN DataInicio AND DataTermino";
                return con.Query<Parceria>($"{_selectBase} {where}", new { DataAtual = DateTime.Now });
            }
        }
    }
}

