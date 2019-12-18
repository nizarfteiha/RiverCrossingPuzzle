using System;
using System.Collections.Generic;

namespace RiverCrossingPuzzle
{
    public interface ICharacter
    {
        public List<ICharacter> riverRestricted { get; set; }
        public List<ICharacter> boatRestricted { get; set; }
        public string ToString();
    }
}
