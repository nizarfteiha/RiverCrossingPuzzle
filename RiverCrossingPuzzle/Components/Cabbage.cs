using System;
using System.Collections.Generic;

namespace RiverCrossingPuzzle
{
    public class Cabbage : ICharacter
    {
        public List<ICharacter> riverRestricted { get; set; }
        public List<ICharacter> boatRestricted { get; set; }

        public Cabbage()
        {
            this.riverRestricted = new List<ICharacter> { };
            this.boatRestricted = new List<ICharacter> { };
        }

        public Cabbage(List<ICharacter> riverRestricted, List<ICharacter> boatRestricted)
        {
            this.riverRestricted = riverRestricted;
            this.boatRestricted = boatRestricted;
        }

        override
        public string ToString()
        {
            return "C";
        }
    }
}
