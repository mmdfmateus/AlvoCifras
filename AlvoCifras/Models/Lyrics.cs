using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlvoCifras.Models
{
    public class Lyrics : BaseModel
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
