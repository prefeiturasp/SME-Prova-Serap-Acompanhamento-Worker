using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories.SerapEstudantes
{
    public class RepositorioSerapUe : RepositorioSerap, IRepositorioSerapUe
    {
        public RepositorioSerapUe(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<UeDto>> ObterPorDreIdAsync(long dreId)
        {
            using var conn = ObterConexao();

            var modalidades = new int[] { (int)Modalidade.Fundamental, (int)Modalidade.EJA, (int)Modalidade.CIEJA, (int)Modalidade.Medio };

            try
            {
                var query = @"select id,
                                     ue_id as codigo,
                                     dre_id as dreId,
                                     nome
                              from Ue u
                              where dre_id = @dreId
                                and exists(select 1 from turma t where t.ue_id = u.id and t.modalidade_codigo = ANY(@modalidades) limit 1)";

                return await conn.QueryAsync<UeDto>(query, new { dreId, modalidades });
            }
            catch (Exception ex)
            {
                var mensagem = ex.Message;
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
