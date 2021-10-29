using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class PdfFajlPropis
    {
        public int Id { get; set; }
        public string PdfPath { get; set; }
        public string NaslovPdf { get; set; }
        public int IdPropis { get; set; }
        public Propis IdPropisNavigation { get; set; }
    }
}
