using System.Collections.Generic;

namespace AdminPanel.Areas.Identity.Data
{
    public class PodpodpodpodrubrikaInAkta
    {
        public PodpodpodpodrubrikaInAkta()
        {
            InAkta = new HashSet<InAkta>();
        }
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int? IdPodpodpodrubrika { get; set; }
        public PodpodpodrubrikaInAkta IdPodpodpodrubrikaInAktaNavigation { get; set; }
        public ICollection<InAkta> InAkta { get; set; }
    }
}