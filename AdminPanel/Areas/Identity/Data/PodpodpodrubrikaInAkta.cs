using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class PodpodpodrubrikaInAkta
    {
        public PodpodpodrubrikaInAkta()
        {
            InAkta = new HashSet<InAkta>();
            PodpodpodpodrubrikaInAkta = new HashSet<PodpodpodpodrubrikaInAkta>();
        }
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int? IdPodpodrubrika { get; set; }
        public PodpodrubrikaInAkta IdPodpodrubrikaInAktaNavigation { get; set; }
        public ICollection<InAkta> InAkta { get; set; }
        public ICollection<PodpodpodpodrubrikaInAkta> PodpodpodpodrubrikaInAkta { get; set; }
    }
}