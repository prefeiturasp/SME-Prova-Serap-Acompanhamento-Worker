using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories.SerapEstudantes
{
    public class RepositorioSerapProvaAlunoResposta : RepositorioSerap, IRepositorioSerapProvaAlunoResposta
    {
        public RepositorioSerapProvaAlunoResposta(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<ProvaAlunoRespostaDto>> ObterRespostasAsync(long provaId, long alunoRa)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select pa.prova_id as ProvaId, 
	                                qar.aluno_ra as AlunoRa, 
	                                qar.questao_id as QuestaoId, 
	                                qar.alternativa_id as AlternativaId, 
	                                qar.tempo_resposta_aluno as Tempo
                                from prova_aluno pa 
                                inner join questao q on q.prova_id = pa.prova_id 
                                inner join f_questao_aluno_resposta_prova_aluno(pa.prova_id, q.id, pa.aluno_ra) qar on 1 = 1
                                where pa.prova_id = @provaId
                                  and pa.aluno_ra = @alunoRa ";

                return await conn.QueryAsync<ProvaAlunoRespostaDto>(query, new { provaId, alunoRa });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
