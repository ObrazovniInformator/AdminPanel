using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class PdfFajlProsvetniPropis
    {

        public int Id { get; set; }
        public string PdfPath { get; set; }
        public string NaslovPdf { get; set; }
        public int IdProsvetniPropis { get; set; }
        public ProsvetniPropis IdProsvetniPropisNavigation { get; set; }

    }
}
