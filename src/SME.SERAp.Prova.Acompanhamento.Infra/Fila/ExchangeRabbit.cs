namespace SME.SERAp.Prova.Acompanhamento.Infra.Fila
{
    public static class ExchangeRabbit
    {
        public static string Log => "EnterpriseApplicationLog";
        public static string SerapEstudanteAcompanhamento => "serap.estudante.acomp.workers";
        public static string SerapEstudanteAcompanhamentoDeadLetter => "serap.estudante.acomp.workers.deadletter";
        public static int SerapDeadLetterTtl => 10 * 60 * 1000; /*10 Min * 60 Seg * 1000 milisegundos = 10 minutos em milisegundos*/
    }
}
