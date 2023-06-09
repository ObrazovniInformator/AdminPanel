using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Areas.Identity.Data
{
    public class Role
    {
        [Required]
        public string RoleName { get; set; }
    }
}
