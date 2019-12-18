using System;
using System.Collections.Generic;

namespace RiverCrossingPuzzle
{
    public class Wolf : ICharacter
    {
        public List<ICharacter> riverRestricted { get; set; }
        public List<ICharacter> boatRestricted { get; set; }

        public Wolf()
        {
            this.riverRestricted = new List<ICharacter> { };
            this.boatRestricted = new List<ICharacter> { };
        }

        public Wolf(List<ICharacter> riverRestricted, List<ICharacter> boatRestricted)
        {
            this.riverRestricted = riverRestricted;
            this.boatRestricted = boatRestricted;
        }
        override
        public String ToString()
        {
            return "W";
        }

    }
}
