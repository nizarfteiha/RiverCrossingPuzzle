using System;
using System.Collections.Generic;
using RiverCrossingPuzzle.Models;

namespace RiverCrossingPuzzle.States
{
    public class State : IState
    {
        public KeyValuePair<List<ICharacter>, List<ICharacter>> state { get; set; }
        public RiverState riverState { get; set; }
        public BoatState boatState { get; set; }

        public State()
        {

        }
        public State(RiverState riverState,BoatState boatState)
        {
            this.riverState = riverState;
            this.boatState = boatState;
        }

        override
        public string ToString()
        {
            return "River:" +this.riverState.ToString() + " \n Boat:" + this.boatState.ToString();
        }

    }
}
