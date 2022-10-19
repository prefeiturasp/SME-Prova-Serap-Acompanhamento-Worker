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
                var query = @"select a.id, a.ra, a.nome, a.nome_social as nomeSocial, a.situacao
                              from aluno a 
                              where a.turma_id = @turmaId";

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
