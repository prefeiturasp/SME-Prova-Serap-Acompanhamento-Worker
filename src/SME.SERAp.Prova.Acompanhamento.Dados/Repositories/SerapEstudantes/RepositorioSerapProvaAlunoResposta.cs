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
                var query = @"select q.prova_id as ProvaId, 
                                     qar.aluno_ra as AlunoRa, 
                                     qar.questao_id as QuestaoId, 
                                     qar.alternativa_id as AlternativaId, 
                                     qar.tempo_resposta_aluno as Tempo
                              from questao q
                              left join questao_aluno_resposta qar on q.id = qar.questao_id
                              where q.prova_id = @provaId and qar.aluno_ra = @alunoRa ";

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
