using System.ComponentModel.DataAnnotations.Schema;

namespace furni1.Models
{
    public class Social
    {
        public int Id { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }
    }
}
