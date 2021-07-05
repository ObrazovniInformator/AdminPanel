using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class InAktaViewModel
    {
        public IEnumerable<InAkta> InAktaList { get; set; }
        public IEnumerable<PropisInAkta> PropisInAktaList { get; set; }
        public IEnumerable<ProsvetniPropisInAkta> ProsvetniPropisInAktaList { get; set; }
        public InAkta InAkta { get; set; }
        public PropisInAkta PropisInAkta { get; set; }
        public ProsvetniPropisInAkta ProsvetniPropisInAkta { get; set; }

        public InAktaViewModel()
        {
            InAkta = new InAkta();
            PropisInAkta = new PropisInAkta();
            ProsvetniPropisInAkta = new ProsvetniPropisInAkta();
        }
    }
}