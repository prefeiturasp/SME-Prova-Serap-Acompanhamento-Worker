using Dapper;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.Eol;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories.Eol
{
    public class RepositorioEolAtribuicao : RepositorioBaseEol, IRepositorioEolAtribuicao
    {
        public RepositorioEolAtribuicao(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<string>> ObterAtribuicaoDreUePorRegistroFuncional(int[] tiposEscola, string codigoRf)
        {
            var conn = ObterConexao();
            try
            {
                var query = @"
					select coalesce(CdUnidadeEducacaoSobre, CdUnidadeEducacaoBase) as Codigo
					from (
						select
							sev.cd_registro_funcional, 
							ue_base.cd_unidade_educacao as CdUnidadeEducacaoBase,
							case 
								when ue_sobre.tp_unidade_educacao <> 3 then ue_sobre.cd_unidade_educacao 
							else 
								case 
									when ue_sobre.cd_unidade_administrativa_portal = '110000' then ue_sobre.cd_unidade_administrativa_portal 
								else
									ue_sobre.cd_unidade_administrativa_referencia end
							end as CdUnidadeEducacaoSobre
						from v_servidor_cotic sev with (nolock)
						-- Cargo Base
						inner join v_cargo_base_cotic cba with (nolock) on sev.cd_servidor = cba.cd_servidor
						left join lotacao_servidor ls with (nolock) on cba.cd_cargo_base_servidor = ls.cd_cargo_base_servidor and ls.dt_fim is null 
						-- Cargo Sobreposto
						left join cargo_sobreposto_servidor css with (nolock) on cba.cd_cargo_base_servidor = css.cd_cargo_base_servidor AND (css.dt_fim_cargo_sobreposto IS NULL OR css.dt_fim_cargo_sobreposto > Getdate())
						--Unidades
						left join v_cadastro_unidade_educacao ue_base with (nolock) on (ls.cd_unidade_educacao = ue_base.cd_unidade_educacao)
						left join escola esc_base with (nolock) on esc_base.cd_escola = ls.cd_unidade_educacao 
						left join v_cadastro_unidade_educacao ue_sobre with (nolock) on (css.cd_unidade_local_servico = ue_sobre.cd_unidade_educacao)
						left join escola esc_sobre with (nolock) on esc_sobre.cd_escola = ue_sobre.cd_unidade_educacao 
						where
							cba.dt_fim_nomeacao is null
						and cba.dt_cancelamento is null
						and sev.cd_registro_funcional = @codigoRf
						and (css.cd_cargo is not null or (
									(css.cd_cargo_base_servidor is not null and esc_sobre.tp_escola in @tiposEscola) or 
									(esc_base.tp_escola in @tiposEscola) or 
									(ue_base.tp_unidade_educacao in @tiposEscola)
								)
							)
					) tb";

                var param = new DynamicParameters();
                param.Add("@codigoRf", codigoRf, System.Data.DbType.AnsiStringFixedLength, null, 7);
                param.Add("@tiposEscola", tiposEscola);

                return await conn.QueryAsync<string>(query, param);
            }
            finally
            {
                conn.Dispose();
            }
        }

        public async Task<IEnumerable<string>> ObterAtribuicaoTurmaPorRegistroFuncional(int[] tiposEscola, string codigoRf)
        {
            var conn = ObterConexao();
            try
            {
                var query = @"
					select coalesce(stg.cd_turma_escola, tegp.cd_turma_escola) Codigo
					from atribuicao_aula (nolock) atb

					-- escolas
					inner join escola (nolock) esc on atb.cd_unidade_educacao = esc.cd_escola
					inner join v_cadastro_unidade_educacao (nolock) vue on vue.cd_unidade_educacao = esc.cd_escola
					inner join (select v_ua.cd_unidade_educacao, v_ua.nm_unidade_educacao, v_ua.nm_exibicao_unidade 
								from unidade_administrativa (nolock) ua
								inner join v_cadastro_unidade_educacao (nolock) v_ua on v_ua.cd_unidade_educacao = ua.cd_unidade_administrativa
								where tp_unidade_administrativa = 24) dre on dre.cd_unidade_educacao = vue.cd_unidade_administrativa_referencia

					--Servidor
					inner join v_cargo_base_cotic (nolock) cbs on atb.cd_cargo_base_servidor = cbs.cd_cargo_base_servidor
					inner join v_servidor_cotic (nolock) vsc on cbs.cd_servidor = vsc.cd_servidor

					-- SerieGrade
					left join serie_turma_grade (nolock) stg on atb.cd_serie_grade = stg.cd_serie_grade
					left join turma_escola (nolock) tur_reg on stg.cd_turma_escola = tur_reg.cd_turma_escola and tur_reg.an_letivo = atb.an_atribuicao and tur_reg.cd_tipo_turma <> 4

					-- ProgramaGrade
					left join turma_escola_grade_programa (nolock) tegp on tegp.cd_turma_escola_grade_programa = atb.cd_turma_escola_grade_programa
					left join turma_escola (nolock) tur_pro on tegp.cd_turma_escola = tur_pro.cd_turma_escola and tur_pro.an_letivo = atb.an_atribuicao and tur_reg.cd_tipo_turma <> 4

					where atb.dt_cancelamento is null
						and cbs.dt_cancelamento is null 
						and cbs.dt_fim_nomeacao is null
						and atb.an_atribuicao = year(getdate())
						and esc.tp_escola in @tiposEscola
						and vsc.cd_registro_funcional = @codigoRf
						and (tur_reg.cd_turma_escola is not null or tur_pro.cd_turma_escola is not null)";

                var param = new DynamicParameters();
                param.Add("@codigoRf", codigoRf, System.Data.DbType.AnsiStringFixedLength, null, 7);
                param.Add("@tiposEscola", tiposEscola);

                return await conn.QueryAsync<string>(query, param);
            }
            finally
            {
                conn.Dispose();
            }
        }
    }
}
