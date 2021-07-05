using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class SudskaPraksaViewModel
    {
        public IEnumerable<SudskaPraksa> SudskaPraksaList { get; set; }
        public IEnumerable<PropisSudskaPraksa> PropisSudskaPraksaList { get; set; }
        public SudskaPraksa SudskaPraksa { get; set; }
        public PropisSudskaPraksa PropisSudskaPraksa { get; set; }

        public SudskaPraksaViewModel()
        {
            SudskaPraksa = new SudskaPraksa();
            PropisSudskaPraksa = new PropisSudskaPraksa();
        }
    }
}