using System.Collections.Generic;

namespace AdminPanel.Areas.Identity.Data
{
    public class PodrubrikaInAkta
    {
        public PodrubrikaInAkta()
        {
            InAkta = new HashSet<InAkta>();
            PodpodrubrikaInAkta = new HashSet<PodpodrubrikaInAkta>();
        }
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int? IdRubrika { get; set; }
        public RubrikaInAkta IdRubrikaInAktaNavigation { get; set; }
        public ICollection<InAkta> InAkta { get; set; }
        public ICollection<PodpodrubrikaInAkta> PodpodrubrikaInAkta { get; set; }
    }
}