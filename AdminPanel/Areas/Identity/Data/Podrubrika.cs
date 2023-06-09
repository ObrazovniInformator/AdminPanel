using System.Collections.Generic;

namespace AdminPanel.Areas.Identity.Data
{
    public class Podrubrika
    {
        public Podrubrika()
        {
           
            Propis = new HashSet<Propis>();
        }
        public int ID { get; set; }
        public string NazivPodrubrike { get; set; }
        public int IdRubrika { get; set; }

        public Rubrika IdRubrikaNavigation { get; set; }
        public ICollection<Propis> Propis { get; set; }
    }
}
