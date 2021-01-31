    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Roulette.Entities
{
    public class Roulette
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int State { get; set; }
        public DateTime DateIni { get; set; }
        public DateTime DateEnd { get; set; }

        [NotMapped]
        public virtual string StateDesc { get; set; }
    }
}
