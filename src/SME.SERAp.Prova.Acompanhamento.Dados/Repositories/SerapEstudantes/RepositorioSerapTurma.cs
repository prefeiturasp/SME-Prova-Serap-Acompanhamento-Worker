using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories.SerapEstudantes
{
    public class RepositorioSerapTurma : RepositorioSerap, IRepositorioSerapTurma
    {
        public RepositorioSerapTurma(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<AlunoDto>> ObterAlunosPorIdAsync(long turmaId, System.DateTime provaInicio, System.DateTime provaFim)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select distinct tb.id, tb.ra, tb.nome, tb.nomeSocial
                              from (
                                    select a.id, a.ra, a.nome, a.nome_social as nomeSocial 
                                    from aluno a 
                                    where a.turma_id = @turmaId 

                                    union all

                                    select a.id, a.ra, a.nome, a.nome_social as nomeSocial 
                                    from turma_aluno_historico tah 
                                    left join aluno a on a.id = tah.aluno_id 
                                    where a.turma_id = @turmaId and tah.data_matricula <= @provaInicio and tah.data_matricula <= @provaFim
                              ) tb";

                return await conn.QueryAsync<AlunoDto>(query, new { turmaId, provaInicio, provaFim });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<IEnumerable<TurmaDto>> ObterPorUeIdAsync(long ueId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select id,
	                                 ue_id as ueId,
	                                 codigo,
	                                 ano_letivo as anoLetivo,
	                                 ano,
	                                 nome,
	                                 modalidade_codigo as modalidade,
	                                 etapa_eja as etapaEja,
                                     tipo_turno as turno,
                                     serie_ensino as SerieEnsino
                              from turma 
                              where ue_id = @ueId";

                return await conn.QueryAsync<TurmaDto>(query, new { ueId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
