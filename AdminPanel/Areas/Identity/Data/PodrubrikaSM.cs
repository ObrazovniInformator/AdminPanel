﻿using System.Collections.Generic;

namespace AdminPanel.Areas.Identity.Data
{
    public class PodrubrikaSM
    {
        public PodrubrikaSM()
        {
            SluzbenoMisljenje = new HashSet<SluzbenoMisljenje>();
        }
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int IdRubrika { get; set; }
        public RubrikaSM IdRubrikaNavigation { get; set; }
        public ICollection<SluzbenoMisljenje> SluzbenoMisljenje { get; set; }
    }
}
