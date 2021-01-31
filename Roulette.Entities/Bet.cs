using System;
using System.Collections.Generic;
using System.Text;

namespace Roulette.Entities
{
    public class Bet
    {
        public int Id { get; set; }
        public int RouletteId { get; set; }
        public int Color { get; set; }
        public int Number { get; set; }
        public double Amount { get; set; }
    }
}
