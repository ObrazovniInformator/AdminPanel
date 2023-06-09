using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminPanel.Areas.Identity.Data
{
    public class Klijent : IdentityUser
    {
        [PersonalData]
        [Column(TypeName ="nvarchar(100)")]
        public string Ime { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Prezime { get; set; }
    }
}
