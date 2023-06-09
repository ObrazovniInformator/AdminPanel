namespace AdminPanel.Areas.Identity.Data
{
    public class PdfFajCasopis
    {
        public int Id { get; set; }
        public string NaslovPdf { get; set; }
        public string PdfPath { get; set; }
        public int IdCasopis { get; set; }
        public CasopisNaslov IdCasopisNavigation { get; set; }
    }
}
