using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AlvoCifras.Models
{
    [DisplayName("Artista")]
    public class Artist : BaseModel
    {
        public Artist()
        {
            Songs = new HashSet<Songs>();
            Lyrics = new HashSet<Lyrics>();
        }

        [DisplayName("Nome")]
        public string Name { get; set; }

        public virtual ICollection<Songs> Songs { get; set; }
        public virtual ICollection<Lyrics> Lyrics { get; set; }
    }
}
