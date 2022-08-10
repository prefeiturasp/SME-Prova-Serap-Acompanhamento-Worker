namespace SME.SERAp.Prova.Acompanhamento.Infra.Fila
{
    public class RotaRabbit
    {
        public static string Log => "ApplicationLog";

        public const string IniciarSync = "serap.estudante.acomp.iniciar.sync";

        public const string DeadLetterSync = "serap.estudante.acomp.deadletter.sync";
        public const string DeadLetterTratar = "serap.estudante.acomp.deadletter.tratar";

        public const string DreSync = "serap.estudante.acomp.dre.sync";
        public const string DreTratar = "serap.estudante.acomp.dre.tratar";
        public const string UeSync = "serap.estudante.acomp.ue.sync";
        public const string UeTratar = "serap.estudante.acomp.ue.tratar";
        public const string TurmaSync = "serap.estudante.acomp.turma.sync";
        public const string TurmaTratar = "serap.estudante.acomp.turma.tratar";

        public const string AnoSync = "serap.estudante.acomp.ano.sync";
        public const string AnoTratar = "serap.estudante.acomp.ano.tratar";

        public const string ProvaSync = "serap.estudante.acomp.prova.sync";
        public const string ProvaTratar = "serap.estudante.acomp.prova.tratar";

        public const string AbrangenciaSync = "serap.estudante.acomp.abrangencia.sync";
        public const string AbrangenciaTratar = "serap.estudante.acomp.abrangencia.tratar";

        public const string ProvaAlunoSync = "serap.estudante.acomp.prova.aluno.sync";
        public const string ProvaAlunoTratar = "serap.estudante.acomp.prova.aluno.tratar";
        public const string ProvaAlunoResultadoTratar = "serap.estudante.acomp.prova.aluno.resultado.tratar";
        public const string ProvaTurmaResultadoTratar = "serap.estudante.acomp.prova.turma.resultado.tratar";

        public const string ProvaAlunoRespostaSync = "serap.estudante.acomp.prova.aluno.resposta.sync";
        public const string ProvaAlunoRespostaTratar = "serap.estudante.acomp.prova.aluno.resposta.tratar";
        public const string ProvaAlunoRespostaConsolidar = "serap.estudante.acomp.prova.aluno.resposta.consolidar";

        public const string ProvaAlunoDownloadTratar = "serap.estudante.acomp.prova.aluno.download.tratar";
        public const string ProvaAlunoInicioFimTratar = "serap.estudante.acomp.prova.aluno.inicio.fim.tratar";
    }
}
