using System;
using System.Collections.Generic;

namespace RiverCrossingPuzzle
{
    public class Goat : ICharacter
    {
        public List<ICharacter> riverRestricted { get; set; }
        public List<ICharacter> boatRestricted { get; set; }

        public Goat()
        {
            this.riverRestricted = new List<ICharacter> { };
            this.boatRestricted = new List<ICharacter> { };
        }

        public Goat(List<ICharacter> riverRestricted, List<ICharacter> boatRestricted)
        {
            this.riverRestricted = riverRestricted;
            this.boatRestricted = boatRestricted;
        }

        override
        public string ToString()
        {
            return "G";
        }
    }
}
