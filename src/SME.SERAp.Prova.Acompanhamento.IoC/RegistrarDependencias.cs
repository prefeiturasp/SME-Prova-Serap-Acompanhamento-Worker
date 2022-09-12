using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SME.SERAp.Prova.Acompanhamento.Aplicacao;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.Coresso;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.Eol;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Dados.Repositories;
using SME.SERAp.Prova.Acompanhamento.Dados.Repositories.Coresso;
using SME.SERAp.Prova.Acompanhamento.Dados.Repositories.Eol;
using SME.SERAp.Prova.Acompanhamento.Dados.Repositories.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Services;
using SME.SERAp.Prova.Acompanhamento.IoC.Extensions;

namespace SME.SERAp.Prova.Acompanhamento.IoC
{
    public static class RegistraDependencias
    {
        public static void Registrar(IServiceCollection services)
        {
            services.AdicionarMediatr();
            services.AdicionarValidadoresFluentValidation();
            services.AddPoliticas();

            RegistrarServicos(services);
            RegistrarRepositorios(services);
            RegistrarRepositoriosSerap(services);
            RegistrarRepositoriosCoresso(services);
            RegistrarRepositoriosEol(services);
            RegistrarCasosDeUso(services);
        }

        private static void RegistrarServicos(IServiceCollection services)
        {
            services.TryAddSingleton<IServicoLog, ServicoLog>();
        }

        private static void RegistrarRepositorios(IServiceCollection services)
        {
            services.AddScoped<IRepositorioDre, RepositorioDre>();
            services.AddScoped<IRepositorioUe, RepositorioUe>();
            services.AddScoped<IRepositorioTurma, RepositorioTurma>();
            services.AddScoped<IRepositorioProva, RepositorioProva>();
            services.AddScoped<IRepositorioAbrangencia, RepositorioAbrangencia>();
            services.AddScoped<IRepositorioAno, RepositorioAno>();
            services.AddScoped<IRepositorioProvaAlunoResposta, RepositorioProvaAlunoResposta>();
            services.AddScoped<IRepositorioProvaAlunoResultado, RepositorioProvaAlunoResultado>();
            services.AddScoped<IRepositorioProvaTurmaResultado, RepositorioProvaTurmaResultado>();
            services.AddScoped<IRepositorioProvaQuestao, RepositorioProvaQuestao>();
        }

        private static void RegistrarRepositoriosEol(IServiceCollection services)
        {
            services.AddScoped<IRepositorioEolAtribuicao, RepositorioEolAtribuicao>();
        }

        private static void RegistrarRepositoriosCoresso(IServiceCollection services)
        {
            services.AddScoped<IRepositorioCoressoGrupo, RepositorioCoressoGrupo>();
            services.AddScoped<IRepositorioCoressoUsuario, RepositorioCoressoUsuario>();
            services.AddScoped<IRepositorioCoressoAbrangencia, RepositorioCoressoAbrangencia>();
        }

        private static void RegistrarRepositoriosSerap(IServiceCollection services)
        {
            services.AddScoped<IRepositorioSerapDre, RepositorioSerapDre>();
            services.AddScoped<IRepositorioSerapUe, RepositorioSerapUe>();
            services.AddScoped<IRepositorioSerapTurma, RepositorioSerapTurma>();
            services.AddScoped<IRepositorioSerapProva, RepositorioSerapProva>();
            services.AddScoped<IRepositorioSerapAbrangencia, RepositorioSerapAbrangencia>();
            services.AddScoped<IRepositorioSerapAno, RepositorioSerapAno>();
            services.AddScoped<IRepositorioSerapProvaAlunoResposta, RepositorioSerapProvaAlunoResposta>();
            services.AddScoped<IRepositorioSerapQuestao, RepositorioSerapQuestao>();
        }

