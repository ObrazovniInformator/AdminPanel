using System.Collections.Generic;

namespace AdminPanel.Areas.Identity.Data
{
    public class RubrikaPK
    {
        public RubrikaPK()
        {
            PrimeriKnjizenja = new HashSet<PrimeriKnjizenja>();
        }
        public int Id { get; set; }
        public string Naziv { get; set; }
        public ICollection<PrimeriKnjizenja> PrimeriKnjizenja { get; set; }
    }
}
