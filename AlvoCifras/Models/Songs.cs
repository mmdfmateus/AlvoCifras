using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AlvoCifras.Models
{
    public class Songs : BaseModel
    {
        [DisplayName("Nome")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Cifra")]
        [Required]
        public string Tabs { get; set; }

        [Required]
        [ForeignKey("Artist")]
        public int ArtistId { get; set; }

        [DisplayName("Url")]
        [Required]
        public string Url { get; set; }

        public virtual Artist Artist { get; set; }
    }
}
