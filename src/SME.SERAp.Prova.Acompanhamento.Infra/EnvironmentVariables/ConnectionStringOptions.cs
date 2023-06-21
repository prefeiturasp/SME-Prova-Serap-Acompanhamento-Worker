namespace SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables
{
    public class ConnectionStringOptions
    {
        public static string Secao => "ConnectionStrings";
        public string ApiSerap { get; set; }
        public string CoreSSO { get; set; }
        public string Eol { get; set; }
    }
}
