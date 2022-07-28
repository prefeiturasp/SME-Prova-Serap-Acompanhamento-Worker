﻿using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
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
                var query = @"select min(pa.criado_em) as Inicio,
                                     max(case when pa.status = 2 then pa.finalizado_em end) as Fim,  
		                             count(qar.alternativa_id) as questaoRespondida,  
		                             sum(qar.tempo_resposta_aluno) / count(1) as tempoMedio,
                                     exists(select 1 from downloads_prova_aluno dpa where dpa.aluno_ra = @ra and dpa.prova_id = @provaId) as FezDownload
                              from questao_aluno_resposta qar
                              left join questao q on q.id = qar.questao_id 
                              left join prova_aluno pa on pa.prova_id = q.prova_id and pa.aluno_ra = qar.aluno_ra 
                              where qar.aluno_ra = @ra and q.prova_id = @provaId ";

                return await conn.QueryFirstOrDefaultAsync<SituacaoAlunoProvaDto>(query, new { provaId, ra });
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
	                                 p.inicio, 
	                                 p.fim 
                              from prova p ";

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
	                               vpta.inicio,
	                               vpta.fim,
	                               u.dre_id as dreId,
	                               vpta.ue_id as ueId,
	                               vpta.turma_ano as ano,
	                               vpta.turma_modalidade as modalidade,
	                               vpta.turma_id as TurmaId
                             from v_prova_turma_aluno vpta 
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
    }
}