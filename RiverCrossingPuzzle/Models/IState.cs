using System;
using System.Collections.Generic;

namespace RiverCrossingPuzzle.Models
{
    public interface IState
    {
        public KeyValuePair<List<ICharacter>,List<ICharacter>> state { get; set; }
    }
}