        private static void RegistrarCasosDeUso(IServiceCollection services)
        {
            services.AddScoped<IIniciarSyncUseCase, IniciarSyncUseCase>();

            services.AddScoped<ITratarDeadletterSyncUseCase, TratarDeadletterSyncUseCase>();
            services.AddScoped<ITratarDeadletterUseCase, TratarDeadletterUseCase>();

            services.AddScoped<ITratarDreSyncUseCase, TratarDreSyncUseCase>();
            services.AddScoped<ITratarDreUseCase, TratarDreUseCase>();

            services.AddScoped<ITratarUeSyncUseCase, TratarUeSyncUseCase>();
            services.AddScoped<ITratarUeUseCase, TratarUeUseCase>();

            services.AddScoped<ITratarTurmaSyncUseCase, TratarTurmaSyncUseCase>();
            services.AddScoped<ITratarTurmaUseCase, TratarTurmaUseCase>();

            services.AddScoped<ITratarProvaSyncUseCase, TratarProvaSyncUseCase>();
            services.AddScoped<ITratarProvaUseCase, TratarProvaUseCase>();

            services.AddScoped<ITratarAbrangenciaSyncUseCase, TratarAbrangenciaSyncUseCase>();
            services.AddScoped<ITratarAbrangenciaGrupoUseCase, TratarAbrangenciaGrupoUseCase>();
            services.AddScoped<ITratarAbrangenciaGrupoUsuarioUseCase, TratarAbrangenciaGrupoUsuarioUseCase>();

            services.AddScoped<ITratarAbrangenciaExcluirUseCase, TratarAbrangenciaExcluirUseCase>();
            services.AddScoped<ITratarAbrangenciaGrupoExcluirUseCase, TratarAbrangenciaGrupoExcluirUseCase>();
            services.AddScoped<ITratarAbrangenciaGrupoUsuarioExcluirUseCase, TratarAbrangenciaGrupoUsuarioExcluirUseCase>();

            services.AddScoped<ITratarAbrangenciaUseCase, TratarAbrangenciaUseCase>();
            services.AddScoped<IExcluirAbrangenciaUseCase, ExcluirAbrangenciaUseCase>();

            services.AddScoped<ITratarAnoSyncUseCase, TratarAnoSyncUseCase>();
            services.AddScoped<ITratarAnoUseCase, TratarAnoUseCase>();

            services.AddScoped<ITratarProvaAlunoSyncUseCase, TratarProvaAlunoSyncUseCase>();
            services.AddScoped<ITratarProvaAlunoTurmaSyncUseCase, TratarProvaAlunoTurmaSyncUseCase>();
            services.AddScoped<ITratarProvaAlunoUseCase, TratarProvaAlunoUseCase>();

            services.AddScoped<ITratarProvaAlunoResultadoUseCase, TratarProvaAlunoResultadoUseCase>();
            services.AddScoped<ITratarProvaTurmaResultadoUseCase, TratarProvaTurmaResultadoUseCase>();
            services.AddScoped<IConsolidarProvaAlunoRespostaUseCase, ConsolidarProvaAlunoRespostaUseCase>();
            services.AddScoped<IRecalcularProvaTurmaResultadoUseCase, RecalcularProvaTurmaResultadoUseCase>();

            services.AddScoped<ITratarProvaAlunoRespostaSyncUseCase, TratarProvaAlunoRespostaSyncUseCase>();
            services.AddScoped<ITratarProvaAlunoRespostaUseCase, TratarProvaAlunoRespostaUseCase>();

            services.AddScoped<ITratarProvaAlunoResultadoDownloadUseCase, TratarProvaAlunoResultadoDownloadUseCase>();
            services.AddScoped<ITratarProvaAlunoResultadoInicioFimUseCase, TratarProvaAlunoResultadoInicioFimUseCase>();

            services.AddScoped<ITratarProvaQuestaoSyncUseCase, TratarProvaQuestaoSyncUseCase>();
            services.AddScoped<ITratarProvaQuestaoUseCase, TratarProvaQuestaoUseCase>();
        }
    }
}
