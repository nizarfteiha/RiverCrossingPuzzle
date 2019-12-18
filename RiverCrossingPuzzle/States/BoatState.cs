using System;
using System.Collections.Generic;
using RiverCrossingPuzzle.Models;

namespace RiverCrossingPuzzle.States
{
    public class BoatState 
    {
        public List<ICharacter> peopleInsideBoat { get; set; }
        public BoatState()
        {
            this.peopleInsideBoat = new List<ICharacter> { };
        }
        public BoatState(List<ICharacter> peopleInsideBoat)
        {
            this.peopleInsideBoat = peopleInsideBoat;
        }

        override
        public string ToString()
        {
            string boatState = "";
            foreach (ICharacter character in this.peopleInsideBoat)
            {
                boatState = boatState + character.ToString();
            }
            return boatState;
        }

    }
}
