using System;

namespace Roulette.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public int IdentificationNumber { get; set; }
        public int IdentificationType { get; set; }
        public string Name { get; set; }
        public double AmountMax { get; set; }
    }
}
