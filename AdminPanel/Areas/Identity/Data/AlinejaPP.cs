namespace AdminPanel.Areas.Identity.Data
{
    public class AlinejaPP
    {
        public int Id { get; set; }
        public string NazivAlineje { get; set; }
        public string Tekst { get; set; }
        public int IdStav { get; set; }

        public StavPP IdStavNavigation { get; set; }
    }
}
