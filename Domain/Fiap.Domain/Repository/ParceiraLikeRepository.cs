using Dapper;
using Fiap.Domain.Entities;
using Fiap.Domain.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Fiap.Domain.Repository
{
    public class ParceiraLikeRepository : RepositoryBase<ParceiraLike, int>, IParceiraLikeRepository
    {
        public int InserirLike(int codigoParceira)
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                var parceria = con.QueryFirstOrDefault<Parceria>(@"SELECT Codigo, DataInicio, DataTermino FROM vParceria WHERE Codigo = @Codigo", new { Codigo = codigoParceira });

                parceria.ValidarInserirLike();

                var param = new DynamicParameters();
                param.Add("@CodigoParceira", codigoParceira);
                con.Execute("spParceriaLike", param, null, commandType: CommandType.StoredProcedure);

                return con.QueryFirstOrDefault<int>($"SELECT QtdLikes FROM vParceria WHERE Codigo = @Codigo", new { Codigo = codigoParceira });
            }
        }
    }
}