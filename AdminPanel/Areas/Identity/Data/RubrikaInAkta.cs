using System.Collections.Generic;

namespace AdminPanel.Areas.Identity.Data
{
    public class RubrikaInAkta
    {
        public RubrikaInAkta()
        {
            InAkta = new HashSet<InAkta>();
            PodrubrikaInAkta = new HashSet<PodrubrikaInAkta>();
        }
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int? IdPodvrsta { get; set; }
        public InAktaPodvrsta IdInAktaPodvrstaNavigation { get; set; }
        public ICollection<InAkta> InAkta { get; set; }
        public ICollection<PodrubrikaInAkta> PodrubrikaInAkta { get; set; }
    }
}