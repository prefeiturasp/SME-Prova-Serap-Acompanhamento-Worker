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

        public async Task<IEnumerable<AlunoDto>> ObterAlunosPorIdAsync(long provaId, long turmaId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select a.id, a.ra, a.nome, a.nome_social as nomeSocial, a.situacao
                              from v_prova_turma_aluno vpta 
                              inner join aluno a on a.id = vpta.aluno_id
                              where vpta.prova_id = @provaId 
                                and vpta.turma_id = @turmaId";

                return await conn.QueryAsync<AlunoDto>(query, new { provaId, turmaId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<IEnumerable<AlunoDto>> ObterAlunosPorIdDeficienciaAsync(long provaId, long turmaId, long[] deficiencias)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select a.id, a.ra, a.nome, a.nome_social as nomeSocial, a.situacao
                              from v_prova_turma_aluno vpta 
                              inner join prova p on p.id = vpta.prova_id 
                              inner join aluno a on a.id = vpta.aluno_id
                              where vpta.prova_id = @provaId 
                                  and vpta.turma_id = @turmaId
                                  and exists(
                                    select 1
                                    from aluno_deficiencia ad 
                                    left join tipo_deficiencia td on td.id = ad.deficiencia_id 
                                    where ad.aluno_ra = vpta.aluno_ra 
                                      and td.codigo_eol = ANY(@deficiencias)
                                  )";

                return await conn.QueryAsync<AlunoDto>(query, new { provaId, turmaId, deficiencias });
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
                                     serie_ensino as SerieEnsino,
                                     semestre  
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
