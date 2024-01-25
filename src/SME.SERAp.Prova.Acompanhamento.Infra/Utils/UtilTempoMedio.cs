namespace SME.SERAp.Prova.Acompanhamento.Infra
{
    public static class UtilTempoMedio
    {
        public static long CalcularTempoMedioEmMinutos(int? tempoTotal, long totalFinalizados)
        {
            if (tempoTotal is null or 0) 
                return 0;
            
            return ((int)tempoTotal / 60) / totalFinalizados;
        }        
    }
}