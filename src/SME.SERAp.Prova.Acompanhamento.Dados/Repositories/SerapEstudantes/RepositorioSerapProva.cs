using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories.SerapEstudantes
{
    public class RepositorioSerapProva : RepositorioSerap, IRepositorioSerapProva
    {
        public RepositorioSerapProva(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<SituacaoAlunoProvaDto> ObterSituacaoAlunoAsync(long provaId, long ra)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select
	                            min(pa.criado_em) as Inicio,
	                            max(case when pa.status in (2, 5) then pa.finalizado_em end) as Fim,
	                            count(tb.alternativa_id) as QuestaoRespondida,
	                            sum(tb.tempo_resposta_aluno) Tempo,
	                            case when count(tb.alternativa_id) > 0 then sum(tb.tempo_resposta_aluno) / count(tb.alternativa_id) else 0 end as TempoMedio,
	                            exists(select 1	from downloads_prova_aluno dpa where dpa.aluno_ra = @ra and dpa.prova_id = @provaId limit 1) as FezDownload
                              from prova_aluno pa
                              left join (select q.prova_id, qar.aluno_ra, qar.tempo_resposta_aluno, qar.alternativa_id
	                          from questao_aluno_resposta qar 
	                          left join questao q on q.id = qar.questao_id) tb on tb.prova_id = pa.prova_id and tb.aluno_ra = pa.aluno_ra
                              where
	                            pa.aluno_ra = @ra and pa.prova_id = @provaId";

                return await conn.QueryFirstOrDefaultAsync<SituacaoAlunoProvaDto>(query, new { provaId, ra });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<SituacaoTurmaProvaDto> ObterSituacaoTurmaAsync(long provaId, long turmaId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select count(case when pa.criado_em::date = current_date then 1 end) as totalIniciadoHoje,
	                                 count(case when pa.criado_em::date < current_date and pa.status = 1 then 1 end) as totalIniciadoNaoFinalizado,
	                                 count(case when pa.status in (2, 5) then 1 end) as totalFinalizado
                              from prova_aluno pa
                              left join aluno a on a.ra = pa.aluno_ra
                              where pa.prova_id = @provaId and a.turma_id = @turmaId";

                return await conn.QueryFirstOrDefaultAsync<SituacaoTurmaProvaDto>(query, new { provaId, turmaId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<IEnumerable<ProvaDto>> ObterTodosAsync()
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select p.id, 
	                                 p.prova_legado_id as codigo, 
	                                 p.descricao, 
                                     p.modalidade,
	                                 p.inicio::date, 
	                                 p.fim::date 
                              from prova p 
                              where (p.ocultar_prova = false or p.ocultar_prova is null)";

                return await conn.QueryAsync<ProvaDto>(query);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<IEnumerable<ProvaDto>> ObterProvasEmAndamentoAsync()
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select p.id, 
	                                 p.prova_legado_id as codigo, 
	                                 p.descricao, 
                                     p.modalidade,
	                                 p.inicio::date, 
	                                 p.fim::date 
                              from prova p 
                              where inicio::date <= current_date and fim::date >= current_date 
                                and (p.ocultar_prova = false or p.ocultar_prova is null)";

                return await conn.QueryAsync<ProvaDto>(query);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<IEnumerable<ProvaTurmaDto>> ObterTurmasAsync(long provaId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select distinct vpta.turma_ano_letivo as anoLetivo,
                                   vpta.prova_id as provaId,
	                               vpta.inicio::date,
	                               vpta.fim::date,
	                               u.dre_id as dreId,
	                               vpta.ue_id as ueId,
	                               vpta.turma_ano as ano,
	                               vpta.turma_modalidade as modalidade,
	                               vpta.turma_id as TurmaId,
                                   p.descricao,
	                               p.total_itens as QuantidadeQuestoes
                             from v_prova_turma_aluno vpta 
                             left join prova p on p.id = vpta.prova_id 
                             left join ue u on u.id = vpta.ue_id
                             where vpta.prova_id = @provaId ";

                return await conn.QueryAsync<ProvaTurmaDto>(query, new { provaId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<ProvaTurmaDto> ObterTurmaAsync(long provaId, long turmaId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select distinct vpta.turma_ano_letivo as anoLetivo,
                                   vpta.prova_id as provaId,
	                               vpta.inicio,
	                               vpta.fim,
	                               u.dre_id as dreId,
	                               vpta.ue_id as ueId,
	                               vpta.turma_ano as ano,
	                               vpta.turma_modalidade as modalidade,
	                               vpta.turma_id as TurmaId,
                                   p.descricao,
	                               p.total_itens as QuantidadeQuestoes
                             from v_prova_turma_aluno vpta 
                             left join prova p on p.id = vpta.prova_id 
                             left join ue u on u.id = vpta.ue_id
                             where vpta.prova_id = @provaId 
                               and vpta.turma_id = @turmaId ";

                return await conn.QueryFirstOrDefaultAsync<ProvaTurmaDto>(query, new { provaId, turmaId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
