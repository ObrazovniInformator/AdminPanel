namespace AdminPanel.Areas.Identity.Data
{
    public class Alineja
    {
        public int Id { get; set; }
        public string NazivAlineje { get; set; }
        public string Tekst { get; set; }
        public int IdStav { get; set; }

        public Stav IdStavNavigation { get; set; }
    }
}
